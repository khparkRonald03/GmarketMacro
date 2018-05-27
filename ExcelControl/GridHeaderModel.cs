namespace ExcelControl
{
    /// <summary>
    /// 공통뷰 컬럼 정보 (자바스크립트로 작성 된 공통뷰 그리드의 columns에 들어갈 데이터들)
    /// http://docs.telerik.com/kendo-ui/controls/data-management/grid/how-to/excel/column-template-export columns 정보 바인딩하는 부분 참조
    /// </summary>
    public class GridHeaderModel
    {
        private string _tableName = string.Empty;

        public GridHeaderModel()
        {
            sortable = true;
            sortType = SortType.DESC;
            defaultSortColumn = false;
        }

        public string key { get; set; }

        /// <summary>
        /// 테이블 컬럼 이름
        /// </summary>
        public string field { get; set; }

        /// <summary>
        /// 공통뷰 그리드 헤더 컬럼 표시 이름
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 컬럼 너비
        /// </summary>
        public string width { get; set; }

        /// <summary>
        /// 정렬 순서입니다. 
        /// (※※※※※ 헤더 컬럼 별로 중복 되지 않게 번호를 부여 하여주십시오. 이름이 중복 된 컬럼이 있을 때 구분자로 쓰이는 중요한 정보 입니다. ※※※※※)
        /// </summary>
        public int order { get; set; }

        /// <summary>
        /// 데이터 유형 (type="date" : 날짜)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 데이터 표시 유형 (format=Resources.Format_Grid_DateTime_yyyyMMddHHmm : Resources.Format_Grid_[Type])
        /// </summary>
        public string format { get; set; }

        /// <summary>
        /// 컬럼 데이터의 다국어 처리 (template = "#:GetTargetCodeDisplayName(TargetCode)#" => ~/Views/Shared/Grid/_Page.cshtml에 Javascript를 구현하여 다국어 처리)
        ///     function GetTargetCodeDisplayName(targetCode)
        ///     {
        ///         var targetCodeDisplayName = "";
        ///         switch(targetCode)
        ///         {
        ///             case "All":
        ///                 targetCodeDisplayName = "@Resources.PolicyTargetCode_All";
        ///                 break;
        ///             case "ComputerPolicy":
        ///                 targetCodeDisplayName = "@Resources.PolicyTargetCode_ComputerPolicy";
        ///                 break;
        ///             case "UserPolicy":
        ///                 targetCodeDisplayName = "@Resources.PolicyTargetCode_UserPolicy";
        ///                 break;
        ///         }
        ///         return targetCodeDisplayName;
        ///     }
        /// </summary>
        public string template { get; set; }

        public string codeList { get; set; }

        /// <summary>
        /// 컬럼을 클릭하지 않은 경우 기본 정렬 (SortType.DESC : 내림차순, SortType.ASC : 오름차순)
        /// </summary>
        public SortType sortType { get; set; }

        /// <summary>
        /// 데이터 모델이름 
        /// (테이블과 매치되는 모델의 이름) -> 내보내기 할 때 서로 중복되는 컬럼이름이 있을 때 구분해주는 역할(ex => DisplayName)
        /// </summary>
        //[ScriptIgnore]
        public string dataModelName { get; set; }

        /// <summary>
        /// 테이블 이름
        /// (공통 뷰에서 검색 조건 넘겨줄 때 "테이블.컬럼이름" 형식으로 넘겨주기 위하여 사용)
        /// (제외 가능 -> 제외 시 "컬럼이름" 만 넘어갑니다.)
        /// </summary>
        //[ScriptIgnore]
        public string tableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                _tableName = value;
            }
        }

        /// <summary>
        /// 숨김 여부 (true : 숨김, false : 표시)
        /// </summary>
        public bool hidden { get; set; }

        /// <summary>
        /// 검색 조건에 표시 여부 (true : 표시 안 함, false : 표시)
        /// </summary>
        public bool excludeSearch { get; set; }

        /// <summary>
        /// 컬럼 정렬 (true : 정렬 가능, false : 정렬 불가능)
        /// </summary>
        public bool sortable { get; set; }

        /// <summary>
        /// 컬럼을 클릭하지 않아 정렬 요청이 없을 시 기본 소팅 컬럼 여부 (true : 기존 정렬, false : 기본 정렬 아님)
        /// </summary>
        public bool defaultSortColumn { get; set; }

        /// <summary>
        /// 테이블이름.컬럼이름 가져오기
        /// </summary>
        //[ScriptIgnore]
        public string searchColumn
        {
            get
            {
                if (string.IsNullOrEmpty(tableName))
                {
                    return field;
                }
                return string.Format("{0}.{1}", tableName, field);
            }
        }

        private string _attributes;
        /// <summary>
        /// style 등의 속성 (attributes = "style=\"text-align:center;\"")
        /// </summary>
        public string attributes
        {
            get
            {
                return string.Format(" {0}", _attributes);
            }
            set
            {
                _attributes = value;
            }
        }

        /// <summary>
        /// 공통뷰 상단의 조회 날짜컨트롤과 매칭되는 컬럼인지 여부
        /// </summary>
        //[ScriptIgnore]
        public bool IsSearchDateColumn { get; set; }
    }

    /// <summary>
    /// 컬럼 소팅 타입 정의
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// 오름차순
        /// </summary>
        ASC,
        /// <summary>
        /// 내림차순
        /// </summary>
        DESC
    }
}