using ExcelControl;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Data;

namespace GmarketMacro
{
    public partial class Main : Form
    {
        const string urlText = "링크를 입력하여 주세요.";

        const string fileName = "파일명을 입력해주세요.";

        const string adminIdText = "admin 아이디";

        delegate void Control_Invoker();

        delegate void Control_Invoker_ParamTxt(string text);

        delegate void Control_Invoker_ParamBool(bool bl);

        Thread actionThread = null;

        string DoneGoodsCode { get; set; } = string.Empty;

        string testDoneGoodsCode { get; set; } = string.Empty;

        bool SilentMode { get; set; } = true;

        //bool IsStopActionThread { get; set; }

        int Tab1SelectedIndex { get; set; }
        string Tab1txtUrl { get; set; }
        string Tab1txtFilePath { get; set; }
        long Tab1nudStart { get; set; }
        long Tab1nudEnd { get; set; }
        int Tab1CboFileNameType { get; set; }
        string Tab1TxtFileName { get; set; }
        string Tab2txtAdminIdTab1 { get; set; }
        string Tab2TxtGoodsCodeFilePath1 { get; set; }
        string Tab2TxtGoodsNameFilePath1 { get; set; }
        int Tab2CboFileNameType1 { get; set; }
        string Tab2TxtFileName1 { get; set; }
        string Tab3txtAdminIdTab2 { get; set; }
        string Tab3txtFilePath2 { get; set; }
        string Tab3FaildFilePath { get; set; }

        public Main()
        {
            InitializeComponent();

            txtUrl.Text = urlText;
            TxtFileName.Text = TxtFileName.ReadOnly ? string.Empty : fileName;
            TxtFileName1.Text = TxtFileName1.ReadOnly ? string.Empty : fileName;
            txtAdminIdTab1.Text = adminIdText;
            txtAdminIdTab2.Text = adminIdText;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // 초기 실행 시 작업표시 줄에 아이콘 안뜨는 현상 임시 폼을 하나 띄워 어거지로 포커스 빠졌다 받도록 구현 (iTalk Theme 버그 찾기 힘들어서..)
            (new ShowTool()).ShowDialog();

            // 크롬 실행 모드 초기화
            SilentMode = GetConfigData(AppConfigKeys.isSilentMode.ToString()) == "true";

            // 엑셀 생성파일명 변경 컨트롤 초기화
            CboFileNameType.SelectedIndex = GetConfigData(AppConfigKeys.tab1FileNameType.ToString()) == "1" ? 1 : CboFileNameType.SelectedIndex;
            string tab1FileName = GetConfigData(AppConfigKeys.tab1FileName.ToString());
            TxtFileName.Text = !TxtFileName.ReadOnly && string.IsNullOrEmpty(tab1FileName) ? fileName : tab1FileName;
            CboFileNameType1.SelectedIndex = GetConfigData(AppConfigKeys.tab2FileNameType.ToString()) == "1" ? 1 : CboFileNameType1.SelectedIndex;
            string tab2FileName = GetConfigData(AppConfigKeys.tab1FileName.ToString());
            TxtFileName1.Text = !TxtFileName1.ReadOnly && string.IsNullOrEmpty(tab2FileName) ? fileName : tab2FileName;

            // 파일 경로 초기화
            txtFilePath.Text = GetConfigData(AppConfigKeys.tab1SetFilePath.ToString());
            TxtGoodsCodeFilePath1.Text = GetConfigData(AppConfigKeys.tab2GetFilePath.ToString());
            TxtGoodsNameFilePath1.Text = GetConfigData(AppConfigKeys.tab2SetFilePath.ToString());
            txtFilePath2.Text = GetConfigData(AppConfigKeys.tab3GetFilePath.ToString());
            TxtFaildFilePath.Text = GetConfigData(AppConfigKeys.tab3FaildFilePath.ToString());
        }

        #region 1. 상품 코드 수집

        /// <summary>
        /// url 클릭 시 문구 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtUrl_Enter(object sender, EventArgs e)
        {
            if (txtUrl.Text == urlText)
            {
                txtUrl.Text = string.Empty;
            }
        }

        /// <summary>
        /// url 텍스트박스 벗어날때 문구 다시 삽입
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtUrl_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUrl.Text))
            {
                txtUrl.Text = urlText;
            }
        }

        /// <summary>
        /// 파일 경로 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetFile_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = GetFilePath();
            SetConfigData(AppConfigKeys.tab1SetFilePath.ToString(), txtFilePath.Text);
        }
        
        private void StopGetGoodsCode(string resultMsg = "")
        {
            if (btnGetGoodsCode.InvokeRequired)
            {
                var ci = new Control_Invoker_ParamTxt(StopGetGoodsCode);
                this.BeginInvoke(ci, resultMsg);
            }
            else
            {
                StopActionThread();
                btnGetGoodsCode.Text = "상품코드 수집";
                ShowTab1ProgragssIndicator(false);

                if (string.IsNullOrEmpty(resultMsg))
                {
                    MessageBoxEx.Show(this, "상품코드 수집이 중지 되었습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var showResult = new ShowResult(resultMsg);
                    showResult.ShowDialog();
                }
                    
            }
        }

        private void StopGetAdminAddData(string resultMsg = "")
        {
            if (btnAdd.InvokeRequired)
            {
                var ci = new Control_Invoker_ParamTxt(StopGetAdminAddData);
                this.BeginInvoke(ci, resultMsg);
            }
            else
            {
                StopActionThread();
                btnAdd.Text = "상품명 입력";
                ShowTab3ProgragssIndicator(false);

                if (string.IsNullOrEmpty(resultMsg))
                {
                    string msg = "상품명 입력이 중지 되었습니다.";
                    if (!string.IsNullOrEmpty(DoneGoodsCode))
                        msg += $"\r\n마지막 입력 상품코드 : {DoneGoodsCode}";

                    MessageBoxEx.Show(this, msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var showResult = new ShowResult(resultMsg);
                    showResult.ShowDialog();
                }

                DoneGoodsCode = string.Empty;
            }
        }

        /// <summary>
        /// 상품코드 가져오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetGoodsCode_Click(object sender, EventArgs e)
        {
            if (btnGetGoodsCode.Text == "상품코드 수집 중지")
            {
                StopGetGoodsCode();
                return;
            }

            Tab1SelectedIndex = cboGetType.SelectedIndex;
            Tab1txtUrl = txtUrl.Text;
            Tab1txtFilePath = txtFilePath.Text;
            Tab1nudStart = nudStart.Value;
            Tab1nudEnd = nudEnd.Value;
            Tab1CboFileNameType =  CboFileNameType.SelectedIndex;
            Tab1TxtFileName = TxtFileName.Text;

            ClearGetGoodsCodeLog();

            WriteGetGoodsCodeLog("1. G마켓 링크 유효성 검사");

            if (nudStart.Value <= 0 || nudEnd.Value <= 0)
            {
                //StopGetGoodsCode();
                MessageBoxEx.Show(this, "상품코드 수집 번호가 잘못 설정 되었습니다. 상품코드 수집 번호를 1부터 설정하여 주세요. ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (nudStart.Value > nudEnd.Value)
            {
                //StopGetGoodsCode();
                MessageBoxEx.Show(this, "상품코드 수집 번호가 잘못 설정 되었습니다. 상품코드 수집 끝번호가 시작 번호보다 큽니다. ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Tab1txtUrl == urlText)
            {
                //StopGetGoodsCode();
                MessageBoxEx.Show(this, "G마켓 링크를 입력하여 주십시오.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteGetGoodsCodeLog("동작 중지 : G마켓 링크를 입력하여 주십시오.");
                return;
            }

            bool isUrl = Uri.TryCreate(Tab1txtUrl, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!isUrl)
            {
                //StopGetGoodsCode();
                MessageBoxEx.Show(this, "G마켓 페이지 링크가 올바르지 않습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteGetGoodsCodeLog("동작 중지 : G마켓 페이지 링크가 올바르지 않습니다.");
                return;
            }

            if (string.IsNullOrEmpty(Tab1txtFilePath))
            {
                //StopGetGoodsCode();
                MessageBoxEx.Show("파일 생성 경로가 지정되지 않았습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteGetGoodsCodeLog("동작 중지 : 파일 생성 경로가 지정되지 않았습니다.");
                return;
            }

            if (!Directory.Exists(Tab1txtFilePath))
            {
                //StopGetGoodsCode();
                MessageBoxEx.Show("파일 생성 경로가 잘못 지정 되었습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteGetGoodsCodeLog("동작 중지 : 파일 생성 경로가 잘못 지정 되었습니다.");
                return;
            }

            if (CboFileNameType.SelectedIndex == 1 && TxtFileName.Text == fileName)
            {
                //StopGetGoodsCode();
                MessageBoxEx.Show("생성할 파일명을 입력하지 않았습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteGetGoodsCodeLog("동작 중지 : 생성할 파일명을 입력하지 않았습니다.");
                return;
            }

            ShowTab1ProgragssIndicator(true);
            var ts = new ThreadStart(GetGoodsCodeProc);
            actionThread = new Thread(ts);
            actionThread.Start();

            btnGetGoodsCode.Text = "상품코드 수집 중지";
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetGoodsCodeProc()
        {
            testDoneGoodsCode = "testDone";

            // 상품코드 가져오기
            List<DataModel> goods = GetGoodsCodeList();

            WriteGetGoodsCodeLog($"상품코드 중복제거 전 갯수 : {goods.Count}");
            WriteGetGoodsCodeLog("6. 상품코드 중복제거");

            goods = goods.GroupBy(g => g.GooodsCode)?.Select(s => s.FirstOrDefault())?.ToList();

            if (goods == null || goods.Count < 1)
            {
                WriteGetGoodsCodeLog("상품코드 수집종료 : 상품코드를 수집하지 못하였습니다.");
                ShowTab1ProgragssIndicator(false);
                StopGetGoodsCode("상품코드 수집종료 : 상품코드를 수집하지 못하였습니다.");
                return;
            }

            WriteGetGoodsCodeLog("7. 엑셀파일 생성");
            WriteGetGoodsCodeLog($"6_1. 엑셀 내보내기 대기 중인 상품코드 갯수 : {goods.Count}");

            string fileName = $"상품코드수집_{DateTime.Now.ToString("yyyy_MM_dd_HHmmss")}";
            if (Tab1CboFileNameType == 1)
                fileName = $"{Tab1TxtFileName}_{DateTime.Now.ToString("yyyy_MM_dd_HHmmss")}";

            CreateExcel<List<DataModel>>(goods, Tab1txtFilePath, fileName, WriteGetGoodsCodeLog);

            WriteGetGoodsCodeLog("8. 상품코드 수집 종료");
            
            ShowTab1ProgragssIndicator(false);
            StopGetGoodsCode("상품코드 수집이 완료 되었습니다.");

            //ShowThreadMessageBox("상품코드 수집이 완료 되었습니다.");
        }

        /// <summary>
        /// 상품코드 가져오기 (HTML 타입별 분기)
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private List<DataModel> GetGoodsCodeList()
        {
            int start = (int)Tab1nudStart;
            int end = (int)Tab1nudEnd;
            var goods = new List<DataModel>();

            var siteType = txtUrl.Text.Contains("mg.gmarket.co.kr") ? "mg" : "";

            switch (siteType)
            {
                case "mg": // G마켓 글로벌 모바일 사이트
                    var mobileGmarketAction = new MobileGmarketAction(SilentMode);

                    WriteGetGoodsCodeLog("2. 상품코드 가져오기 시작");
                    if (!mobileGmarketAction.Start(WriteGetGoodsCodeLog))
                    {
                        WriteGetGoodsCodeLog("동작 중지");
                        return goods;
                    }

                    WriteGetGoodsCodeLog("3. G마켓 페이지로 이동");
                    if (!mobileGmarketAction.SetUrl(txtUrl.Text, WriteGetGoodsCodeLog))
                    {
                        WriteGetGoodsCodeLog("동작 중지");
                        return goods;
                    }

                    switch (Tab1SelectedIndex)
                    {
                        case 0: // 페이지 단위
                            WriteGetGoodsCodeLog($"4. http://mg.gmarket.co.kr 사이트 전체 페이지 상품 코드 가져오기 시작");
                            goods = mobileGmarketAction.GetPageTypeGoods(start, end, WriteGetGoodsCodeLog);
                            break;

                        case 1: // 갯수 단위
                            WriteGetGoodsCodeLog($"4. http://mg.gmarket.co.kr 사이트 {start} 번째 부터 ~ {end} 번째 까지 상품 코드 가져오기 시작");
                            goods = mobileGmarketAction.GetNumTypeGoods(start, end, WriteGetGoodsCodeLog);
                            break;
                    }

                    mobileGmarketAction.Close();
                    break;

                default: // G마켓 글로벌 PC 웹 사이트
                    
                    var pcGmarketAction = new PCGmarketAction(SilentMode);

                    WriteGetGoodsCodeLog("2. 상품코드 가져오기 시작");
                    if (!pcGmarketAction.Start(WriteGetGoodsCodeLog))
                    {
                        WriteGetGoodsCodeLog("동작 중지");
                        return goods;
                    }

                    WriteGetGoodsCodeLog("3. G마켓 페이지로 이동");
                    if (!pcGmarketAction.SetUrl(txtUrl.Text.Trim(), WriteGetGoodsCodeLog))
                    {
                        WriteGetGoodsCodeLog("동작 중지");
                        return goods;
                    }

                    switch (Tab1SelectedIndex)
                    {
                        case 0: // 페이지 단위
                            WriteGetGoodsCodeLog($"4. {start} 페이지 부터 ~ {end} 페이지 까지 상품 코드 가져오기 시작");
                            goods = pcGmarketAction.GetPageTypeGoods(start, end, WriteGetGoodsCodeLog);
                            break;

                        case 1: // 갯수 단위
                            WriteGetGoodsCodeLog($"4. {start} 번째 부터 ~ {end} 번째 까지 상품 코드 가져오기 시작");
                            goods = pcGmarketAction.GetNumTypeGoods(start, end, WriteGetGoodsCodeLog);
                            break;
                    }

                    pcGmarketAction.Close();
                    break;
                
            }

            WriteGetGoodsCodeLog($"4_1. {goods.Count} 개의 상품 갯수 엑셀 내보내기 대기 중...");
            return goods;
        }

        
        #endregion 

        #region 2. 상품명 수집

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAdminIdTab1_Enter(object sender, EventArgs e)
        {
            if (txtAdminIdTab1.Text == adminIdText)
            {
                txtAdminIdTab1.Text = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAdminIdTab1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAdminIdTab1.Text))
            {
                txtAdminIdTab1.Text = adminIdText;
            }
        }

        private void BtnGetFileExplore1_Click(object sender, EventArgs e)
        {
            TxtGoodsCodeFilePath1.Text = GetFileName();
            SetConfigData(AppConfigKeys.tab2GetFilePath.ToString(), TxtGoodsCodeFilePath1.Text);
        }

        private void BtnGetGoodsNameFilePath1_Click(object sender, EventArgs e)
        {
            TxtGoodsNameFilePath1.Text = GetFilePath();
            SetConfigData(AppConfigKeys.tab2SetFilePath.ToString(), TxtGoodsNameFilePath1.Text);
        }

        private void StopGetAdminData(string stopMsg = "")
        {
            if (btnAdd1.InvokeRequired)
            {
                var ci = new Control_Invoker_ParamTxt(StopGetAdminData);
                this.BeginInvoke(ci, stopMsg);
            }
            else
            {
                StopActionThread();
                ShowTab2ProgragssIndicator(false);
                btnAdd1.Text = "상품명 수집";
                if (string.IsNullOrEmpty(stopMsg))
                {
                    MessageBoxEx.Show(this, "상품명 수집이 중지 되었습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var showResult = new ShowResult(stopMsg);
                    showResult.ShowDialog();
                }
            }
        }

        private void BtnAdd1_Click(object sender, EventArgs e)
        {
            if (btnAdd1.Text == "상품명 수집 중지")
            {
                StopGetAdminData();
                return;
            }

            Tab2txtAdminIdTab1 = txtAdminIdTab1.Text;
            Tab2TxtGoodsCodeFilePath1 = TxtGoodsCodeFilePath1.Text;
            Tab2TxtGoodsNameFilePath1 = TxtGoodsNameFilePath1.Text;
            Tab2CboFileNameType1 = CboFileNameType1.SelectedIndex;
            Tab2TxtFileName1 = TxtFileName1.Text;

            // 설정 지정 됐는지 확인

            if (Tab2txtAdminIdTab1 == adminIdText)
            {
                MessageBoxEx.Show(this, "Admin 아이디를 입력하여 주십시오.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(Tab2TxtGoodsCodeFilePath1))
            {
                MessageBoxEx.Show(this, "상품 코드 수집 된 엑셀파일의 경로를 지정하여 주십시오.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 엑셀파일 있는지 확인
            if (!File.Exists(Tab2TxtGoodsCodeFilePath1))
            {
                MessageBoxEx.Show(this, "상품 코드 수집 된 엑셀파일의 경로가 잘못 되었습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(Tab2TxtGoodsNameFilePath1))
            {
                MessageBoxEx.Show(this, "상품명 수집 후 엑셀 파일 생성 할 경로를 지정하여 주십시오.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (CboFileNameType1.SelectedIndex == 1 && TxtFileName1.Text == fileName)
            {
                MessageBoxEx.Show("생성할 파일명을 입력하지 않았습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ShowTab2ProgragssIndicator(true);

            var ts = new ThreadStart(GetAdminData);
            actionThread = new Thread(ts);
            actionThread.Start();

            btnAdd1.Text = "상품명 수집 중지";

        }

        public void ShowThreadMessageBox(string msg)
        {
            if (lvCrawlerLog.InvokeRequired)
            {
                var ci = new Control_Invoker_ParamTxt(ShowThreadMessageBox);
                this.BeginInvoke(ci, msg);
            }
            else
            {
                MessageBoxEx.Show(this, msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 상품 이름 수집 동작 수행
        /// </summary>
        /// <returns></returns>
        public void GetAdminData()
        {
            // 상품코드 엑셀 파일 가져오기 시작
            var fromExcel = new FromExcel();
            var err = string.Empty;
            var dt = fromExcel.OpenExcel(Tab2TxtGoodsCodeFilePath1, ref err);
            
            if (dt == null)
            {
                if (err.Contains("다른 프로세스에서 사용 중"))
                {
                    ShowThreadMessageBox($"해당 엑셀 파일이 실행 중입니다. \r\n엑셀파일을 종료하고 다시 시도 해주세요.\r\n파일명 : {Tab2TxtGoodsCodeFilePath1}");
                }
                else
                {
                    ShowThreadMessageBox($"해당 엑셀 파일을 가져오는 중 오류가 발생하였습니다. \r\n파일명 : {Tab2TxtGoodsCodeFilePath1} \r\n오류 : {err}");
                }

                StopGetAdminData();
                ShowTab2ProgragssIndicator(false);
                return;
            }
            else if (dt.Rows == null || dt.Rows.Count < 1)
            {
                StopGetAdminData();
                ShowTab2ProgragssIndicator(false);
                //ShowThreadMessageBox("엑셀 파일에 상품코드가 없습니다.");
                return;
            }

            // 데이터모델 리스트에 담기
            var goods = new List<DataModel>();

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    var dataModel = new DataModel();

                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName == "상품코드")
                            dataModel.GooodsCode = row[column]?.ToString() ?? string.Empty;
                    }

                    if (!string.IsNullOrEmpty(dataModel.GooodsCode))
                        goods.Add(dataModel);
                }
            }
            catch (Exception e)
            {
                StopGetAdminData();
                ShowTab3ProgragssIndicator(false);
                ShowThreadMessageBox($"엑셀파일을 가져올 수 없어 상품명 입력이 중지됐습니다. \r\n에러메시지 : {e.Message}");
                return;
            }

            // 로그 화면 초기화
            ClearAdminCrawlerLog();
            
            var action = new AdminAction(SilentMode);

            WriteAdminCrawlerLog("상품코드 이용하여 admin에서 상품이름 수집 시작");
            if (!action.Start(WriteAdminCrawlerLog))
            {
                StopGetAdminData();
                ShowTab2ProgragssIndicator(false);
                WriteAdminCrawlerLog("동작 중지 : 크롬 브라우저 실행 중 오류가 발생하였습니다.");
                return;
            }

            // 로그인 페이지 이동
            if (!action.SetUrl("http://ccadmin.gmarket.co.kr/GMKT.KRAdmin.Web/Admin/ForeignGoodsNameMng_Gate.aspx", WriteAdminCrawlerLog))
            {   
                StopGetAdminData();
                ShowTab2ProgragssIndicator(false);
                WriteAdminCrawlerLog("동작 중지 : G마켓 Admin 페이지 로드 중 오류가 발생하였습니다.");
                return;
            }

            Thread.Sleep(500);
            WriteAdminCrawlerLog("아이디 입력");
            // 아이디입력 ex) Global001
            if (!action.SetInputValue("loginId", Tab2txtAdminIdTab1, WriteAdminCrawlerLog))
            {
                StopGetAdminData();
                ShowTab2ProgragssIndicator(false);
                WriteAdminCrawlerLog("동작 중지 : Admin 아이디가 입력중 오류 발생 하였습니다.");
                return;
            }

            WriteAdminCrawlerLog("로그인 버튼 클릭");
            if (!action.ClickButton("btnLogin", WriteAdminCrawlerLog))
            {
                StopGetAdminData();
                ShowTab2ProgragssIndicator(false);
                WriteAdminCrawlerLog("동작 중지 : 로그인 버튼 클릭 중 오류가 발생 하였습니다.");
                return;
            }

            WriteAdminCrawlerLog("로그인 완료까지 대기 중");
            // 로그인 완료 시 까지 기다리기
            string mainPage = "http://ccadmin.gmarket.co.kr/GMKT.KRAdmin.Web/B_GOODS_TOTAL_MNG/ForeignGoodsNameMng.aspx";
            for (int roopCnt = 1; roopCnt <= 5; roopCnt++)
            {
                Thread.Sleep(300);

                string currentPage = action.GetUrl();
                if (currentPage == mainPage)
                {
                    WriteAdminCrawlerLog("로그인 성공");
                    break;
                }

                if (roopCnt == 5)
                {
                    StopGetAdminData("동작 중지 : 로그인 실패로 동작이 중지 됩니다.");
                    ShowTab2ProgragssIndicator(false);
                    WriteAdminCrawlerLog("동작 중지 : 로그인 실패로 동작이 중지 됩니다.");
                    return;
                }
            }

            WriteAdminCrawlerLog("상품이름 가져오기 시작");
            
            //== 루프 시작 : 상품명 수집 시작
            foreach (var dataModel in goods)
            {
                if (string.IsNullOrEmpty(dataModel.GooodsCode))
                    continue;

                WriteAdminCrawlerLog($"[{dataModel.GooodsCode}] 상품이름 가져오기 시작");

                Thread.Sleep(400);
                WriteAdminCrawlerLog("상품코드 입력");
                if (!action.SetInputValue("txtGdNo", dataModel.GooodsCode, WriteMacroLog))
                {
                    StopGetAdminData("동작 중지 : 로그인 실패로 동작이 중지 됩니다.");
                    ShowTab2ProgragssIndicator(false);
                    WriteAdminCrawlerLog("동작 중지 : 로그인 실패로 동작이 중지 됩니다.");
                    return;
                }

                WriteAdminCrawlerLog("검색 버튼 클릭");
                if (!action.ClickButton("btnSearch", WriteAdminCrawlerLog))
                {
                    StopGetAdminData("동작 중지 : 검색버튼 클릭 중 오류가 발생되었습니다.");
                    ShowTab2ProgragssIndicator(false);
                    WriteAdminCrawlerLog("동작 중지 : 검색버튼 클릭 중 오류가 발생되었습니다.");
                    return;
                }

                Thread.Sleep(600);

                WriteAdminCrawlerLog("테이블 클릭");
                if (!action.DoubleClickGoodsTableRow("//*[@id='__grid_grid']/div[2]/table/tbody/tr[2]/td[1]", WriteAdminCrawlerLog))
                {
                    StopGetAdminData("동작 중지 : 상품코드 테이블 클릭 중 오류가 발생되었습니다.");
                    ShowTab2ProgragssIndicator(false);
                    WriteAdminCrawlerLog("동작 중지 : 상품코드 테이블 클릭 중 오류가 발생되었습니다.");
                    return;
                }

                Thread.Sleep(500);

                WriteAdminCrawlerLog("상품명 가져오기 시작");
                dataModel.NameKOR = action.GetInputValue("txtGdNm", WriteAdminCrawlerLog);
                dataModel.NameEN = action.GetInputValue("txtGdEngNm", WriteAdminCrawlerLog);
                dataModel.NameCN = action.GetInputValue("txtGdNm_Cn", WriteAdminCrawlerLog);
                dataModel.NameJP = action.GetInputValue("txtGdNm_Jp", WriteAdminCrawlerLog);

            } //== 루프 종료

            WriteGetGoodsCodeLog("엑셀파일 생성");
            string fileName = $"상품명수집_{DateTime.Now.ToString("yyyy_MM_dd_HHmmss")}";
            if (Tab2CboFileNameType1 == 1)
                fileName = $"{Tab2TxtFileName1}_{DateTime.Now.ToString("yyyy_MM_dd_HHmmss")}";

            CreateExcel<List<DataModel>>(goods, Tab2TxtGoodsNameFilePath1, fileName, WriteAdminCrawlerLog);

            action.Close();
            WriteAdminCrawlerLog("상품명 수집 종료");

            StopGetAdminData("상품명 수집이 완료 되었습니다.");
            ShowTab2ProgragssIndicator(false);

            //ShowThreadMessageBox("상품명 수집이 완료 되었습니다.");
        }

        #endregion

        #region 3. 상품명 입력

        private void TxtAdminIdTab2_Enter(object sender, EventArgs e)
        {
            if (txtAdminIdTab2.Text == adminIdText)
            {
                txtAdminIdTab2.Text = string.Empty;
            }
        }

        private void TxtAdminIdTab2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAdminIdTab2.Text))
            {
                txtAdminIdTab2.Text = adminIdText;
            }
        }

        /// <summary>
        /// 번역추가 파일 경로 가져오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetFileExplore_Click(object sender, EventArgs e)
        {
            txtFilePath2.Text = GetFileName();
            SetConfigData(AppConfigKeys.tab3GetFilePath.ToString(), txtFilePath2.Text);
        }

        /// <summary>
        /// 상품명 입력 실패한 상품정보 엑셀파일 생성할 경로 가져오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFaildFilePath_Click(object sender, EventArgs e)
        {
            TxtFaildFilePath.Text = GetFilePath();
            SetConfigData(AppConfigKeys.tab3FaildFilePath.ToString(), TxtFaildFilePath.Text);
        }

        /// <summary>
        /// 상품명 입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "상품명 입력 중지")
            {
                StopGetAdminAddData();
                return;
            }

            Tab3txtAdminIdTab2 = txtAdminIdTab2.Text;
            Tab3txtFilePath2 = txtFilePath2.Text;
            Tab3FaildFilePath = TxtFaildFilePath.Text;

            ClearMacroLog();

            if (Tab3txtAdminIdTab2 == adminIdText)
            {
                //StopGetAdminAddData(false);
                MessageBoxEx.Show(this, "admin 아이디를 입력하여 주십시오.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteMacroLog("동작 중지 : admin 아이디를 입력하여 주십시오.");
                return;
            }

            if (string.IsNullOrEmpty(Tab3txtFilePath2))
            {
                //StopGetAdminAddData(false);
                MessageBoxEx.Show(this, "상품명 입력할 상품정보 엑셀 파일 경로를 지정하여 주십시오.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteMacroLog("동작 중지 : 상품명 입력할 상품정보 엑셀 파일 경로를 지정하여 주십시오.");
                return;
            }

            if (!File.Exists(Tab3txtFilePath2))
            {
                //StopGetAdminAddData(false);
                MessageBoxEx.Show(this, "상품명 입력할 상품정보 엑셀 파일이 지정된 경로에 없습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteMacroLog("동작 중지 : 상품명 입력할 상품정보 엑셀 파일이 지정된 경로에 없습니다.");
                return;
            }

            if (ChkFaildProc.Checked && string.IsNullOrEmpty(Tab3FaildFilePath))
            {
                //StopGetAdminAddData(false);
                MessageBoxEx.Show(this, "입력 실패한 상품정보 엑셀파일을 생성할 경로를 지정하여 주십시오..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteMacroLog("동작 중지 : 입력 실패한 상품정보 엑셀파일을 생성할 경로를 지정하여 주십시오.");
                return;
            }

            ShowTab3ProgragssIndicator(true);

            var ts = new ThreadStart(SetAdminData);
            actionThread = new Thread(ts);
            actionThread.Start();

            btnAdd.Text = "상품명 입력 중지";
        }

        /// <summary>
        /// 현재 실행 중인 수집 및 수정 동작을 중지합니다.
        /// </summary>
        /// <returns></returns>
        private bool StopActionThread()
        {
            try
            {
                //IsStopActionThread = true;
                if (actionThread != null && actionThread.IsAlive)
                    actionThread.Abort();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 상품명 입력 동작 (함수가 길어도 리팩토링 하지 않음... 테스트 편하게 하기 위해)
        /// </summary>
        /// <returns></returns>
        private void SetAdminData()
        {
            bool iEn = false;
            bool iJp = false;
            bool iCn = false;

            // 1. 엑셀 파일 가져오기
            // 상품코드 엑셀 파일 가져오기 시작
            var fromExcel = new FromExcel();
            var err = string.Empty;
            var dt = fromExcel.OpenExcel(Tab3txtFilePath2, ref err);

            if (dt == null)
            {
                if (err.Contains("프로세스"))
                {
                    ShowThreadMessageBox($"해당 엑셀 파일이 실행 중입니다. \r\n엑셀파일을 종료하고 다시 시도 해주세요.\r\n파일명 : {Tab3txtFilePath2}");
                }
                else
                {
                    ShowThreadMessageBox($"해당 엑셀 파일을 가져오는 중 오류가 발생하였습니다. \r\n파일명 : {Tab3txtFilePath2} \r\n오류 : {err}");
                }

                StopGetAdminAddData();
                return;
            }
            else if (dt.Rows == null || dt.Rows.Count < 1)
            {
                StopGetAdminAddData();
                //ShowThreadMessageBox("엑셀 파일에 상품정보가 없습니다.");
                return;
            }

            // 데이터모델 리스트에 담기
            var goods = new List<DataModel>();

            // 판매자가 번역 제외 시킨 상품 정보
            var ignoreGoods = new List<DataModel>();

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    var dataModel = new DataModel();

                    foreach (DataColumn column in dt.Columns)
                    {
                        switch(column.ColumnName.Trim())
                        {
                            // 국문은 입력하지 않음
                            case "상품코드":
                                dataModel.GooodsCode = row[column]?.ToString() ?? string.Empty;
                                break;
                            case "영문":
                                dataModel.NameEN = row[column]?.ToString() ?? string.Empty;
                                iEn = true;
                                break;
                            case "일문":
                                dataModel.NameJP = row[column]?.ToString() ?? string.Empty;
                                iJp = true;
                                break;
                            case "중문":
                                dataModel.NameCN = row[column]?.ToString() ?? string.Empty;
                                iCn = true;
                                break;
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(dataModel.GooodsCode))
                        goods.Add(dataModel);
                }
            }
            catch (Exception e)
            {
                StopGetAdminAddData($"엑셀파일을 가져올 수 없어 상품명 입력이 중지됐습니다. \r\n엑셀파일 양식이 맞는지 확인해주세요. \r\n에러메시지 : {e.Message}");
                ShowThreadMessageBox($"엑셀파일을 가져올 수 없어 상품명 입력이 중지됐습니다. \r\n엑셀파일 양식이 맞는지 확인해주세요. \r\n에러메시지 : {e.Message}");
                return;
            }

            var action = new AdminAction(SilentMode);

            WriteMacroLog("상품코드 이용하여 admin에서 상품이름 수집 시작");
            if (!action.Start(WriteMacroLog))
            {
                StopGetAdminAddData("동작 중지 : 크롬 브라우저 실행 중 오류가 발생하였습니다.");
                WriteMacroLog("동작 중지 : 크롬 브라우저 실행 중 오류가 발생하였습니다.");
                return;
            }

            // 로그인 페이지 이동
            if (!action.SetUrl("http://ccadmin.gmarket.co.kr/GMKT.KRAdmin.Web/Admin/ForeignGoodsNameMng_Gate.aspx", WriteMacroLog))
            {
                StopGetAdminAddData("동작 중지 : G마켓 Admin 페이지 로드 중 오류가 발생하였습니다.");
                WriteMacroLog("동작 중지 : G마켓 Admin 페이지 로드 중 오류가 발생하였습니다.");
                return;
            }

            Thread.Sleep(500);
            WriteMacroLog("아이디 입력");
            // 아이디입력 ex) Global001
            if (!action.SetInputValue("loginId", Tab3txtAdminIdTab2, WriteMacroLog))
            {
                StopGetAdminAddData("동작 중지 : Admin 아이디가 입력중 오류 발생 하였습니다.");
                WriteMacroLog("동작 중지 : Admin 아이디가 입력중 오류 발생 하였습니다.");
                return;
            }

            WriteMacroLog("로그인 버튼 클릭");
            if (!action.ClickButton("btnLogin", WriteMacroLog))
            {
                WriteMacroLog("동작 중지 : 로그인 버튼 클릭 중 오류가 발생 하였습니다.");
                ShowTab3ProgragssIndicator(false);
                return;
            }

            WriteMacroLog("로그인 완료까지 대기 중");
            // 로그인 완료 시 까지 기다리기
            string mainPage = "http://ccadmin.gmarket.co.kr/GMKT.KRAdmin.Web/B_GOODS_TOTAL_MNG/ForeignGoodsNameMng.aspx";
            for (int roopCnt = 1; roopCnt <= 5; roopCnt++)
            {
                Thread.Sleep(300);

                string currentPage = action.GetUrl();
                if (currentPage == mainPage)
                {
                    WriteMacroLog("로그인 성공");
                    break;
                }

                if (roopCnt == 5)
                {
                    StopGetAdminAddData("동작 중지 : 로그인 실패로 동작이 중지 됩니다.");
                    WriteMacroLog("동작 중지 : 로그인 실패로 동작이 중지 됩니다.");
                    return;
                }
            }

            var AddFaildGoods = new List<DataModel>();

            int attempt = 0;
            int success = 0;

            //== 루프 시작
            foreach (var dataModel in goods)
            {
                if (string.IsNullOrEmpty(dataModel.GooodsCode))
                    continue;

                attempt++;
                WriteMacroLog($"{dataModel.GooodsCode} 상품명 입력 시작");

                Thread.Sleep(400);
                //상품코드 입력
                if (!action.SetInputValue("txtGdNo", dataModel.GooodsCode, WriteMacroLog))
                {
                    StopGetAdminAddData("동작 중지 : 상품코드 입력 중 오류가 발생하였습니다.");
                    WriteMacroLog("동작 중지 : 상품코드 입력 중 오류가 발생하였습니다.");
                    WriteMacroLog($"업로드 시도 {attempt} 건 / 성공 {success} 건 / 실패 {AddFaildGoods.Count} 건 / 번역 미동의 {ignoreGoods.Count} 건");
                    action.Close();
                    return;
                }

                //검색 버튼 클릭
                if (!action.ClickButton("btnSearch", WriteMacroLog))
                {
                    StopGetAdminAddData("동작 중지 : 검색 버튼 클릭 중 오류가 발생하였습니다.");
                    WriteMacroLog("동작 중지 : 검색 버튼 클릭 중 오류가 발생하였습니다.");
                    WriteMacroLog($"업로드 시도 {attempt} 건 / 성공 {success} 건 / 실패 {AddFaildGoods.Count} 건 / 번역 미동의 {ignoreGoods.Count} 건");
                    action.Close();
                    return;
                }

                Thread.Sleep(600);

                WriteMacroLog("테이블 클릭");
                if (!action.DoubleClickGoodsTableRow("//*[@id='__grid_grid']/div[2]/table/tbody/tr[2]/td[1]", WriteMacroLog))
                {
                    AddFaildGoods.Add(new DataModel() { GooodsCode = dataModel.GooodsCode });
                    WriteMacroLog("Sold out 상품 발견 : Sold out 상품이 발견 되어 다음 상품 입력으로 넘어갑니다.");
                    continue;
                }

                Thread.Sleep(600);

                // 상품명 입력 : 없는 상품명은 입력하지 않음
                //영문 상품명 추가
                if (iEn && !string.IsNullOrEmpty(dataModel.NameEN))
                {
                    action.SetInputValue("txtGdEngNm", dataModel.NameEN, WriteMacroLog);
                    WriteMacroLog("영문 상품명 입력");
                }

                // 중문 상품명 추가
                if (iCn && !string.IsNullOrEmpty(dataModel.NameCN))
                {
                    action.SetInputValue("txtGdNm_Cn", dataModel.NameCN, WriteMacroLog);
                    WriteMacroLog("중문 상품명 입력");
                }

                // 일문 상품명 추가
                if (iJp && !string.IsNullOrEmpty(dataModel.NameJP))
                {
                    action.SetInputValue("txtGdNm_Jp", dataModel.NameJP, WriteMacroLog);
                    WriteMacroLog("일문 상품명 입력");
                }

                //수정 버튼 클릭
                if (!action.ClickButton("btnUpdate", WriteMacroLog))
                {
                    AddFaildGoods.Add(new DataModel() { GooodsCode = dataModel.GooodsCode });
                    WriteMacroLog($"{dataModel.GooodsCode} 상품코드 상품명 입력 실패 다음 상품명을 입력합니다");
                    continue;
                }

                Thread.Sleep(600);

                WriteMacroLog("alert 창 닫기");
                if (action.CloseAlertMessage(out string alertMessage, WriteMacroLog))
                {
                    // 완료
                    WriteMacroLog(alertMessage);
                }
                else
                {
                    // alert창이 안닫히면 다음동작이 진행이 되지 않으므로 어쩔 수 없이 종료
                    //ShowThreadMessageBox($"오류 발생으로 인하여 동작을 중지 합니다. \r\n-마지막 동작 상품코드 : {DoneGoodsCode}");
                    StopGetAdminAddData($"오류 발생으로 인하여 동작을 중지 합니다. \r\n-마지막 동작 상품코드 : {DoneGoodsCode}");
                    WriteMacroLog($"{dataModel.GooodsCode} alert 메세지창 클릭 동작이 실패하여 동작 중지합니다.");
                    WriteMacroLog($"업로드 시도 {attempt} 건 / 성공 {success} 건 / 실패 {AddFaildGoods.Count} 건 / 번역 미동의 {ignoreGoods.Count} 건");
                    return;
                }

                // 번역 동의가 되지 않은 상품 담기 : 해당 판매자는 번역동의가 되지 않았습니다.
                if (alertMessage.Contains("번역동의가"))
                {
                    ignoreGoods.Add(new DataModel() { GooodsCode = dataModel.GooodsCode });
                    WriteMacroLog($"업로드 시도 {attempt} 건 / 성공 {success} 건 / 실패 {AddFaildGoods.Count} 건 / 번역 미동의 {ignoreGoods.Count} 건");
                }
                else
                {
                    success++;
                    WriteMacroLog($"업로드 시도 {attempt} 건 / 성공 {success} 건 / 실패 {AddFaildGoods.Count} 건 / 번역 미동의 {ignoreGoods.Count} 건");
                }

                // 완료 된 것까지 저장 하여 중지버튼 클릭 시 표시해주기
                DoneGoodsCode = dataModel.GooodsCode;

                Thread.Sleep(300);

            } //== 루프 종료

            // 입력 성공했는지 검사
            if (ChkFaildProc.Checked)
            {
                Thread.Sleep(1000);
                foreach (var dataModel in goods)
                {
                    if (string.IsNullOrEmpty(dataModel.GooodsCode))
                        continue;

                    // 번역 동의 되지 않은 상품 건너띄기
                    if (ignoreGoods.Any(g => g.GooodsCode == dataModel.GooodsCode))
                        continue;

                    WriteMacroLog($"[{dataModel.GooodsCode}] 상품명 가져오기 시작");

                    WriteMacroLog("상품코드 입력");
                    if (!action.SetInputValue("txtGdNo", dataModel.GooodsCode, WriteMacroLog))
                    {
                        WriteMacroLog("동작 중지 : 상품코드 입력 실패.");
                        continue;
                    }

                    WriteMacroLog("검색 버튼 클릭");
                    if (!action.ClickButton("btnSearch", WriteAdminCrawlerLog))
                    {
                        WriteMacroLog("검색버튼 클릭 중 오류가 발생되었습니다.");
                        continue;
                    }

                    Thread.Sleep(600);

                    WriteMacroLog("테이블 클릭");
                    if (!action.DoubleClickGoodsTableRow("//*[@id='__grid_grid']/div[2]/table/tbody/tr[2]/td[1]", WriteAdminCrawlerLog))
                    {
                        StopGetAdminAddData("-상품코드 테이블 클릭 중 오류가 발생되었습니다.");
                        WriteMacroLog("-상품코드 테이블 클릭 중 오류가 발생되었습니다.");
                        continue;
                    }

                    Thread.Sleep(500);

                    // 영문의 경우 늦게 뜨기 때문에 일단 검사하지 않습니다...
                    //if (iEn && !string.IsNullOrEmpty(dataModel.NameEN) && dataModel.NameEN != action.GetInputValue("txtGdEngNm", WriteMacroLog))
                    //{
                    //    dataModel.NameKOR = action.GetInputValue("txtGdNm", WriteMacroLog);
                    //    AddFaildGoods.Add(new DataModel() { GooodsCode = dataModel.GooodsCode });
                    //    continue;
                    //}
                    if (iCn && !string.IsNullOrEmpty(dataModel.NameCN) && dataModel.NameCN != action.GetInputValue("txtGdNm_Cn", WriteMacroLog))
                    {
                        dataModel.NameKOR = action.GetInputValue("txtGdNm", WriteMacroLog);
                        AddFaildGoods.Add(new DataModel() { GooodsCode = dataModel.GooodsCode });
                        continue;
                    }
                    if (iJp && !string.IsNullOrEmpty(dataModel.NameJP) && dataModel.NameJP != action.GetInputValue("txtGdNm_Jp", WriteMacroLog))
                    {
                        dataModel.NameKOR = action.GetInputValue("txtGdNm", WriteMacroLog);
                        AddFaildGoods.Add(new DataModel() { GooodsCode = dataModel.GooodsCode });
                        continue;
                    }

                } //== 루프 종료

                if (AddFaildGoods.Count > 0)
                {
                    //중복제거
                    AddFaildGoods = AddFaildGoods.GroupBy(g => g.GooodsCode)?.Select(s => s.FirstOrDefault())?.ToList();
                    WriteMacroLog($"상품명 수정 실패 {AddFaildGoods.Count}건이 발생하여 엑셀파일을 생성합니다.");

                    string fileName = $"상품명입력실패_{DateTime.Now.ToString("yyyy_MM_dd_HHmmss")}";
                    CreateExcel<List<DataModel>>(AddFaildGoods, Tab3FaildFilePath, fileName, WriteMacroLog);
                }
                else
                {
                    WriteMacroLog($"상품명 수정이 실패없이 모두 완료 되었습니다.");
                }
            }

            if (ignoreGoods.Count > 0)
            {
                // 중복제거
                ignoreGoods = ignoreGoods.GroupBy(g => g.GooodsCode)?.Select(s => s.FirstOrDefault())?.ToList();
                WriteMacroLog($"번역 미동의 된 상품정보가 발견 되었습니다.");
                string fileName = $"번역미동의상품목록_{DateTime.Now.ToString("yyyy_MM_dd_HHmmss")}";
                CreateExcel<List<DataModel>>(ignoreGoods, Tab3FaildFilePath, fileName, WriteMacroLog);
            }

            action.Close();
            StopGetAdminAddData("상품명 수정이 완료 되었습니다.");
            WriteMacroLog($"업로드 시도 {attempt} 건 / 성공 {success} 건 / 실패 {AddFaildGoods.Count} 건 / 번역 미동의 {ignoreGoods.Count} 건");
            //ShowThreadMessageBox("상품명 수정이 완료 되었습니다.");
        }

        #endregion

        #region 파일 경로 가져오기

        /// <summary>
        /// 파일 이름 없이 경로만 가져옵니다.
        /// </summary>
        /// <returns></returns>
        private string GetFilePath()
        {
            string resultFilePath = string.Empty;

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    resultFilePath = fbd.SelectedPath;
                }
            }

            return resultFilePath;
        }

        /// <summary>
        /// 파일 경로 + 이름 가져오기
        /// </summary>
        /// <returns></returns>
        private string GetFileName()
        {
            string resultFileName = string.Empty;

            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        resultFileName = ofd.FileName;
                    }
                    catch { }
                }
            }

            return resultFileName;
        }

        #endregion

        #region 로그 출력

        /// <summary>
        /// 상품 코드 가져오는 동작 로그 출력
        /// </summary>
        /// <param name="text"></param>
        private void WriteGetGoodsCodeLog(string text)
        {
            if (lvCrawlerLog.InvokeRequired)
            {
                var ci = new Control_Invoker_ParamTxt(WriteGetGoodsCodeLog);
                this.BeginInvoke(ci, text);
            }
            else
            {
                string log = $"- {text}";
                lvCrawlerLog.Items.Add(new ListViewItem(log));
            }
        }

        /// <summary>
        /// 상품 코드 가져오는 동작 로그 초기화
        /// </summary>
        private void ClearGetGoodsCodeLog()
        {
            if (lvCrawlerLog.InvokeRequired)
            {
                var ci = new Control_Invoker(ClearGetGoodsCodeLog);
                this.BeginInvoke(ci, null);
            }
            else
            {
                lvCrawlerLog.Items.Clear();
            }
        }

        /// <summary>
        /// Admin 상품명 수집 동작 로그 출력
        /// </summary>
        /// <param name="text"></param>
        private void WriteAdminCrawlerLog(string text)
        {
            if (lvAdminCrawlerLog.InvokeRequired)
            {
                var ci = new Control_Invoker_ParamTxt(WriteAdminCrawlerLog);
                this.BeginInvoke(ci, text);
            }
            else
            {
                string log = $"- {text}";
                lvAdminCrawlerLog.Items.Add(new ListViewItem(log));
            }
        }

        /// <summary>
        /// Admin에 상품명 수집 동작 로그 초기화
        /// </summary>
        private void ClearAdminCrawlerLog()
        {
            if (lvAdminCrawlerLog.InvokeRequired)
            {
                var ci = new Control_Invoker(ClearAdminCrawlerLog);
                this.BeginInvoke(ci, null);
            }
            else
            {
                lvAdminCrawlerLog.Items.Clear();
            }
        }

        /// <summary>
        /// Admin에 상품명 수정 동작 로그 출력
        /// </summary>
        /// <param name="text"></param>
        private void WriteMacroLog(string text)
        {
            if (lvMacroLog.InvokeRequired)
            {
                var ci = new Control_Invoker_ParamTxt(WriteMacroLog);
                this.BeginInvoke(ci, text);
            }
            else
            {
                string log = $"- {text}";
                lvMacroLog.Items.Add(new ListViewItem(log));
            }
        }

        /// <summary>
        /// Admin에 상품명 입력 동작 로그 초기화
        /// </summary>
        private void ClearMacroLog()
        {
            if (lvMacroLog.InvokeRequired)
            {
                var ci = new Control_Invoker(ClearMacroLog);
                this.BeginInvoke(ci, null);
            }
            else
            {
                lvMacroLog.Items.Clear();
            }
        }

        #endregion

        #region 프로그래스 인디케이터

        private void ShowTab1ProgragssIndicator(bool isShow)
        {
            if (LblTabActionText.InvokeRequired)
            {
                var ci = new Control_Invoker_ParamBool(ShowTab1ProgragssIndicator);
                this.BeginInvoke(ci, isShow);
            }
            else
            {
                LblTabActionText.Visible = isShow;
                PiTab1.Visible = isShow;
                
                if (CboFileNameType.SelectedIndex == 1)
                    TxtFileName.Enabled = !isShow;
                CboFileNameType.Enabled = !isShow;
                txtUrl.Enabled = !isShow;
                cboGetType.Enabled = !isShow;
                nudStart.Enabled = !isShow;
                nudEnd.Enabled = !isShow;
                btnGetFile.Enabled = !isShow;
                tabCrawler.UseWaitCursor = isShow;

                //tabCrawler.Enabled = !isShow;
                tabAdminCrawler.Enabled = !isShow;
                tabMacro.Enabled = !isShow;
            }
        }

        private void ShowTab2ProgragssIndicator(bool isShow)
        {
            if (LblTab1ActionText.InvokeRequired)
            {
                var ci = new Control_Invoker_ParamBool(ShowTab2ProgragssIndicator);
                this.BeginInvoke(ci, isShow);
            }
            else
            {
                LblTab1ActionText.Visible = isShow;
                PiTab2.Visible = isShow;
                tabAdminCrawler.UseWaitCursor = isShow;

                if (CboFileNameType1.SelectedIndex == 1)
                    TxtFileName1.Enabled = !isShow;
                CboFileNameType1.Enabled = !isShow;
                txtAdminIdTab1.Enabled = !isShow;
                BtnGetGoodsCodeFileExplore1.Enabled = !isShow;
                BtnGetGoodsNameFilePath1.Enabled = !isShow;

                tabCrawler.Enabled = !isShow;
                //tabAdminCrawler.Enabled = !isShow;
                tabMacro.Enabled = !isShow;
            }
        }

        private void ShowTab3ProgragssIndicator(bool isShow)
        {
            if (LblTab2ActionText.InvokeRequired)
            {
                var ci = new Control_Invoker_ParamBool(ShowTab3ProgragssIndicator);
                this.BeginInvoke(ci, isShow);
            }
            else
            {
                LblTab2ActionText.Visible = isShow;
                PiTab3.Visible = isShow;
                tabMacro.UseWaitCursor = isShow;

                txtAdminIdTab2.Enabled = !isShow;
                btnGetFileExplore.Enabled = !isShow;
                BtnFaildFilePath.Enabled = !isShow;
                ChkFaildProc.Enabled = !isShow;

                tabCrawler.Enabled = !isShow;
                tabAdminCrawler.Enabled = !isShow;
                //tabMacro.Enabled = !isShow;
            }
        }

        #endregion

        #region Excel

        /// <summary>
        /// Excel파일 생성
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        private bool CreateExcel<T>(T dataList, string filePath, string fileName, Action<string> logProc)
        {
            try
            {
                var excel = new ToExcel();
                var streamData = excel.DataToExcel<T>(dataList, fileName);

                File.WriteAllBytes($"{filePath}\\{fileName}.xlsx", streamData);

                return true;
            }
            catch (Exception e)
            {
                logProc($"엑셀파일 생성 중 오류 발생 : {e.Message}");
                return false;
            }
        }

        #endregion

        #region App.config 수정 / 가져오기

        /// <summary>
        /// App.config 파일 설정 데이터 가져오기
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetConfigData(string key)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                string value = ConfigurationManager.AppSettings[key];
                return value;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// App.config 파일 설정 데이터 수정
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool SetConfigData(string  key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings[key].Value = value;

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(key);

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        private void BtnClearUrl_Click(object sender, EventArgs e)
        {
            txtUrl.Text = urlText;
            BtnClearUrl.Focus();
        }

        private void TxtFileName_Enter(object sender, EventArgs e)
        {
            if (TxtFileName.Text == fileName)
            {
                TxtFileName.Text = string.Empty;
            }
        }

        private void TxtFileName1_Enter(object sender, EventArgs e)
        {
            if (TxtFileName1.Text == fileName)
            {
                TxtFileName1.Text = string.Empty;
            }
        }

        private void TxtFileName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtFileName.Text))
            {
                if (!TxtFileName.ReadOnly)
                    TxtFileName.Text = fileName;
            }
            else
            {
                SetConfigData(AppConfigKeys.tab1FileName.ToString(), TxtFileName.Text);
            }
        }

        private void TxtFileName1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtFileName1.Text))
            {
                if (!TxtFileName1.ReadOnly)
                    TxtFileName1.Text = fileName;
            }
            else
            {
                SetConfigData(AppConfigKeys.tab2FileName.ToString(), TxtFileName1.Text);
            }
        }

        private void CboFileNameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtFileName.ReadOnly = (CboFileNameType.SelectedIndex == 0);

            if (CboFileNameType.SelectedIndex == 0)
            {
                TxtFileName.Text = string.Empty;
                SetConfigData(AppConfigKeys.tab1FileName.ToString(), TxtFileName.Text);
            }
            else
            {
                TxtFileName.Text = fileName;
            }

            SetConfigData(AppConfigKeys.tab1FileNameType.ToString(), CboFileNameType.SelectedIndex.ToString());
        }

        private void CboFileNameType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtFileName1.ReadOnly = (CboFileNameType1.SelectedIndex == 0);

            if (CboFileNameType1.SelectedIndex == 0)
            {
                TxtFileName1.Text = string.Empty;
                SetConfigData(AppConfigKeys.tab2FileName.ToString(), TxtFileName1.Text);
            }
            else
            {
                TxtFileName1.Text = fileName;
            }

            SetConfigData(AppConfigKeys.tab2FileNameType.ToString(), CboFileNameType1.SelectedIndex.ToString());
        }
    }

    public enum AppConfigKeys
    {
        /// <summary>
        /// 크롬 실행 시 화면에 표시 여부 설정
        /// </summary>
        isSilentMode,

        /// <summary>
        /// 상품코드 수집 탭 - 파일 생성 경로
        /// </summary>
        tab1SetFilePath,

        /// <summary>
        /// 상품명 수집 탭 - 상품코드 정보가 있는 엑셀파일 경로
        /// </summary>
        tab2GetFilePath,

        /// <summary>
        /// 상품코드 수집 탭 - 생성 파일이름 지정 타입
        /// </summary>
        tab1FileNameType,

        /// <summary>
        /// 상품명 수집 탭 - 생성 파일이름
        /// </summary>
        tab2FileNameType,

        /// <summary>
        /// 상품코드 수집 탭 - 생성 파일이름 지정 타입
        /// </summary>
        tab1FileName,

        /// <summary>
        /// 상품명 수집 탭 - 생성 파일이름 
        /// </summary>
        tab2FileName,

        /// <summary>
        /// 상품명 수집 탭 - 상품명 수집 된 엑셀파일 생성 경로
        /// </summary>
        tab2SetFilePath,

        /// <summary>
        /// 상품명 입력 탭 - 상품코드 & 상품명 수집 된 엑셀파일 경로
        /// </summary>
        tab3GetFilePath,

        /// <summary>
        /// 상품명 입력 탭 - 상품명 입력 중 실패한 상품정보 엑셀 생성할 경로
        /// </summary>
        tab3FaildFilePath,
    }
}