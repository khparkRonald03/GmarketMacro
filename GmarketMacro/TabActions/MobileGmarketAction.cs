using System;
using System.Collections.Generic;
using System.Linq;
using BeautifulWeb;
using System.Text;
using System.Threading.Tasks;

namespace GmarketMacro
{
    public class MobileGmarketAction : CommonAction
    {
        readonly List<XPathModel> xPathForNodes;

        public MobileGmarketAction(bool isSilent = true)
            : base(isSilent)
        {
            xPathForNodes = GetxPathForNodes();
        }

        /// <summary>
        /// xPath 리스트
        /// </summary>
        /// <returns></returns>
        private List<XPathModel> GetxPathForNodes() => new List<XPathModel>()
            {
                // 1. 파라미터 1개
                // 타켓 Url -> http://mg.gmarket.co.kr/bestseller/category?catecode=0
                new XPathModel() { Alias = "type1_1", PageNodeXPath = "//*[@id='list_bx']/li/div/a/@href", NumNodeXPath = "//*[@id='list_bx']/li[{0}]/div/a/@href", ParamCount = 1 },

                // 타켓 Url -> http://mg.gmarket.co.kr/Category/List?lcId=100000003
                new XPathModel() { Alias = "type1_2", PageNodeXPath = "//*[@id='resultItem']/ul/li/div/a/@href", NumNodeXPath = "//*[@id='resultItem']/ul/li[{0}]/div/a/@href", ParamCount = 1 },

                // 타켓 Url -> http://mg.gmarket.co.kr/?pCache=2018050204320912575
                new XPathModel() { Alias = "type1_3", PageNodeXPath = "//*[@id='content']/div[3]/ul[2]/li/a/@href", NumNodeXPath = "//*[@id='content']/div[3]/ul[2]/li[{0}]/a/@href", ParamCount = 1 },

                // 2. 파라미터 2개
                // 타켓 Url -> http://mg.gmarket.co.kr/BestSeller/Minishop/ALL
                new XPathModel() { Alias = "type2_1", PageNodeXPath = "//*[@id='list_bx']/li/div/div/div[1]/div/ul/li/div/a/@href", NumNodeXPath = "//*[@id='list_bx']/li[{0}]/div/div/div[1]/div/ul/li[{1}]/div/a/@href", ParamCount = 2, ParamsStartNum = new List<int>() { 1, 1 }, ColLength = 5 },

                // 타켓 Url -> http://mg.gmarket.co.kr/Promotion/Plan?lcId=&sid=160045
                // 특이 케이스 페이지 : 첫번째 파라미터 : 그룹, 두번째 파라미터 : 그룹별 상품 순서 (몇개인지 알 수 없음)
                new XPathModel() { Alias = "type2_2", PageNodeXPath = "//*[@id='planItemGroup']/ul/li/div/a/@href", NumNodeXPath = "//*[@id='planItemGroup']/ul[{0}]/li[{1}]/div/a/@href", ParamCount = 2, ParamsStartNum = new List<int>() { 1, 1 } },
            };

        /// <summary>
        /// 페이지 단위 상품코드 수집
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        public List<DataModel> GetPageTypeGoods(int startNum, int endNum, Action<string> logFunc)
        {
            return base.GetPageTypeGoods(startNum, endNum, xPathForNodes, logFunc);
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
