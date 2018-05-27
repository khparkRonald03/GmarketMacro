using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmarketMacro
{
    public class WebController
    {
        ChromeDriverService Service = null;

        ChromeOptions Options = null;

        IWebDriver Driver = null;
        
        
        public string Url { get; set; }

        public bool IsSilentMode { get; } = false;

        /// <summary>
        /// 생성자 : 크롬 브라우저 보이도록 초기화
        /// </summary>
        public WebController()
        {
            Driver = new ChromeDriver();
        }

        /// <summary>
        /// 생성자 : 크롬 브라우저 보이지 않게 초기화
        /// </summary>
        /// <param name="isSilentMode"></param>
        public WebController(bool isSilentMode)
        {
            IsSilentMode = isSilentMode;

            if (isSilentMode)
            {
                Options = new ChromeOptions();
                Options.AddArgument("headless");

                Service = ChromeDriverService.CreateDefaultService();
                Service.HideCommandPromptWindow = true;

                Driver = new ChromeDriver(Service, Options);
            }
            else
            {
                Driver = new ChromeDriver();
            }
        }

        public bool SetUrl(string url)
        {
            try
            {
                Url = url;
                Driver.Url = Url;
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        #region 기본기능

        /// <summary>
        /// 단일 엘레멘트 가져오기
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private IWebElement FindElement(ElementsSelectType type, string name)
        {
            try
            {
                var by = GetBy(type, name);
                var webElement = Driver.FindElement(by);
                return webElement;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 멀티 엘레먼트 가져오기
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private ReadOnlyCollection<IWebElement> FindElements(ElementsSelectType type, string name)
        {
            try
            {
                var by = GetBy(type, name);
                var webElements = Driver.FindElements(by);
                return webElements;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 선택자 객체 가져오기
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private By GetBy(ElementsSelectType type, string name)
        {
            switch(type.ToString())
            {
                case "ClassName":
                    return By.ClassName(name);
                case "CssSelector":
                    return By.CssSelector(name);
                case "Id":
                    return By.Id(name);
                case "LinkText":
                    return By.LinkText(name);
                case "Name":
                    return By.Name(name);
                case "PartialLinkText":
                    return By.PartialLinkText(name);
                case "TagName":
                    return By.TagName(name);
                case "XPath":
                    return By.XPath(name);
                default:
                    return null;
            }
        }

        #endregion

        // 태그의 text 값 가져오기
        public string GetTagText(ElementsSelectType type, string tagName, out string tagText)
        {
            try
            {
                var tag = FindElement(type, tagName);
                tagText = tag.Text;
                return string.Empty;
            }
            catch (Exception e)
            {
                tagText = string.Empty;
                return e.Message;
            }
        }

        // input value값 가져오기
        public string GetValueInputTag(ElementsSelectType type, string tagName, out string inputValue)
        {
            try
            {
                var input = FindElement(type, tagName);
                inputValue = input.GetAttribute("value");
                return string.Empty;
            }
            catch (Exception e)
            {
                inputValue = string.Empty;
                return e.Message;
            }
        }

        // input에 텍스트 넣기 
        public string SetTextInputTag(ElementsSelectType type, string tagName, string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return string.Empty;

                var input = FindElement(type, tagName);
                if (input == null)
                    return "null";

                input.Clear();
                input.SendKeys(text);

                return string.Empty;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        // 클릭 (버튼, 테이블, 태그 등)
        public string ClickTag(ElementsSelectType type, string name)
        {
            try
            {
                var element = FindElement(type, name);
                if (element == null)
                    return "null";

                element.Click();

                return string.Empty;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        // alert 클릭
        public string ClickAlert(out string alertText)
        {
            try
            {
                alertText = Driver.SwitchTo().Alert().Text;
                Driver?.SwitchTo()?.Alert()?.Accept();

                return string.Empty;
            }
            catch(Exception e)
            {
                alertText = string.Empty;
                return e.Message;
            }
        }

        public void CloseDriver()
        {
            if(Driver != null)
            {
                Driver.Close();
                Driver.Dispose();
            }
        }
    }

    public enum ElementsSelectType
    {
        ClassName,
        CssSelector,
        Id,
        LinkText,
        Name,
        PartialLinkText,
        TagName,
        XPath,
    }
}
