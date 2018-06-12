namespace GmarketMacro
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGetFile = new iTalk.iTalk_Button_1();
            this.txtFilePath = new iTalk.iTalk_TextBox_Small();
            this.TxtGoodsNameFilePath1 = new iTalk.iTalk_TextBox_Small();
            this.BtnGetGoodsNameFilePath1 = new iTalk.iTalk_Button_1();
            this.TxtGoodsCodeFilePath1 = new iTalk.iTalk_TextBox_Small();
            this.BtnGetGoodsCodeFileExplore1 = new iTalk.iTalk_Button_1();
            this.txtFilePath2 = new iTalk.iTalk_TextBox_Small();
            this.btnGetFileExplore = new iTalk.iTalk_Button_1();
            this.TxtFaildFilePath = new iTalk.iTalk_TextBox_Small();
            this.BtnFaildFilePath = new iTalk.iTalk_Button_1();
            this.BtnClearUrl = new iTalk.iTalk_Button_1();
            this.iTalk_ThemeContainer = new iTalk.iTalk_ThemeContainer();
            this.iTalk_ControlBox1 = new iTalk.iTalk_ControlBox();
            this.iTalk_TabControl = new iTalk.iTalk_TabControl();
            this.tabCrawler = new System.Windows.Forms.TabPage();
            this.CBLanguage = new iTalk.iTalk_ComboBox();
            this.LblTabActionText = new iTalk.iTalk_Label();
            this.nudStart = new iTalk.iTalk_NumericUpDown();
            this.CboFileNameType = new iTalk.iTalk_ComboBox();
            this.cboGetType = new iTalk.iTalk_ComboBox();
            this.btnGetGoodsCode = new iTalk.iTalk_Button_2();
            this.iTalk_Label1 = new iTalk.iTalk_Label();
            this.TxtFileName = new iTalk.iTalk_TextBox_Small();
            this.txtUrl = new iTalk.iTalk_TextBox_Small();
            this.nudEnd = new iTalk.iTalk_NumericUpDown();
            this.iTalk_GroupBox1 = new iTalk.iTalk_GroupBox();
            this.PiTab1 = new iTalk.iTalk_ProgressIndicator();
            this.lvCrawlerLog = new iTalk.iTalk_Listview();
            this.tabAdminCrawler = new System.Windows.Forms.TabPage();
            this.CboFileNameType1 = new iTalk.iTalk_ComboBox();
            this.TxtFileName1 = new iTalk.iTalk_TextBox_Small();
            this.LblTab1ActionText = new iTalk.iTalk_Label();
            this.txtAdminIdTab1 = new iTalk.iTalk_TextBox_Small();
            this.btnAdd1 = new iTalk.iTalk_Button_2();
            this.iTalk_GroupBox3 = new iTalk.iTalk_GroupBox();
            this.PiTab2 = new iTalk.iTalk_ProgressIndicator();
            this.lvAdminCrawlerLog = new iTalk.iTalk_Listview();
            this.tabMacro = new System.Windows.Forms.TabPage();
            this.ChkFaildProc = new iTalk.iTalk_CheckBox();
            this.LblTab2ActionText = new iTalk.iTalk_Label();
            this.txtAdminIdTab2 = new iTalk.iTalk_TextBox_Small();
            this.btnAdd = new iTalk.iTalk_Button_2();
            this.iTalk_GroupBox2 = new iTalk.iTalk_GroupBox();
            this.PiTab3 = new iTalk.iTalk_ProgressIndicator();
            this.lvMacroLog = new iTalk.iTalk_Listview();
            this.iTalk_ThemeContainer.SuspendLayout();
            this.iTalk_TabControl.SuspendLayout();
            this.tabCrawler.SuspendLayout();
            this.iTalk_GroupBox1.SuspendLayout();
            this.tabAdminCrawler.SuspendLayout();
            this.iTalk_GroupBox3.SuspendLayout();
            this.tabMacro.SuspendLayout();
            this.iTalk_GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "if_meanicons_23_197211.png");
            this.imageList1.Images.SetKeyName(1, "if_meanicons_25_197209.png");
            this.imageList1.Images.SetKeyName(2, "if_list.png");
            this.imageList1.Images.SetKeyName(3, "if_shopping-cart_white.png");
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // btnGetFile
            // 
            this.btnGetFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGetFile.BackColor = System.Drawing.Color.Transparent;
            this.btnGetFile.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnGetFile.Image = null;
            this.btnGetFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetFile.Location = new System.Drawing.Point(685, 63);
            this.btnGetFile.Name = "btnGetFile";
            this.btnGetFile.Size = new System.Drawing.Size(52, 28);
            this.btnGetFile.TabIndex = 59;
            this.btnGetFile.Text = "...";
            this.btnGetFile.TextAlignment = System.Drawing.StringAlignment.Center;
            this.toolTip1.SetToolTip(this.btnGetFile, "상품코드 엑셀파일 생성 경로를 지정하세요.");
            this.btnGetFile.Click += new System.EventHandler(this.BtnGetFile_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.BackColor = System.Drawing.Color.Transparent;
            this.txtFilePath.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtFilePath.ForeColor = System.Drawing.Color.DimGray;
            this.txtFilePath.Location = new System.Drawing.Point(371, 63);
            this.txtFilePath.MaxLength = 32767;
            this.txtFilePath.Multiline = false;
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(308, 28);
            this.txtFilePath.TabIndex = 58;
            this.txtFilePath.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.toolTip1.SetToolTip(this.txtFilePath, "엑셀파일 생성 경로 입니다.");
            this.txtFilePath.UseSystemPasswordChar = false;
            // 
            // TxtGoodsNameFilePath1
            // 
            this.TxtGoodsNameFilePath1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtGoodsNameFilePath1.BackColor = System.Drawing.Color.Transparent;
            this.TxtGoodsNameFilePath1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.TxtGoodsNameFilePath1.ForeColor = System.Drawing.Color.DimGray;
            this.TxtGoodsNameFilePath1.Location = new System.Drawing.Point(371, 65);
            this.TxtGoodsNameFilePath1.MaxLength = 32767;
            this.TxtGoodsNameFilePath1.Multiline = false;
            this.TxtGoodsNameFilePath1.Name = "TxtGoodsNameFilePath1";
            this.TxtGoodsNameFilePath1.ReadOnly = true;
            this.TxtGoodsNameFilePath1.Size = new System.Drawing.Size(305, 28);
            this.TxtGoodsNameFilePath1.TabIndex = 73;
            this.TxtGoodsNameFilePath1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.toolTip1.SetToolTip(this.TxtGoodsNameFilePath1, "상품명 수집 후 엑셀 파일 생성 할 경로입니다.");
            this.TxtGoodsNameFilePath1.UseSystemPasswordChar = false;
            // 
            // BtnGetGoodsNameFilePath1
            // 
            this.BtnGetGoodsNameFilePath1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnGetGoodsNameFilePath1.BackColor = System.Drawing.Color.Transparent;
            this.BtnGetGoodsNameFilePath1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.BtnGetGoodsNameFilePath1.Image = null;
            this.BtnGetGoodsNameFilePath1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnGetGoodsNameFilePath1.Location = new System.Drawing.Point(682, 65);
            this.BtnGetGoodsNameFilePath1.Name = "BtnGetGoodsNameFilePath1";
            this.BtnGetGoodsNameFilePath1.Size = new System.Drawing.Size(55, 28);
            this.BtnGetGoodsNameFilePath1.TabIndex = 72;
            this.BtnGetGoodsNameFilePath1.Text = "...";
            this.BtnGetGoodsNameFilePath1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.toolTip1.SetToolTip(this.BtnGetGoodsNameFilePath1, "상품명 수집 후 엑셀 파일 생성 할 경로를 지정하세요.");
            this.BtnGetGoodsNameFilePath1.Click += new System.EventHandler(this.BtnGetGoodsNameFilePath1_Click);
            // 
            // TxtGoodsCodeFilePath1
            // 
            this.TxtGoodsCodeFilePath1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtGoodsCodeFilePath1.BackColor = System.Drawing.Color.Transparent;
            this.TxtGoodsCodeFilePath1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.TxtGoodsCodeFilePath1.ForeColor = System.Drawing.Color.DimGray;
            this.TxtGoodsCodeFilePath1.Location = new System.Drawing.Point(203, 27);
            this.TxtGoodsCodeFilePath1.MaxLength = 32767;
            this.TxtGoodsCodeFilePath1.Multiline = false;
            this.TxtGoodsCodeFilePath1.Name = "TxtGoodsCodeFilePath1";
            this.TxtGoodsCodeFilePath1.ReadOnly = true;
            this.TxtGoodsCodeFilePath1.Size = new System.Drawing.Size(473, 28);
            this.TxtGoodsCodeFilePath1.TabIndex = 68;
            this.TxtGoodsCodeFilePath1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.toolTip1.SetToolTip(this.TxtGoodsCodeFilePath1, "상품 코드 수집 된 엑셀파일 가져올 경로 입니다.");
            this.TxtGoodsCodeFilePath1.UseSystemPasswordChar = false;
            // 
            // BtnGetGoodsCodeFileExplore1
            // 
            this.BtnGetGoodsCodeFileExplore1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnGetGoodsCodeFileExplore1.BackColor = System.Drawing.Color.Transparent;
            this.BtnGetGoodsCodeFileExplore1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.BtnGetGoodsCodeFileExplore1.Image = null;
            this.BtnGetGoodsCodeFileExplore1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnGetGoodsCodeFileExplore1.Location = new System.Drawing.Point(682, 27);
            this.BtnGetGoodsCodeFileExplore1.Name = "BtnGetGoodsCodeFileExplore1";
            this.BtnGetGoodsCodeFileExplore1.Size = new System.Drawing.Size(55, 28);
            this.BtnGetGoodsCodeFileExplore1.TabIndex = 67;
            this.BtnGetGoodsCodeFileExplore1.Text = "...";
            this.BtnGetGoodsCodeFileExplore1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.toolTip1.SetToolTip(this.BtnGetGoodsCodeFileExplore1, "상품코드 엑셀파일 경로를 지정하세요.");
            this.BtnGetGoodsCodeFileExplore1.Click += new System.EventHandler(this.BtnGetFileExplore1_Click);
            // 
            // txtFilePath2
            // 
            this.txtFilePath2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath2.BackColor = System.Drawing.Color.Transparent;
            this.txtFilePath2.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtFilePath2.ForeColor = System.Drawing.Color.DimGray;
            this.txtFilePath2.Location = new System.Drawing.Point(203, 27);
            this.txtFilePath2.MaxLength = 32767;
            this.txtFilePath2.Multiline = false;
            this.txtFilePath2.Name = "txtFilePath2";
            this.txtFilePath2.ReadOnly = true;
            this.txtFilePath2.Size = new System.Drawing.Size(473, 28);
            this.txtFilePath2.TabIndex = 31;
            this.txtFilePath2.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.toolTip1.SetToolTip(this.txtFilePath2, "상품명 입력할 상품정보 엑셀 파일 경로입니다.");
            this.txtFilePath2.UseSystemPasswordChar = false;
            // 
            // btnGetFileExplore
            // 
            this.btnGetFileExplore.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGetFileExplore.BackColor = System.Drawing.Color.Transparent;
            this.btnGetFileExplore.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnGetFileExplore.Image = null;
            this.btnGetFileExplore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetFileExplore.Location = new System.Drawing.Point(682, 27);
            this.btnGetFileExplore.Name = "btnGetFileExplore";
            this.btnGetFileExplore.Size = new System.Drawing.Size(55, 28);
            this.btnGetFileExplore.TabIndex = 30;
            this.btnGetFileExplore.Text = "...";
            this.btnGetFileExplore.TextAlignment = System.Drawing.StringAlignment.Center;
            this.toolTip1.SetToolTip(this.btnGetFileExplore, "상품명 입력할 상품정보 엑셀 파일 경로를 지정하세요.");
            this.btnGetFileExplore.Click += new System.EventHandler(this.BtnGetFileExplore_Click);
            // 
            // TxtFaildFilePath
            // 
            this.TxtFaildFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtFaildFilePath.BackColor = System.Drawing.Color.Transparent;
            this.TxtFaildFilePath.Font = new System.Drawing.Font("Tahoma", 11F);
            this.TxtFaildFilePath.ForeColor = System.Drawing.Color.DimGray;
            this.TxtFaildFilePath.Location = new System.Drawing.Point(62, 65);
            this.TxtFaildFilePath.MaxLength = 32767;
            this.TxtFaildFilePath.Multiline = false;
            this.TxtFaildFilePath.Name = "TxtFaildFilePath";
            this.TxtFaildFilePath.ReadOnly = true;
            this.TxtFaildFilePath.Size = new System.Drawing.Size(614, 28);
            this.TxtFaildFilePath.TabIndex = 67;
            this.TxtFaildFilePath.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.toolTip1.SetToolTip(this.TxtFaildFilePath, "입력 실패한 상품정보와 번역미동의 엑셀파일을 생성할 경로입니다.");
            this.TxtFaildFilePath.UseSystemPasswordChar = false;
            // 
            // BtnFaildFilePath
            // 
            this.BtnFaildFilePath.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnFaildFilePath.BackColor = System.Drawing.Color.Transparent;
            this.BtnFaildFilePath.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.BtnFaildFilePath.Image = null;
            this.BtnFaildFilePath.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnFaildFilePath.Location = new System.Drawing.Point(682, 65);
            this.BtnFaildFilePath.Name = "BtnFaildFilePath";
            this.BtnFaildFilePath.Size = new System.Drawing.Size(55, 28);
            this.BtnFaildFilePath.TabIndex = 68;
            this.BtnFaildFilePath.Text = "...";
            this.BtnFaildFilePath.TextAlignment = System.Drawing.StringAlignment.Center;
            this.toolTip1.SetToolTip(this.BtnFaildFilePath, "입력 실패한 상품정보와 번역미동의 엑셀파일을 생성할 경로를 지정하세요.");
            this.BtnFaildFilePath.Click += new System.EventHandler(this.BtnFaildFilePath_Click);
            // 
            // BtnClearUrl
            // 
            this.BtnClearUrl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnClearUrl.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearUrl.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.BtnClearUrl.Image = null;
            this.BtnClearUrl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnClearUrl.Location = new System.Drawing.Point(685, 27);
            this.BtnClearUrl.Name = "BtnClearUrl";
            this.BtnClearUrl.Size = new System.Drawing.Size(52, 28);
            this.BtnClearUrl.TabIndex = 66;
            this.BtnClearUrl.Text = "x";
            this.BtnClearUrl.TextAlignment = System.Drawing.StringAlignment.Center;
            this.toolTip1.SetToolTip(this.BtnClearUrl, "G마켓 링크를 삭제합니다.");
            this.BtnClearUrl.Click += new System.EventHandler(this.BtnClearUrl_Click);
            // 
            // iTalk_ThemeContainer
            // 
            this.iTalk_ThemeContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.iTalk_ThemeContainer.Controls.Add(this.iTalk_ControlBox1);
            this.iTalk_ThemeContainer.Controls.Add(this.iTalk_TabControl);
            this.iTalk_ThemeContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iTalk_ThemeContainer.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.iTalk_ThemeContainer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.iTalk_ThemeContainer.Location = new System.Drawing.Point(0, 0);
            this.iTalk_ThemeContainer.Name = "iTalk_ThemeContainer";
            this.iTalk_ThemeContainer.Padding = new System.Windows.Forms.Padding(3, 24, 3, 24);
            this.iTalk_ThemeContainer.Sizable = false;
            this.iTalk_ThemeContainer.Size = new System.Drawing.Size(953, 621);
            this.iTalk_ThemeContainer.SmartBounds = false;
            this.iTalk_ThemeContainer.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.iTalk_ThemeContainer.TabIndex = 0;
            this.iTalk_ThemeContainer.Text = "Concentrix & Gmarket";
            // 
            // iTalk_ControlBox1
            // 
            this.iTalk_ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iTalk_ControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.iTalk_ControlBox1.Location = new System.Drawing.Point(872, -1);
            this.iTalk_ControlBox1.Name = "iTalk_ControlBox1";
            this.iTalk_ControlBox1.Size = new System.Drawing.Size(77, 19);
            this.iTalk_ControlBox1.TabIndex = 0;
            this.iTalk_ControlBox1.Text = "iTalk_ControlBox1";
            // 
            // iTalk_TabControl
            // 
            this.iTalk_TabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.iTalk_TabControl.Controls.Add(this.tabCrawler);
            this.iTalk_TabControl.Controls.Add(this.tabAdminCrawler);
            this.iTalk_TabControl.Controls.Add(this.tabMacro);
            this.iTalk_TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iTalk_TabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.iTalk_TabControl.HotTrack = true;
            this.iTalk_TabControl.ImageList = this.imageList1;
            this.iTalk_TabControl.ItemSize = new System.Drawing.Size(44, 135);
            this.iTalk_TabControl.Location = new System.Drawing.Point(3, 24);
            this.iTalk_TabControl.Margin = new System.Windows.Forms.Padding(0);
            this.iTalk_TabControl.Multiline = true;
            this.iTalk_TabControl.Name = "iTalk_TabControl";
            this.iTalk_TabControl.SelectedIndex = 0;
            this.iTalk_TabControl.Size = new System.Drawing.Size(947, 573);
            this.iTalk_TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.iTalk_TabControl.TabIndex = 1;
            // 
            // tabCrawler
            // 
            this.tabCrawler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tabCrawler.Controls.Add(this.CBLanguage);
            this.tabCrawler.Controls.Add(this.BtnClearUrl);
            this.tabCrawler.Controls.Add(this.LblTabActionText);
            this.tabCrawler.Controls.Add(this.nudStart);
            this.tabCrawler.Controls.Add(this.CboFileNameType);
            this.tabCrawler.Controls.Add(this.cboGetType);
            this.tabCrawler.Controls.Add(this.btnGetGoodsCode);
            this.tabCrawler.Controls.Add(this.iTalk_Label1);
            this.tabCrawler.Controls.Add(this.TxtFileName);
            this.tabCrawler.Controls.Add(this.txtUrl);
            this.tabCrawler.Controls.Add(this.nudEnd);
            this.tabCrawler.Controls.Add(this.btnGetFile);
            this.tabCrawler.Controls.Add(this.txtFilePath);
            this.tabCrawler.Controls.Add(this.iTalk_GroupBox1);
            this.tabCrawler.ImageIndex = 0;
            this.tabCrawler.Location = new System.Drawing.Point(139, 4);
            this.tabCrawler.Name = "tabCrawler";
            this.tabCrawler.Padding = new System.Windows.Forms.Padding(3);
            this.tabCrawler.Size = new System.Drawing.Size(804, 565);
            this.tabCrawler.TabIndex = 0;
            this.tabCrawler.Text = "상품코드 수집";
            // 
            // CBLanguage
            // 
            this.CBLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CBLanguage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.CBLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBLanguage.DropDownHeight = 100;
            this.CBLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBLanguage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CBLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.CBLanguage.FormattingEnabled = true;
            this.CBLanguage.HoverSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.CBLanguage.IntegralHeight = false;
            this.CBLanguage.ItemHeight = 20;
            this.CBLanguage.Items.AddRange(new object[] {
            "기본",
            "중문"});
            this.CBLanguage.Location = new System.Drawing.Point(371, 29);
            this.CBLanguage.Name = "CBLanguage";
            this.CBLanguage.Size = new System.Drawing.Size(76, 26);
            this.CBLanguage.StartIndex = 0;
            this.CBLanguage.TabIndex = 67;
            // 
            // LblTabActionText
            // 
            this.LblTabActionText.AutoSize = true;
            this.LblTabActionText.BackColor = System.Drawing.Color.Transparent;
            this.LblTabActionText.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.LblTabActionText.ForeColor = System.Drawing.Color.Blue;
            this.LblTabActionText.Location = new System.Drawing.Point(336, 159);
            this.LblTabActionText.Name = "LblTabActionText";
            this.LblTabActionText.Size = new System.Drawing.Size(132, 13);
            this.LblTabActionText.TabIndex = 65;
            this.LblTabActionText.Text = "상품코드 수집 중입니다...";
            this.LblTabActionText.Visible = false;
            // 
            // nudStart
            // 
            this.nudStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudStart.BackColor = System.Drawing.Color.Transparent;
            this.nudStart.Font = new System.Drawing.Font("Tahoma", 11F);
            this.nudStart.ForeColor = System.Drawing.Color.DimGray;
            this.nudStart.Location = new System.Drawing.Point(188, 27);
            this.nudStart.Maximum = ((long)(999999));
            this.nudStart.Minimum = ((long)(1));
            this.nudStart.MinimumSize = new System.Drawing.Size(62, 28);
            this.nudStart.Name = "nudStart";
            this.nudStart.Size = new System.Drawing.Size(75, 28);
            this.nudStart.TabIndex = 64;
            this.nudStart.Text = "iTalk_NumericUpDown1";
            this.nudStart.TextAlignment = iTalk.iTalk_NumericUpDown._TextAlignment.Near;
            this.nudStart.Value = ((long)(1));
            // 
            // CboFileNameType
            // 
            this.CboFileNameType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CboFileNameType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.CboFileNameType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CboFileNameType.DropDownHeight = 100;
            this.CboFileNameType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboFileNameType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CboFileNameType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.CboFileNameType.FormattingEnabled = true;
            this.CboFileNameType.HoverSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.CboFileNameType.IntegralHeight = false;
            this.CboFileNameType.ItemHeight = 20;
            this.CboFileNameType.Items.AddRange(new object[] {
            "기본 파일명",
            "변경 파일명"});
            this.CboFileNameType.Location = new System.Drawing.Point(62, 64);
            this.CboFileNameType.Name = "CboFileNameType";
            this.CboFileNameType.Size = new System.Drawing.Size(113, 26);
            this.CboFileNameType.StartIndex = 0;
            this.CboFileNameType.TabIndex = 63;
            this.CboFileNameType.SelectedIndexChanged += new System.EventHandler(this.CboFileNameType_SelectedIndexChanged);
            // 
            // cboGetType
            // 
            this.cboGetType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboGetType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.cboGetType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGetType.DropDownHeight = 100;
            this.cboGetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGetType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGetType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.cboGetType.FormattingEnabled = true;
            this.cboGetType.HoverSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.cboGetType.IntegralHeight = false;
            this.cboGetType.ItemHeight = 20;
            this.cboGetType.Items.AddRange(new object[] {
            "페이지",
            "갯수 "});
            this.cboGetType.Location = new System.Drawing.Point(62, 29);
            this.cboGetType.Name = "cboGetType";
            this.cboGetType.Size = new System.Drawing.Size(113, 26);
            this.cboGetType.StartIndex = 0;
            this.cboGetType.TabIndex = 63;
            // 
            // btnGetGoodsCode
            // 
            this.btnGetGoodsCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetGoodsCode.BackColor = System.Drawing.Color.Transparent;
            this.btnGetGoodsCode.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnGetGoodsCode.ForeColor = System.Drawing.Color.White;
            this.btnGetGoodsCode.Image = null;
            this.btnGetGoodsCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetGoodsCode.Location = new System.Drawing.Point(62, 100);
            this.btnGetGoodsCode.Name = "btnGetGoodsCode";
            this.btnGetGoodsCode.Size = new System.Drawing.Size(675, 40);
            this.btnGetGoodsCode.TabIndex = 62;
            this.btnGetGoodsCode.Text = "상품코드 수집";
            this.btnGetGoodsCode.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnGetGoodsCode.Click += new System.EventHandler(this.BtnGetGoodsCode_Click);
            // 
            // iTalk_Label1
            // 
            this.iTalk_Label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.iTalk_Label1.AutoSize = true;
            this.iTalk_Label1.BackColor = System.Drawing.Color.Transparent;
            this.iTalk_Label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.iTalk_Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.iTalk_Label1.Location = new System.Drawing.Point(269, 35);
            this.iTalk_Label1.Name = "iTalk_Label1";
            this.iTalk_Label1.Size = new System.Drawing.Size(15, 13);
            this.iTalk_Label1.TabIndex = 61;
            this.iTalk_Label1.Text = "~";
            // 
            // TxtFileName
            // 
            this.TxtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtFileName.BackColor = System.Drawing.Color.Transparent;
            this.TxtFileName.Font = new System.Drawing.Font("Tahoma", 11F);
            this.TxtFileName.ForeColor = System.Drawing.Color.DimGray;
            this.TxtFileName.Location = new System.Drawing.Point(188, 63);
            this.TxtFileName.MaxLength = 32767;
            this.TxtFileName.Multiline = false;
            this.TxtFileName.Name = "TxtFileName";
            this.TxtFileName.ReadOnly = true;
            this.TxtFileName.Size = new System.Drawing.Size(177, 28);
            this.TxtFileName.TabIndex = 57;
            this.TxtFileName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtFileName.UseSystemPasswordChar = false;
            this.TxtFileName.Enter += new System.EventHandler(this.TxtFileName_Enter);
            this.TxtFileName.Leave += new System.EventHandler(this.TxtFileName_Leave);
            // 
            // txtUrl
            // 
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.BackColor = System.Drawing.Color.Transparent;
            this.txtUrl.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtUrl.ForeColor = System.Drawing.Color.DimGray;
            this.txtUrl.Location = new System.Drawing.Point(453, 27);
            this.txtUrl.MaxLength = 32767;
            this.txtUrl.Multiline = false;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = false;
            this.txtUrl.Size = new System.Drawing.Size(226, 28);
            this.txtUrl.TabIndex = 57;
            this.txtUrl.Text = "링크를 입력하여 주세요.";
            this.txtUrl.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUrl.UseSystemPasswordChar = false;
            this.txtUrl.Enter += new System.EventHandler(this.TxtUrl_Enter);
            this.txtUrl.Leave += new System.EventHandler(this.TxtUrl_Leave);
            // 
            // nudEnd
            // 
            this.nudEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudEnd.BackColor = System.Drawing.Color.Transparent;
            this.nudEnd.Font = new System.Drawing.Font("Tahoma", 11F);
            this.nudEnd.ForeColor = System.Drawing.Color.DimGray;
            this.nudEnd.Location = new System.Drawing.Point(290, 27);
            this.nudEnd.Maximum = ((long)(999999));
            this.nudEnd.Minimum = ((long)(1));
            this.nudEnd.MinimumSize = new System.Drawing.Size(62, 28);
            this.nudEnd.Name = "nudEnd";
            this.nudEnd.Size = new System.Drawing.Size(75, 28);
            this.nudEnd.TabIndex = 60;
            this.nudEnd.Text = "iTalk_NumericUpDown1";
            this.nudEnd.TextAlignment = iTalk.iTalk_NumericUpDown._TextAlignment.Near;
            this.nudEnd.Value = ((long)(2));
            // 
            // iTalk_GroupBox1
            // 
            this.iTalk_GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iTalk_GroupBox1.AutoScroll = true;
            this.iTalk_GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.iTalk_GroupBox1.Controls.Add(this.PiTab1);
            this.iTalk_GroupBox1.Controls.Add(this.lvCrawlerLog);
            this.iTalk_GroupBox1.Location = new System.Drawing.Point(3, 175);
            this.iTalk_GroupBox1.MinimumSize = new System.Drawing.Size(136, 50);
            this.iTalk_GroupBox1.Name = "iTalk_GroupBox1";
            this.iTalk_GroupBox1.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            this.iTalk_GroupBox1.Size = new System.Drawing.Size(798, 362);
            this.iTalk_GroupBox1.TabIndex = 56;
            this.iTalk_GroupBox1.Text = "진행 내역";
            // 
            // PiTab1
            // 
            this.PiTab1.Location = new System.Drawing.Point(355, 31);
            this.PiTab1.MinimumSize = new System.Drawing.Size(80, 80);
            this.PiTab1.Name = "PiTab1";
            this.PiTab1.P_AnimationColor = System.Drawing.Color.DimGray;
            this.PiTab1.P_AnimationSpeed = 100;
            this.PiTab1.P_BaseColor = System.Drawing.Color.DarkGray;
            this.PiTab1.Size = new System.Drawing.Size(80, 80);
            this.PiTab1.TabIndex = 3;
            this.PiTab1.Text = "iTalk_ProgressIndicator1";
            this.PiTab1.Visible = false;
            // 
            // lvCrawlerLog
            // 
            this.lvCrawlerLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvCrawlerLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCrawlerLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvCrawlerLog.Location = new System.Drawing.Point(5, 28);
            this.lvCrawlerLog.Name = "lvCrawlerLog";
            this.lvCrawlerLog.Size = new System.Drawing.Size(788, 329);
            this.lvCrawlerLog.TabIndex = 2;
            this.lvCrawlerLog.UseCompatibleStateImageBehavior = false;
            this.lvCrawlerLog.View = System.Windows.Forms.View.List;
            // 
            // tabAdminCrawler
            // 
            this.tabAdminCrawler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tabAdminCrawler.Controls.Add(this.CboFileNameType1);
            this.tabAdminCrawler.Controls.Add(this.TxtFileName1);
            this.tabAdminCrawler.Controls.Add(this.LblTab1ActionText);
            this.tabAdminCrawler.Controls.Add(this.TxtGoodsNameFilePath1);
            this.tabAdminCrawler.Controls.Add(this.BtnGetGoodsNameFilePath1);
            this.tabAdminCrawler.Controls.Add(this.txtAdminIdTab1);
            this.tabAdminCrawler.Controls.Add(this.btnAdd1);
            this.tabAdminCrawler.Controls.Add(this.iTalk_GroupBox3);
            this.tabAdminCrawler.Controls.Add(this.TxtGoodsCodeFilePath1);
            this.tabAdminCrawler.Controls.Add(this.BtnGetGoodsCodeFileExplore1);
            this.tabAdminCrawler.ImageIndex = 2;
            this.tabAdminCrawler.Location = new System.Drawing.Point(139, 4);
            this.tabAdminCrawler.Name = "tabAdminCrawler";
            this.tabAdminCrawler.Size = new System.Drawing.Size(804, 565);
            this.tabAdminCrawler.TabIndex = 2;
            this.tabAdminCrawler.Text = "상품명 수집";
            // 
            // CboFileNameType1
            // 
            this.CboFileNameType1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CboFileNameType1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.CboFileNameType1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CboFileNameType1.DropDownHeight = 100;
            this.CboFileNameType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboFileNameType1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CboFileNameType1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.CboFileNameType1.FormattingEnabled = true;
            this.CboFileNameType1.HoverSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.CboFileNameType1.IntegralHeight = false;
            this.CboFileNameType1.ItemHeight = 20;
            this.CboFileNameType1.Items.AddRange(new object[] {
            "기본 파일명",
            "변경 파일명"});
            this.CboFileNameType1.Location = new System.Drawing.Point(62, 66);
            this.CboFileNameType1.Name = "CboFileNameType1";
            this.CboFileNameType1.Size = new System.Drawing.Size(113, 26);
            this.CboFileNameType1.StartIndex = 0;
            this.CboFileNameType1.TabIndex = 76;
            this.CboFileNameType1.SelectedIndexChanged += new System.EventHandler(this.CboFileNameType1_SelectedIndexChanged);
            // 
            // TxtFileName1
            // 
            this.TxtFileName1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtFileName1.BackColor = System.Drawing.Color.Transparent;
            this.TxtFileName1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.TxtFileName1.ForeColor = System.Drawing.Color.DimGray;
            this.TxtFileName1.Location = new System.Drawing.Point(188, 65);
            this.TxtFileName1.MaxLength = 32767;
            this.TxtFileName1.Multiline = false;
            this.TxtFileName1.Name = "TxtFileName1";
            this.TxtFileName1.ReadOnly = true;
            this.TxtFileName1.Size = new System.Drawing.Size(177, 28);
            this.TxtFileName1.TabIndex = 75;
            this.TxtFileName1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtFileName1.UseSystemPasswordChar = false;
            this.TxtFileName1.Enter += new System.EventHandler(this.TxtFileName1_Enter);
            this.TxtFileName1.Leave += new System.EventHandler(this.TxtFileName1_Leave);
            // 
            // LblTab1ActionText
            // 
            this.LblTab1ActionText.AutoSize = true;
            this.LblTab1ActionText.BackColor = System.Drawing.Color.Transparent;
            this.LblTab1ActionText.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.LblTab1ActionText.ForeColor = System.Drawing.Color.Blue;
            this.LblTab1ActionText.Location = new System.Drawing.Point(342, 159);
            this.LblTab1ActionText.Name = "LblTab1ActionText";
            this.LblTab1ActionText.Size = new System.Drawing.Size(121, 13);
            this.LblTab1ActionText.TabIndex = 74;
            this.LblTab1ActionText.Text = "상품명 수집 중입니다...";
            this.LblTab1ActionText.Visible = false;
            // 
            // txtAdminIdTab1
            // 
            this.txtAdminIdTab1.BackColor = System.Drawing.Color.Transparent;
            this.txtAdminIdTab1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtAdminIdTab1.ForeColor = System.Drawing.Color.DimGray;
            this.txtAdminIdTab1.Location = new System.Drawing.Point(62, 27);
            this.txtAdminIdTab1.MaxLength = 32767;
            this.txtAdminIdTab1.Multiline = false;
            this.txtAdminIdTab1.Name = "txtAdminIdTab1";
            this.txtAdminIdTab1.ReadOnly = false;
            this.txtAdminIdTab1.Size = new System.Drawing.Size(135, 28);
            this.txtAdminIdTab1.TabIndex = 71;
            this.txtAdminIdTab1.Text = "admin 아이디";
            this.txtAdminIdTab1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAdminIdTab1.UseSystemPasswordChar = false;
            this.txtAdminIdTab1.Enter += new System.EventHandler(this.TxtAdminIdTab1_Enter);
            this.txtAdminIdTab1.Leave += new System.EventHandler(this.TxtAdminIdTab1_Leave);
            // 
            // btnAdd1
            // 
            this.btnAdd1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd1.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnAdd1.ForeColor = System.Drawing.Color.White;
            this.btnAdd1.Image = null;
            this.btnAdd1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd1.Location = new System.Drawing.Point(62, 100);
            this.btnAdd1.Name = "btnAdd1";
            this.btnAdd1.Size = new System.Drawing.Size(675, 40);
            this.btnAdd1.TabIndex = 70;
            this.btnAdd1.Text = "상품명 수집";
            this.btnAdd1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnAdd1.Click += new System.EventHandler(this.BtnAdd1_Click);
            // 
            // iTalk_GroupBox3
            // 
            this.iTalk_GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iTalk_GroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.iTalk_GroupBox3.Controls.Add(this.PiTab2);
            this.iTalk_GroupBox3.Controls.Add(this.lvAdminCrawlerLog);
            this.iTalk_GroupBox3.Location = new System.Drawing.Point(3, 175);
            this.iTalk_GroupBox3.MinimumSize = new System.Drawing.Size(136, 50);
            this.iTalk_GroupBox3.Name = "iTalk_GroupBox3";
            this.iTalk_GroupBox3.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            this.iTalk_GroupBox3.Size = new System.Drawing.Size(798, 362);
            this.iTalk_GroupBox3.TabIndex = 69;
            this.iTalk_GroupBox3.Text = "진행 내역";
            // 
            // PiTab2
            // 
            this.PiTab2.Location = new System.Drawing.Point(355, 31);
            this.PiTab2.MinimumSize = new System.Drawing.Size(80, 80);
            this.PiTab2.Name = "PiTab2";
            this.PiTab2.P_AnimationColor = System.Drawing.Color.DimGray;
            this.PiTab2.P_AnimationSpeed = 100;
            this.PiTab2.P_BaseColor = System.Drawing.Color.DarkGray;
            this.PiTab2.Size = new System.Drawing.Size(80, 80);
            this.PiTab2.TabIndex = 4;
            this.PiTab2.Text = "iTalk_ProgressIndicator2";
            this.PiTab2.Visible = false;
            // 
            // lvAdminCrawlerLog
            // 
            this.lvAdminCrawlerLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvAdminCrawlerLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAdminCrawlerLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvAdminCrawlerLog.Location = new System.Drawing.Point(5, 28);
            this.lvAdminCrawlerLog.Name = "lvAdminCrawlerLog";
            this.lvAdminCrawlerLog.Size = new System.Drawing.Size(788, 329);
            this.lvAdminCrawlerLog.TabIndex = 2;
            this.lvAdminCrawlerLog.UseCompatibleStateImageBehavior = false;
            this.lvAdminCrawlerLog.View = System.Windows.Forms.View.List;
            // 
            // tabMacro
            // 
            this.tabMacro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tabMacro.Controls.Add(this.ChkFaildProc);
            this.tabMacro.Controls.Add(this.BtnFaildFilePath);
            this.tabMacro.Controls.Add(this.TxtFaildFilePath);
            this.tabMacro.Controls.Add(this.LblTab2ActionText);
            this.tabMacro.Controls.Add(this.txtAdminIdTab2);
            this.tabMacro.Controls.Add(this.btnAdd);
            this.tabMacro.Controls.Add(this.iTalk_GroupBox2);
            this.tabMacro.Controls.Add(this.txtFilePath2);
            this.tabMacro.Controls.Add(this.btnGetFileExplore);
            this.tabMacro.ImageIndex = 1;
            this.tabMacro.Location = new System.Drawing.Point(139, 4);
            this.tabMacro.Name = "tabMacro";
            this.tabMacro.Padding = new System.Windows.Forms.Padding(3);
            this.tabMacro.Size = new System.Drawing.Size(804, 565);
            this.tabMacro.TabIndex = 1;
            this.tabMacro.Text = "상품명 입력";
            // 
            // ChkFaildProc
            // 
            this.ChkFaildProc.BackColor = System.Drawing.Color.Transparent;
            this.ChkFaildProc.Checked = true;
            this.ChkFaildProc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ChkFaildProc.Location = new System.Drawing.Point(62, 154);
            this.ChkFaildProc.Name = "ChkFaildProc";
            this.ChkFaildProc.Size = new System.Drawing.Size(245, 15);
            this.ChkFaildProc.TabIndex = 69;
            this.ChkFaildProc.Text = "상품명 입력 후 입력 실패 내역 검사";
            // 
            // LblTab2ActionText
            // 
            this.LblTab2ActionText.AutoSize = true;
            this.LblTab2ActionText.BackColor = System.Drawing.Color.Transparent;
            this.LblTab2ActionText.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.LblTab2ActionText.ForeColor = System.Drawing.Color.Blue;
            this.LblTab2ActionText.Location = new System.Drawing.Point(342, 159);
            this.LblTab2ActionText.Name = "LblTab2ActionText";
            this.LblTab2ActionText.Size = new System.Drawing.Size(121, 13);
            this.LblTab2ActionText.TabIndex = 5;
            this.LblTab2ActionText.Text = "상품명 입력 중입니다...";
            this.LblTab2ActionText.Visible = false;
            // 
            // txtAdminIdTab2
            // 
            this.txtAdminIdTab2.BackColor = System.Drawing.Color.Transparent;
            this.txtAdminIdTab2.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtAdminIdTab2.ForeColor = System.Drawing.Color.DimGray;
            this.txtAdminIdTab2.Location = new System.Drawing.Point(62, 27);
            this.txtAdminIdTab2.MaxLength = 32767;
            this.txtAdminIdTab2.Multiline = false;
            this.txtAdminIdTab2.Name = "txtAdminIdTab2";
            this.txtAdminIdTab2.ReadOnly = false;
            this.txtAdminIdTab2.Size = new System.Drawing.Size(135, 28);
            this.txtAdminIdTab2.TabIndex = 66;
            this.txtAdminIdTab2.Text = "admin 아이디";
            this.txtAdminIdTab2.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAdminIdTab2.UseSystemPasswordChar = false;
            this.txtAdminIdTab2.Enter += new System.EventHandler(this.TxtAdminIdTab2_Enter);
            this.txtAdminIdTab2.Leave += new System.EventHandler(this.TxtAdminIdTab2_Leave);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = null;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(62, 100);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(675, 40);
            this.btnAdd.TabIndex = 33;
            this.btnAdd.Text = "상품명 입력";
            this.btnAdd.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // iTalk_GroupBox2
            // 
            this.iTalk_GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iTalk_GroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.iTalk_GroupBox2.Controls.Add(this.PiTab3);
            this.iTalk_GroupBox2.Controls.Add(this.lvMacroLog);
            this.iTalk_GroupBox2.Location = new System.Drawing.Point(3, 175);
            this.iTalk_GroupBox2.MinimumSize = new System.Drawing.Size(136, 50);
            this.iTalk_GroupBox2.Name = "iTalk_GroupBox2";
            this.iTalk_GroupBox2.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            this.iTalk_GroupBox2.Size = new System.Drawing.Size(798, 362);
            this.iTalk_GroupBox2.TabIndex = 32;
            this.iTalk_GroupBox2.Text = "진행 내역";
            // 
            // PiTab3
            // 
            this.PiTab3.Location = new System.Drawing.Point(355, 31);
            this.PiTab3.MinimumSize = new System.Drawing.Size(80, 80);
            this.PiTab3.Name = "PiTab3";
            this.PiTab3.P_AnimationColor = System.Drawing.Color.DimGray;
            this.PiTab3.P_AnimationSpeed = 100;
            this.PiTab3.P_BaseColor = System.Drawing.Color.DarkGray;
            this.PiTab3.Size = new System.Drawing.Size(80, 80);
            this.PiTab3.TabIndex = 4;
            this.PiTab3.Text = "iTalk_ProgressIndicator3";
            this.PiTab3.Visible = false;
            // 
            // lvMacroLog
            // 
            this.lvMacroLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvMacroLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMacroLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMacroLog.Location = new System.Drawing.Point(5, 28);
            this.lvMacroLog.Name = "lvMacroLog";
            this.lvMacroLog.Size = new System.Drawing.Size(788, 329);
            this.lvMacroLog.TabIndex = 2;
            this.lvMacroLog.UseCompatibleStateImageBehavior = false;
            this.lvMacroLog.View = System.Windows.Forms.View.List;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 621);
            this.Controls.Add(this.iTalk_ThemeContainer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(953, 621);
            this.MinimumSize = new System.Drawing.Size(953, 621);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Concentrix & Gmarket";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Main_Load);
            this.iTalk_ThemeContainer.ResumeLayout(false);
            this.iTalk_TabControl.ResumeLayout(false);
            this.tabCrawler.ResumeLayout(false);
            this.tabCrawler.PerformLayout();
            this.iTalk_GroupBox1.ResumeLayout(false);
            this.tabAdminCrawler.ResumeLayout(false);
            this.tabAdminCrawler.PerformLayout();
            this.iTalk_GroupBox3.ResumeLayout(false);
            this.tabMacro.ResumeLayout(false);
            this.tabMacro.PerformLayout();
            this.iTalk_GroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private iTalk.iTalk_ThemeContainer iTalk_ThemeContainer;
        private iTalk.iTalk_TabControl iTalk_TabControl;
        private System.Windows.Forms.TabPage tabCrawler;
        private System.Windows.Forms.TabPage tabMacro;
        private iTalk.iTalk_ControlBox iTalk_ControlBox1;
        private System.Windows.Forms.ImageList imageList1;
        private iTalk.iTalk_ComboBox cboGetType;
        private iTalk.iTalk_Button_2 btnGetGoodsCode;
        private iTalk.iTalk_Label iTalk_Label1;
        private iTalk.iTalk_TextBox_Small txtUrl;
        private iTalk.iTalk_NumericUpDown nudEnd;
        private iTalk.iTalk_Button_1 btnGetFile;
        private iTalk.iTalk_TextBox_Small txtFilePath;
        private iTalk.iTalk_GroupBox iTalk_GroupBox1;
        private iTalk.iTalk_Listview lvCrawlerLog;
        private iTalk.iTalk_Button_2 btnAdd;
        private iTalk.iTalk_GroupBox iTalk_GroupBox2;
        private iTalk.iTalk_Listview lvMacroLog;
        private iTalk.iTalk_TextBox_Small txtFilePath2;
        private iTalk.iTalk_Button_1 btnGetFileExplore;
        private iTalk.iTalk_TextBox_Small txtAdminIdTab2;
        private iTalk.iTalk_NumericUpDown nudStart;
        private System.Windows.Forms.TabPage tabAdminCrawler;
        private iTalk.iTalk_TextBox_Small txtAdminIdTab1;
        private iTalk.iTalk_Button_2 btnAdd1;
        private iTalk.iTalk_GroupBox iTalk_GroupBox3;
        private iTalk.iTalk_Listview lvAdminCrawlerLog;
        private iTalk.iTalk_TextBox_Small TxtGoodsCodeFilePath1;
        private iTalk.iTalk_Button_1 BtnGetGoodsCodeFileExplore1;
        private iTalk.iTalk_TextBox_Small TxtGoodsNameFilePath1;
        private iTalk.iTalk_Button_1 BtnGetGoodsNameFilePath1;
        private System.Windows.Forms.ToolTip toolTip1;
        private iTalk.iTalk_ProgressIndicator PiTab1;
        private iTalk.iTalk_ProgressIndicator PiTab2;
        private iTalk.iTalk_ProgressIndicator PiTab3;
        private iTalk.iTalk_Label LblTab2ActionText;
        private iTalk.iTalk_Label LblTabActionText;
        private iTalk.iTalk_Label LblTab1ActionText;
        private iTalk.iTalk_Button_1 BtnFaildFilePath;
        private iTalk.iTalk_TextBox_Small TxtFaildFilePath;
        private iTalk.iTalk_CheckBox ChkFaildProc;
        private iTalk.iTalk_Button_1 BtnClearUrl;
        private iTalk.iTalk_ComboBox CboFileNameType;
        private iTalk.iTalk_TextBox_Small TxtFileName;
        private iTalk.iTalk_ComboBox CboFileNameType1;
        private iTalk.iTalk_TextBox_Small TxtFileName1;
        private iTalk.iTalk_ComboBox CBLanguage;
    }
}

