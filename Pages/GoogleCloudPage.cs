using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModel.Source.Pages
{
    public class GoogleCloudPage1
    {
        private IWebDriver _driver;

        public GoogleCloudPage1(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.CssSelector, Using = "a[href='https://cloud.google.com/pricing/']")]
        public IWebElement PricingLink;

        [FindsBy(How = How.ClassName, Using = "mb2a7b")]
        public IWebElement SearchInput;

        //[FindsBy(How = How.CssSelector, Using = "div.gs-title a.gs-title")]
        //public IWebElement ResultBox;



    }
}