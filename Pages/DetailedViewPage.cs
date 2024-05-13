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
    public class DetailedViewPage
    {
        private IWebDriver _driver;

        public DetailedViewPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"yDmH0d\"]/c-wiz[1]/div/div/div/div/div[1]/div/div[1]/div[1]/h5")]
        public IWebElement PriceCalculation;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"yDmH0d\"]/c-wiz[1]/div/div/div/div/div[2]/div[2]/div[1]/div[2]/div[3]/span[2]/span[1]/span[2]")]
        public IWebElement MachineTypeSummary;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"yDmH0d\"]/c-wiz[1]/div/div/div/div/div[2]/div[2]/div[1]/div[2]/div[4]/span[2]/span[1]/span[2]")]
        public IWebElement GPUModelSummary;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"yDmH0d\"]/c-wiz[1]/div/div/div/div/div[2]/div[2]/div[1]/div[2]/div[5]/span/span[1]/span[2]")]
        public IWebElement LocalSSDSummary;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"yDmH0d\"]/c-wiz[1]/div/div/div/div/div[2]/div[2]/div[1]/div[2]/div[7]/span/span[1]/span[2]")]
        public IWebElement NumberOfInstancesSummary;


        public void NewBrowserSwitch()
        {
            string mainWindowHandle = _driver.CurrentWindowHandle;

            List<string> allWindowHandles = new List<string>(_driver.WindowHandles);

            // Iterate over all window handles and switch to the new one
            foreach (string windowHandle in allWindowHandles)
            {
                if (windowHandle != mainWindowHandle)
                {
                    _driver.SwitchTo().Window(windowHandle);
                    break;
                }
            }
        }

    }
}