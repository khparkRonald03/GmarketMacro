using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmarketMacro
{
    public class XPathModel
    {
        /// <summary>
        /// 한페이지 전체 상품코드 가져오는 xPath (갯수로 가져오는 함수에서 한페이지 전체를 가져와야 되는 경우가 있어 어쩔 수 없이 나눔)
        /// </summary>
        public string PageNodeXPath { get; set; }

        /// <summary>
        /// 하나의 상품코드를 가져오는 xPath (갯수로 가져오는 함수에서 한페이지 전체를 가져와야 되는 경우가 있어 어쩔 수 없이 나눔)
        /// </summary>
        public string NumNodeXPath { get; set; }

        /// <summary>
        /// String.Format 파라미터 수
        /// </summary>
        public int ParamCount { get; set; }

        /// <summary>
        /// xPath문자열을 String.Format으로 파라미터를 붙을 때 파라미터 별 시작 번호를 지정 (ex_ 첫번째 div 태그에는 메인 이미지가 있고 두번째 div 태그 부터 상품리스트가 시작 되는 경우)
        /// </summary>
        public List<int> ParamsStartNum { get; set; } = new List<int>();

        /// <summary>
        /// 가로 갯수 (카드형 상품리스트일 경우 필요)
        /// </summary>
        public int ColLength { get; set; } = 0;

        /// <summary>
        /// 특이케이스는 따로 처리해주기 위해 별칭 부여
        /// </summary>
        public string Alias { get; set; } = string.Empty;

        /// <summary>
        /// PageNodeXPath 로 수집 한 노드 수 (상품코드 노드 수집 후 가장 많이 수집 된 xPath 쓸때 여기에 값들을 넣어놓고 비교하려고 사용.. 자세한건 코드 참조)
        /// </summary>
        public int SelectTotalNode { get; set; }
    }
}
