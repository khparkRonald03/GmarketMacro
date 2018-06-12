using BeautifulWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebController;

namespace GmarketMacro
{
    public class CommonAction
    {
        protected Controller webController = null;

        public CommonAction(bool isSilent = true)
        {
            webController = new Controller(isSilent);
        }

        ~CommonAction()
        {
            Close();
        }

        #region 시작 / 종료

        /// <summary>
        /// 드라이브 실행
        /// </summary>
        /// <param name="logFunc">로그 출력 함수</param>
        /// <returns></returns>
        public bool Start(Action<string> logFunc)
        {
            var startResult = webController.Start();
            if (!startResult.ResultFlag)
                logFunc(startResult.Err);

            return startResult.ResultValue;
        }

        /// <summary>
        /// 드라이브 종료
        /// </summary>
        /// <param name="logFunc">로그 출력 함수</param>
        /// <returns></returns>
        public bool Close()
        {
            if (webController != null)
            {
                var closeResult = webController.Close();
                return closeResult.ResultValue;
            }
            
            return true;
        }

        #endregion

        #region 공통

        /// <summary>
        /// Url 초기화
        /// </summary>
        /// <param name="url"></param>
        /// <param name="logFunc">로그 출력 함수</param>
        /// <returns></returns>
        public bool SetUrl(string url, Action<string> logFunc)
        {
            var setUrlResult = webController.SetUrl(url);
            if (!setUrlResult.ResultFlag)
                logFunc(setUrlResult.Err);

            return setUrlResult.ResultValue;
        }

        public bool SetLanguage(Action<string> logFunc)
        {
            var clickTagResult = webController.ClickTag(ElementsSelectType.XPath, "//*[@id='utill']/div/ul[1]/li[2]/a");
            if (!clickTagResult.ResultFlag)
                logFunc(clickTagResult.Err);

            return clickTagResult.ResultValue;
        }

        /// <summary>
        /// 현재 Url 가져오기
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            var getUrlResult = webController.GetUrl();

            return getUrlResult.ResultValue;
        }

        #endregion

        #region 상품코드 가져오기

        /// <summary>
        /// 페이지로 설정 후 상품코드 가져오기
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected List<DataModel> GetPageTypeGoods(int start, int end, List<XPathModel> xPathModels, Action<string> logFunc)
        {
            var result = new List<DataModel>();
            
            for (int cur = start; cur <= end; cur++)
            {
                // 0. 첫로드 시 1페이지로 표시 되고 1페이지는 페이지 이동 필요 없음
                if (start == 1 && cur == start)
                {
                    Thread.Sleep(300);
                    logFunc($"{cur} 페이지 상품코드 가져오기 시작");
                    var goodsCode = GetAllGoodsCodeTableType(xPathModels, logFunc);
                    result.AddRange(goodsCode);
                    logFunc($"{goodsCode.Count} 개 추가 수집 완료 / 현재 총 {result.Count} 개 수집");
                    continue;
                }

                // 1. 페이지 이동
                Thread.Sleep(200);
                logFunc($"{cur} 페이지 이동");
                if (NavigationPaging(cur, logFunc))
                {
                    logFunc($"{cur} 페이지 이동 후 상품코드 가져오기 시작");

                    // 2. 상품코드 가져오기
                    Thread.Sleep(200);
                    var goodsCodes = GetAllGoodsCodeTableType(xPathModels, logFunc);
                    result.AddRange(goodsCodes);

                    logFunc($"{goodsCodes.Count} 개 추가 수집 완료 / 현재 총 {result.Count} 개 수집");
                }
                else
                {
                    logFunc($"동작 종료 : {cur} 페이지가 없어 수집 중단 됩니다.");
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 갯수로 설정 후 상품코드 가져오기
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <param name="xPathModels"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        protected List<DataModel> GetNumTypeGoods(int startNum, int endNum, List<XPathModel> xPathModels, Action<string> logFunc)
        {
            var result = new List<DataModel>();

            // 페이지 전체 갯수 가져오기 (페이지 마지막 갯수로 페이지의 마지막 상품코드 기준 잡기)
            var goodsCodes = GetAllGoodsCodeTableType(xPathModels, logFunc);
            if (goodsCodes == null || goodsCodes.Count <= 0)
            {
                logFunc("동작중지 : 페이지 전체 갯수 체크 중 오류가 발생하였습니다.");
                return result;
            }
            int pageTotalCount = goodsCodes.Count;

            // 갯수를 페이지로 계산해서 페이지를 지정하고 
            int startPage = startNum / pageTotalCount;
            startPage = startPage == 0 ? 1 : startPage;
            
            decimal endPageTmp = Math.Ceiling((decimal)endNum / (decimal)pageTotalCount);
            int endPage = (int)endPageTmp;

            //int startIndex = startNum % pageTotalCount; // X 이거 왜 했지? 기억이 안나 일단 주석처리..
            int endIndex = endNum % pageTotalCount;

            // 맞는 xPath찾기 xPath가 없다면 리턴
            var xPathModel = GetXPathModel(xPathModels);
            if (xPathModel == null)
                return result;

            // 특이케이스 처리
            if (xPathModel.Alias == "type3_1")
            {
                result = GetGoods_PC_Alias_type3_1(startNum, endNum, xPathModel, logFunc);
                return result;
            }

            if (xPathModel.Alias == "type2_2")
            {
                result = GetGoods_Mobile_Alias_type2_2(startNum, endNum, xPathModel, logFunc);
                return result;
            }
            

            // 1 ~ 1 페이지로 설정 됐다면 첫페이지만 가져오기
            if (startPage == endPage)
            {
                for (int curNum = startNum; curNum <= endNum; curNum++)
                {
                    string goodsText = GetIndexGoodsCodeTableType(curNum, xPathModel, logFunc);
                    if (!string.IsNullOrEmpty(goodsText))
                    {
                        result.Add(new DataModel() { GooodsCode = goodsText });
                    }
                }

                return result;
            }

            // 첫번째와 마지막 페이지는 갯수로 가져오고
            // (첫번째 : 지정번호 ~ 60)
            // (마지막 : 0 ~ 지정 번호)
            // 중간 페이지들은 모두 가져오기
            for (int curPage = startPage; curPage <= endPage; curPage++)
            {
                // 첫번째 페이지 (상품 코드 하나씩 가져오기)
                if (curPage == startPage)
                {
                    for (int curNum = startNum; curNum <= pageTotalCount; curNum++)
                    {
                        string goodsText = GetIndexGoodsCodeTableType(curNum, xPathModel, logFunc);
                        if (!string.IsNullOrEmpty(goodsText))
                        {
                            result.Add(new DataModel() { GooodsCode = goodsText });
                        }
                    }

                    continue;
                }

                // 페이지 이동
                if (!NavigationPaging(curPage, logFunc))
                {
                    logFunc($"동작 종료 : {curPage} 페이지가 없어 수집 중단 됩니다.");
                    break;
                }

                // 마지막 페이지 (상품 코드 하나씩 가져오기)
                if (curPage == endPage)
                {
                    for (int curNum = 1; curNum <= endIndex; curNum++)
                    {
                        string goodsText = GetIndexGoodsCodeTableType(curNum, xPathModel, logFunc);
                        if (!string.IsNullOrEmpty(goodsText))
                        {
                            result.Add(new DataModel() { GooodsCode = goodsText });
                        }
                    }

                    continue;
                }

                // 중간 페이지들 (한페이지 전체 가져오기)
                result.AddRange(GetAllGoodsCodeTableType(xPathModels, logFunc));
            }

            return result;
        }

        #endregion

        #region 상품 코드 가져오기 동작 함수

        /// <summary>
        /// 페이지 이동
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        private bool NavigationPaging(int pageIndex, Action<string> logFunc)
        {
            // 1차 시도 : 페이징 버튼을 클릭하여 페이지를 이동 (PC 웹 페이지)
            var javascript = $"SearchListPage.GoPage({pageIndex})";
            var pagingClickJSResult = webController.ExecuteJS(javascript);
            if (pagingClickJSResult.ResultFlag)
            {
                // 페이지 이동 완료 여부 검사
                var currentPage = webController.FindElement(ElementsSelectType.XPath, "//*[@id='searchlist_paging']/span");
                if (currentPage == null || currentPage.Text != pageIndex.ToString())
                {
                    pagingClickJSResult.Err = "페이지 이동 실패";
                }

                return pagingClickJSResult.ResultFlag;
            }

            // 2차 시도 : 이동 페이지 값 입력 후 페이지 이동 버튼을 클릭하여 이동 (PC 웹 페이지)
            var SetTextInputResult = webController.SetTextInputTag(ElementsSelectType.Id, "page", pageIndex.ToString());
            if (SetTextInputResult.ResultFlag)
            {
                logFunc("페이지 이동 버튼을 클릭합니다.");
                var clickTagResult = webController.ClickTag(ElementsSelectType.XPath, "//*[@id='searchlist_paging']/div/button");
                if (!clickTagResult.ResultFlag)
                {
                    logFunc($"페이지 이동 버튼 클릭 중 오류 발생 : {clickTagResult.Err}");
                }

                // 페이지 이동 완료 여부 검사
                var currentPage = webController.FindElement(ElementsSelectType.XPath, "//*[@id='searchlist_paging']/span");
                if (currentPage == null || currentPage.Text != pageIndex.ToString())
                {
                    clickTagResult.Err = "페이지 이동 실패";
                }

                return clickTagResult.ResultFlag;
            }

            // 3차 시도 : 페이징 버튼을 클릭하여 페이지를 이동 (모바일 웹 페이지)
            var mJavascript = $"goPage({pageIndex})";
            var mPagingClickJSResult = webController.ExecuteJS(mJavascript);
            if (mPagingClickJSResult.ResultFlag)
            {
                Thread.Sleep(300);
                var xPath = $"//*[@id='paginate']/a[contains(@class,'selected')]";
                var currentPage = webController.FindElements(ElementsSelectType.XPath, xPath);
                if (currentPage == null || !currentPage.Any(t => t.Text.Contains(pageIndex.ToString())))
                {
                    mPagingClickJSResult.Err = "페이지 이동 실패";
                }

                return mPagingClickJSResult.ResultFlag;
            }
            
            logFunc($"페이징 태그를 찾지 못하였습니다.");
            return false;
        }

        /// <summary>
        /// 페이징 된 현재 페이지 값 가져오기 (페이지 인덱스가 리프레쉬 안되는 관계로 안씀)
        /// </summary>
        /// <returns></returns>
        private string GetCurrentPageNo()
        {
            var getValueInputTagResult = webController.GetValueInputTag(ElementsSelectType.Id, "hdPageNo");

            return getValueInputTagResult.ResultValue;
        }

        /// <summary>
        /// 페이지의 전체 상품코드 리스트 가져오기
        /// </summary>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        private List<DataModel> GetAllGoodsCodeTableType(List<XPathModel> xPathModels, Action<string> logFunc)
        {
            var result = new List<DataModel>();
            var getPageSourceResult = webController.GetPageSource();
            if (!getPageSourceResult.ResultFlag)
            {
                logFunc($"페이지소스 수집 중 오류 발생 : {getPageSourceResult.Err}");
                return result;
            }

            try
            {
                var nodes = GetSelectNodes(getPageSourceResult.ResultValue, xPathModels);

                List<string> tmpList = nodes.Where(n => n.Href.ToLower().Contains("goodscode"))?.Select(n => GetGoodsCode(n.Href))?.Where(n => n != string.Empty)?.Distinct()?.ToList();
                foreach (var goods in tmpList)
                {
                    result.Add(new DataModel() { GooodsCode = goods });
                }

            }
            catch (Exception e)
            {
                string err = $" 페이지의 전체 상품코드 수집 중 오류가 발생 : {e.Message}";
                logFunc(err);
            }

            return result;
        }
        
        /// <summary>
        /// 선택 된 전체 노드 가져오기
        /// </summary>
        /// <param name="pageSource"></param>
        /// <param name="xPathModels"></param>
        /// <returns></returns>
        private IEnumerable<BeautifulNode> GetSelectNodes(string pageSource, List<XPathModel> xPathModels)
        {
            // 설명 : 애매한 상황들 때문에 어쩔 수 없이 모든 xPath로 노드 수집 후 상품코드가 가장 많이 수집 되는 노드 리턴

            IEnumerable<BeautifulNode> result = new List<BeautifulNode>();
            List<IEnumerable<BeautifulNode>> resultTmp = new List<IEnumerable<BeautifulNode>>();

            foreach (var xPathModel in xPathModels)
            {
                if (string.IsNullOrEmpty(xPathModel.PageNodeXPath))
                    continue;

                var page = new BeautifulPage(pageSource);
                var nodes = page.SelectNodes(xPathModel.PageNodeXPath);

                resultTmp.Add(nodes);
            }

            // goodscode 포함 돼 있는 노드들 가져오기
            var resultTmp2 = resultTmp.Select(n => n.Where(nc => nc.Href.ToLower().Contains("goodscode")));

            // goodscode 가 가장 많이 포함된 노드 반환 (애매하게 몇개씩 껴서 수집 되는 경우가 있어 제일 많은 갯수가 수집된 노드를 반환)
            foreach (var node in resultTmp2)
            {
                if (result.Count() < node.Count())
                {
                    result = node;
                }
            }

            return result;
        }

        /// <summary>
        /// 페이지의 특정 순서 상품코드 가져오기
        /// </summary>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        private string GetIndexGoodsCodeTableType(int codeNum, XPathModel xPathModel, Action<string> logFunc)
        {
            var getPageSourceResult = webController.GetPageSource();
            if (!getPageSourceResult.ResultFlag)
            {
                logFunc(getPageSourceResult.Err);
                return string.Empty;
            }

            string result = string.Empty;

            try
            {
                var page = new BeautifulPage(getPageSourceResult.ResultValue);

                BeautifulNode node = null;
                string xPathText = string.Empty;

                // PC 웹의 경우 html 구조가 여러가지여서 여기서 상황별 대처가 필요함. 
                // (현재는 파라미터가 2, 3 개인 케이스가 한건씩 밖에 없어서 이렇게 처리 했는데 다른 케이스가 발견 되면 수정 필요...)
                switch (xPathModel.ParamCount)
                {
                    case 1:
                        xPathText = string.Format(xPathModel.NumNodeXPath, codeNum);
                        node = page.SelectNode(xPathText);
                        break;
                    case 2:
                        if (xPathModel.ParamsStartNum.Count == 2 && xPathModel.ColLength > 0)
                        {
                            int startDepth0 = xPathModel.ParamsStartNum[0];  // 첫번째 파라미터 시작번호
                            int startDepth1 = xPathModel.ParamsStartNum[1];  // 두번째 파라미터 시작번호
                            int colLen = xPathModel.ColLength;               // 총 갯수

                            // 첫번째 파라미터 위치
                            decimal currentTmp = Math.Ceiling((decimal)codeNum / (decimal)colLen);
                            int param0 = (int)currentTmp;

                            // 두번째 파라미터 위치
                            int param1Tmp = codeNum % colLen;
                            int param1 = param1Tmp == 0 ? colLen : param1Tmp;

                            xPathText = string.Format(xPathModel.NumNodeXPath, param0, param1);
                        }
                        else if (xPathModel.ParamsStartNum.Count == 2 && xPathModel.ColLength == 0)
                        {
                            int startDepth0 = xPathModel.ParamsStartNum[0];  // 첫번째 파라미터 시작번호
                            int startDepth1 = xPathModel.ParamsStartNum[1];  // 두번째 파라미터 시작번호
                            int colLen = xPathModel.ColLength;               // 총 갯수

                            // 첫번째 파라미터 위치
                            decimal currentTmp = Math.Ceiling((decimal)codeNum / (decimal)colLen);
                            int param0 = (int)currentTmp;

                            // 두번째 파라미터 위치
                            int param1Tmp = codeNum % colLen;
                            int param1 = param1Tmp == 0 ? colLen : param1Tmp;

                            xPathText = string.Format(xPathModel.NumNodeXPath, param0, param1);
                        }

                        break;
                }
                
                // xPath를 가져왔다면 상품코드 가져오기
                if (!string.IsNullOrEmpty(xPathText))
                {
                    node = page.SelectNode(xPathText);
                    result = GetGoodsCode(node.Href);
                }
                
            }
            catch (Exception e)
            {
                string err = $"상품코드 가져오는 중 오류가 발생했습니다. [{e.Message}]";
                logFunc(err);
            }

            return result;
        }

        /// <summary>
        /// xPath 선택자
        /// </summary>
        /// <param name="xPathModels"></param>
        /// <returns></returns>
        private XPathModel GetXPathModel(List<XPathModel> xPathModels)
        {
            // 설명 : 애매한 상황들 때문에 어쩔 수 없이 모든 xPath로 상품코드 노드 수 수집 후 가장 노드 수 가 많은 xPath 리턴

            if (xPathModels == null || xPathModels.Count <= 0)
                return null;

            XPathModel result = null;

            var getPageSource = webController.GetPageSource();
            if (!getPageSource.ResultFlag)
                return result;

            foreach (var xPathModel in xPathModels)
            {
                if (string.IsNullOrEmpty(xPathModel.PageNodeXPath))
                    continue;

                var page = new BeautifulPage(getPageSource.ResultValue);
                var nodes = page.SelectNodes(xPathModel.PageNodeXPath);

                if (nodes != null && nodes.Count() > 0)
                {
                    xPathModel.SelectTotalNode = nodes.Where(nc => nc.Href.ToLower().Contains("goodscode"))?.Count() ?? 0;
                }
            }

            result = xPathModels.OrderByDescending(n => n.SelectTotalNode)?.FirstOrDefault();

            return result;
        }

        /// <summary>
        /// 상품코드만 추출하여 가져오기
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetGoodsCode(string url)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(url))
                return result;

            string[] textArray = url.ToLower().Split(new string[] { "goodscode=" }, StringSplitOptions.RemoveEmptyEntries);
            if (textArray.Length <= 1)
            {
                // 모바일일 경우 url이 다름 -> http://sna.gmarket.co.kr/?fcd=706600002&amp;url=http%3A%2F%2Fmg.gmarket.co.kr%2FItem%3Fgoodscode%3D189607991
                textArray = url.ToLower().Split(new string[] { "goodscode" }, StringSplitOptions.RemoveEmptyEntries);
                if (textArray.Length <= 1)
                {
                    return result;
                }
            }

            result = textArray[1];

            if (result.Contains("'"))
            {
                int endIndex = result.IndexOf("'");
                result = result.Substring(0, endIndex)?.Replace("%3d", "") ?? string.Empty;
            }
            else if (result.Contains("%3d"))
            {
                // 모바일일 경우 = 문자가  %3D 로 표시 되어 제거 (대소문자 이슈때문에 url을 소문자로 변경하여 처리하기 때문에 코드에서는 %3d 와 같이 처리)
                result = result.Replace("%3d", "");
            }
            else if (result.Contains("&amp;sid="))
            {
                var resultTmp = result.Split(new string[] { "&amp;sid=" }, StringSplitOptions.RemoveEmptyEntries);
                if (resultTmp.Length == 2)
                    result = resultTmp[0];
            }
            else if (result.Contains("&amp;"))
            {
                result = result.Replace("&amp;", "");
            }
            else if (result.Contains("&sid"))
            {
                result = result.Replace("&sid", "");
            }

            return result;
        }

        /// <summary>
        /// 특이케이스 페이지 처리 ( 타켓 url -> http://gpromotion.gmarket.co.kr/Plan/PlanView?sid=160591 )
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <param name="pageTotalCount"></param>
        /// <param name="xPathModels"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        List<DataModel> GetGoods_PC_Alias_type3_1(int startNum, int endNum, XPathModel xPathModel, Action<string> logFunc)
        {
            var result = new List<DataModel>();

            // 페이지 소스 가져오기
            var getPageSourceResult = webController.GetPageSource();
            if (!getPageSourceResult.ResultFlag)
            {
                logFunc($"페이지소스를 가져오는 중 오류 발생 : {getPageSourceResult.Err}");
                return result;
            }
            var page = new BeautifulPage(getPageSourceResult.ResultValue);

            // 시작 지점 초기화
            int curNumGroup1 = xPathModel.ParamsStartNum[0];
            int curNumGroup2 = xPathModel.ParamsStartNum[1];
            int curNumGroup3 = xPathModel.ParamsStartNum[2];

            for (int num = 1; num <= endNum; num++, curNumGroup3++)
            {
                // 우선 값 가져오기
                var xPath = string.Format(xPathModel.NumNodeXPath, curNumGroup1, curNumGroup2, curNumGroup3);
                BeautifulNode node = page.SelectNode(xPath);

                // 라인 끝났는지 체크 후 다시 수집 :  한 라인이 끝나면 다음 라인으로 옮기기 (라인은 3개짜리 4개짜리 2종류 있음)
                // 4, 5번째와 비교 해주는 이유는 3, 4번까지는 노드가 있으니 가져오고 4, 5번째 부터 null로 오기 때문
                if ((node == null || string.IsNullOrEmpty(node.Href))
                    && ((curNumGroup3 % 4) == 0 || (curNumGroup3 % 5) == 0))
                {
                    curNumGroup2++;                                 // 라인 옮기고
                    curNumGroup3 = xPathModel.ParamsStartNum[2];    // 상품순번 첫번째로 이동

                    // 다시 가져오기
                    xPath = string.Format(xPathModel.NumNodeXPath, curNumGroup1, curNumGroup2, curNumGroup3);
                    node = page.SelectNode(xPath);
                }

                // 그룹 끝났는지 체크 후 다시 수집 : 라인 수정 했는데도 null이면 현재 그룹의 상품코드가 더이상 없다고 보고 그룹 옮기기
                if (node == null || string.IsNullOrEmpty(node.Href))
                {
                    curNumGroup1++;                                 // 그룹 옮기고
                    curNumGroup2 = xPathModel.ParamsStartNum[1];    // 라인 첫번째로 이동
                    curNumGroup3 = xPathModel.ParamsStartNum[2];    // 상품순번 첫번째로 이동

                    // 다시 가져오기
                    xPath = string.Format(xPathModel.NumNodeXPath, curNumGroup1, curNumGroup2, curNumGroup3);
                    node = page.SelectNode(xPath);
                }

                // 위에서 파라미터 인덱스 증가 시켜줬는데도 null이면 더이상 상품이 없다고 보고 종료
                // 하지만 null이 아니라면 num부터 endNum 까지 계속 루프 돌아줌
                if (node == null || string.IsNullOrEmpty(node.Href))
                {
                    break;
                }

                // 상품코드 가져올 처음번호 이전 꺼는 전부 버리기
                if (num < startNum)
                    continue;

                // 상품코드 추가
                var goodsCode = GetGoodsCode(node.Href);
                result.Add(new DataModel() { GooodsCode = goodsCode });
            }

            return result;
        }

        /// <summary>
        /// 특이 케이스 페이지 처리 ( 타켓 Url -> http://mg.gmarket.co.kr/Promotion/Plan?lcId=&sid=160045 )
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <param name="xPathModel"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        List<DataModel> GetGoods_Mobile_Alias_type2_2(int startNum, int endNum, XPathModel xPathModel, Action<string> logFunc)
        {
            var result = new List<DataModel>();

            // 페이지 소스 가져오기
            var getPageSourceResult = webController.GetPageSource();
            if (!getPageSourceResult.ResultFlag)
            {
                logFunc($"페이지소스를 가져오는 중 오류 발생 : {getPageSourceResult.Err}");
                return result;
            }
            var page = new BeautifulPage(getPageSourceResult.ResultValue);

            // 시작 지점 초기화
            int curNumGroup1 = xPathModel.ParamsStartNum[0];
            int curNumGroup2 = xPathModel.ParamsStartNum[1];

            for (int num = 1; num <= endNum; num++, curNumGroup2++)
            {
                // 우선 값 가져오기
                var xPath = string.Format(xPathModel.NumNodeXPath, curNumGroup1, curNumGroup2);
                BeautifulNode node = page.SelectNode(xPath);

                // 그룹 끝났는지 체크 후 다시 수집 : 라인 수정 했는데도 null이면 현재 그룹의 상품코드가 더이상 없다고 보고 그룹 옮기기
                if (node == null || string.IsNullOrEmpty(node.Href))
                {
                    curNumGroup1++;                                 // 그룹 옮기고
                    curNumGroup2 = xPathModel.ParamsStartNum[1];    // 상품 순번 첫번째로 이동

                    // 다시 가져오기
                    xPath = string.Format(xPathModel.NumNodeXPath, curNumGroup1, curNumGroup2);
                    node = page.SelectNode(xPath);
                }

                // 위에서 파라미터 인덱스 증가 시켜줬는데도 null이면 더이상 상품이 없다고 보고 종료
                // 하지만 null이 아니라면 num부터 endNum 까지 계속 루프 돌아줌
                if (node == null || string.IsNullOrEmpty(node.Href))
                {
                    break;
                }

                // 상품코드 가져올 처음번호 이전 꺼는 전부 버리기
                if (num < startNum)
                    continue;

                // 상품코드 추가
                var goodsCode = GetGoodsCode(node.Href);
                result.Add(new DataModel() { GooodsCode = goodsCode });
            }

            return result;
        }

        #endregion 상품 코드 가져오기 동작 함수_End
    }
}
