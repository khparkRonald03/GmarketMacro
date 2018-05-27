using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmarketMacro
{
    /// <summary>
    /// 크롤링 & 매크로에 필요한 데이터, 엑셀에 왼쪽부터 [국, 영, 중, 일] 순으로 배치
    /// </summary>
    public class DataModel
    {
        /// <summary>
        /// 상품 코드
        /// </summary>
        [Display(Name = "상품코드")]
        public string GooodsCode { get; set; } = string.Empty;

        /// <summary>
        /// 국문 상품명
        /// </summary>
        [Display(Name = "국문")]
        public string NameKOR { get; set; } = string.Empty;

        /// <summary>
        /// 영문 상품명
        /// </summary>
        [Display(Name = "영문")]
        public string NameEN { get; set; } = string.Empty;

        /// <summary>
        /// 중문 상품명
        /// </summary>
        [Display(Name = "중문")]
        public string NameCN { get; set; } = string.Empty;

        /// <summary>
        /// 일문 상품명
        /// </summary>
        [Display(Name = "일문")]
        public string NameJP { get; set; } = string.Empty;
    }
}
