using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebController;

namespace GmarketMacro
{
    public class AdminAction : CommonAction
    {
        public AdminAction(bool isSilent = true)
            : base(isSilent)
        {
        }

        #region Admin에 번역 추가

        /// <summary>
        /// input 입력
        /// </summary>
        /// <param name="adminID"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        public bool SetInputValue(string selectText, string value, Action<string> logFunc)
        {
            var getPageSourceResult = webController.SetTextInputTag(ElementsSelectType.Id, selectText, value);
            if (!getPageSourceResult.ResultFlag)
                logFunc(getPageSourceResult.Err);

            return getPageSourceResult.ResultValue;
        }

        /// <summary>
        /// input value값 가져오기
        /// </summary>
        /// <param name="selectText"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        public string GetInputValue(string selectText, Action<string> logFunc)
        {
            var getPageSourceResult = webController.GetValueInputTag(ElementsSelectType.Id, selectText);
            if (!getPageSourceResult.ResultFlag)
                logFunc(getPageSourceResult.Err);

            return getPageSourceResult.ResultValue;
        }

        /// <summary>
        /// 버튼(태그) 클릭
        /// </summary>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        public bool ClickButton(string tagID, Action<string> logFunc)
        {
            var clickTagResult = webController.ClickTag(ElementsSelectType.Id, tagID);
            if (!clickTagResult.ResultFlag)
                logFunc(clickTagResult.Err);

            return clickTagResult.ResultValue;
        }

        /// <summary>
        /// Admin 페이지 상품 코드 정보 테이블 로우 더블클릭
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xPathText"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        public bool DoubleClickGoodsTableRow(string xPathText, Action<string> logFunc)
        {
            var doubleClickTagResult = webController.DoubleClickTag(ElementsSelectType.XPath, xPathText);
            if (!doubleClickTagResult.ResultFlag)
                logFunc(doubleClickTagResult.Err);

            return doubleClickTagResult.ResultValue;
        }

        /// <summary>
        /// alert 창 닫기
        /// </summary>
        /// <param name="alertMessage"></param>
        /// <param name="logFunc"></param>
        /// <returns></returns>
        public bool CloseAlertMessage(out string alertMessage, Action<string> logFunc)
        {
            var clickTagResult = webController.ClickAlert(out alertMessage);
            if (!clickTagResult.ResultFlag)
                logFunc(clickTagResult.Err);

            return clickTagResult.ResultValue;
        }

        #endregion
    }
}