using BeautifulWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebController;

namespace GmarketMacro
{
    public class PCGmarketAction : CommonAction
    {
        private readonly List<XPathModel> xPathForNodes = null;

        public PCGmarketAction(bool isSilent = true)
            : base(isSilent)
        {
            xPathForNodes = GetxPathForNode();
        }

        /// <summary>
        /// xPath 리스트
        /// </summary>
        /// <returns></returns>
        private List<XPathModel> GetxPathForNode() => new List<XPathModel>()
            {
                // 1. 파라미터 1개
                // 타켓 Url -> http://gshop.gmarket.co.kr/Minishop/GlobalMinishop?CustNo=zM0MR38DNjkxNYx3MTk0NjE3NzJ/Rw==&GdlcCd=100000085
                new XPathModel() { Alias = "type1_1", PageNodeXPath = "//*[@id='minilist']/tr/td[2]/ul/li/a/@href", NumNodeXPath = "//*[@id='minilist']/tr[{0}]/td[2]/ul/li/a/@href", ParamCount = 1 },

                // 타켓 Url -> http://gcategory.gmarket.co.kr/Listview/Category?GdlcCd=100000103
                new XPathModel() { Alias = "type1_2", PageNodeXPath = "//*[@id='srplist']/tr/td[2]/ul/li[2]/a/@href", NumNodeXPath = "//*[@id='srplist']/tr[{0}]/td[2]/ul/li[2]/a/@href", ParamCount = 1 },

                // 타켓 Url -> http://gcorner.gmarket.co.kr/Superdeals
                new XPathModel() { Alias = "type1_3", PageNodeXPath = "//*[@id='superdeal']/div[3]/div/ul/li/a/@href", NumNodeXPath = "//*[@id='superdeal']/div[3]/div/ul/li[{0}]/a/@href", ParamCount = 1 },

                // 타켓 Url -> http://gcorner.gmarket.co.kr/SuperDeals/?GroupNo=4
                new XPathModel() { Alias = "type1_4", PageNodeXPath = "//*[@id='superdeal']/div[3]/div/ul/li/div/div/div/a/@href", NumNodeXPath = "//*[@id='superdeal']/div[3]/div/ul/li[{0}]/div/div/div/a/@href", ParamCount = 1 },

                // 타켓 Url -> http://global.gmarket.co.kr/Home/Main
                new XPathModel() { Alias = "type1_5", PageNodeXPath = "//*[@id='container']/div/div/div/ul/li/a/@href", NumNodeXPath = "//*[@id='container']/div/div/div/ul/li[{0}]/a/@href", ParamCount = 1 },

                // 타켓 Url -> http://gsearch.gmarket.co.kr/Listview/Search?keyword=%EC%A7%80%EC%98%A4%ED%95%84%EB%A1%9C%EC%9A%B0&type=IMG&pagesize=60&ordertype=&IsOversea=True&IsDeliveryFee=&IsGmarketBest=False&IsGmileage=False&IsDiscount=False&IsGstamp=False&IsBookCash=False&DelFee=&page=1&IsFeature=&IsGlobalSearch=undefined
                new XPathModel() { Alias = "type1_6", PageNodeXPath = "//*[@id='srplist']/li/div/div/div[1]/a/@href", NumNodeXPath = "//*[@id='srplist']/li[{0}]/div/div/div[1]/a/@href", ParamCount = 1 },

                // 타켓 Url -> (한글사이트) http://corners.gmarket.co.kr/Bestsellers
                new XPathModel() { Alias = "type1_7", PageNodeXPath = "//*[@id='gBestWrap']/div/div[3]/div[2]/ul/li/div[1]/a/@href", NumNodeXPath = "//*[@id='gBestWrap']/div/div[3]/div[2]/ul/li[{0}]/div[1]/a/@href", ParamCount = 1 },

                // 타켓 Url -> (한글사이트) http://corners.gmarket.co.kr/SuperDeals
                new XPathModel() { Alias = "type1_8", PageNodeXPath = "//*[@id='container']/div[2]/ul/li/div/a/@href", NumNodeXPath = "//*[@id='container']/div[2]/ul/li[{0}]/div/a/@href", ParamCount = 1 },


                // 2. 파라미터 2개

                // 타켓 Url -> http://gpromotion.gmarket.co.kr/Plan/PlanView?sid=160357 
                new XPathModel() { Alias = "type2_1", PageNodeXPath = "//*[@id='plan_contents']/div[2]/div[2]/ul/li/div/a/@href", NumNodeXPath = "//*[@id='plan_contents']/div[2]/div[2]/ul[{0}]/li[{1}]/div/a/@href", ParamCount = 2, ParamsStartNum = new List<int>() { 1, 1 }, ColLength = 3 },

                // 타켓 Url -> http://gcorner.gmarket.co.kr/Bestsellers/Category#
                new XPathModel() { Alias = "type2_2", PageNodeXPath = "//*[@id='bestseller_goodslst_form']/ul/li/div/a/@href", NumNodeXPath = "//*[@id='bestseller_goodslst_form']/ul[{0}]/li[{1}]/div/a/@href", ParamCount = 2, ParamsStartNum = new List<int>() { 1, 1 }, ColLength = 5 },

                // 타켓 Url -> http://gcorner.gmarket.co.kr/Bestsellers/Minishop
                new XPathModel() { Alias = "type2_3", PageNodeXPath = "//*[@id='bestmini_goodslst_form']/div/ul/li/p[1]/a/@href", NumNodeXPath = "//*[@id='bestmini_goodslst_form']/div[contains(@class, 'list_form contents_style5')][{0}]/ul/li[{1}]/p[1]/a/@href", ParamCount = 2, ParamsStartNum = new List<int>() { 1, 1 }, ColLength = 5 },
                
                // 타켓 Url -> http://gcorner.gmarket.co.kr/Bestsellers/Country
                new XPathModel() { Alias = "type2_4", PageNodeXPath = "//*[@id='container']/div/div[2]/ul/li/p[1]/a/@href", NumNodeXPath = "//*[@id='container']/div/div[2]/ul[{0}]/li[{1}]/p[1]/a/@href", ParamCount = 2, ParamsStartNum = new List<int>() { 1, 1 }, ColLength = 5 },

                // 타켓 Url -> http://gshop.gmarket.co.kr/Minishop/GlobalMinishop?keyword=&CustNo=zIxNR38zNzkxMcx1ODk0NDc0NzN/Rw==&type=IMG&pagesize=60&ordertype=&IsOversea=False&IsDeliveryFee=&IsGmarketBest=&IsGmileage=False&IsDiscount=False&IsGstamp=False&IsBookCash=False&DelFee=&page=1
                new XPathModel() { Alias = "type2_5", PageNodeXPath = "//*[@id='minilist']/ul/li/div/a/@href", NumNodeXPath = "//*[@id='minilist']/ul[{0}]/li[{1}]/div/a/@href", ParamCount = 2, ParamsStartNum = new List<int>() { 1, 1 }, ColLength = 4 },

                // 3. 파라미터 3개
                // 타켓 Url -> http://gpromotion.gmarket.co.kr/Plan/PlanView?sid=160591
                // 특이 케이스 페이지 : ColLength 3개, 4개 짜리가 있음
                new XPathModel() { Alias = "type3_1",  PageNodeXPath = "//*[@id='plan_contents']/div/div[2]/ul/li/div/a/@href", NumNodeXPath = "//*[@id='plan_contents']/div[{0}]/div[2]/ul[{1}]/li[{2}]/div/a/@href", ParamCount = 3, ParamsStartNum = new List<int>() { 2, 1, 1 } },
            };

        /// <summary>
        /// 페이지 단위 상품코드 수집
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        public List<DataModel> GetPageTypeGoods(int start, int end, Action<string> logFunc)
        {
            return base.GetPageTypeGoods(start, end, xPathForNodes, logFunc);
        }

        /// <summary>
        /// 갯수 단위 상품코드 수집
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        public List<DataModel> GetNumTypeGoods(int startNum, int endNum, Action<string> logFunc)
        {
            return base.GetNumTypeGoods(startNum, endNum, xPathForNodes, logFunc);
        }
    }
}
