using log4net;
using log4net.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using PageObjectModel.Source.Pages;
using SeleniumExtras.PageObjects;
using GoogleCloudPage.Driver;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Drawing.Imaging;


namespace WebDriver_Framework_task_1_nunit_
{
    namespace PageObjectModel.Tests
    {
        public class HomeTests
        {
            protected ExtentReports extent;
            protected ExtentTest test;
            private IWebDriver _driver;
            private GoogleCloudPage1 googleCloudPage;
            private ComputeEnginePage computeEnginePage;
            private DetailedViewPage detailedViewPage;
            private static readonly ILog log = LogManager.GetLogger(typeof(HomeTests));

            [SetUp]
            public void Setup()
            {
                

                _driver = new ChromeDriver();
                _driver.Manage().Window.Maximize();
                _driver.Navigate().GoToUrl("https://cloud.google.com/");

                googleCloudPage = new GoogleCloudPage1(_driver);
                computeEnginePage = new ComputeEnginePage(_driver);
                detailedViewPage = new DetailedViewPage(_driver);
            }


            [Test]

            public void GoogleCloudCreation()
            {
                var extent = new ExtentReports();
                var spark = new ExtentSparkReporter("Spark.html");
                //var htmlReporter = new ExtentHtmlReporter("TestReport.html");
                extent.AttachReporter(spark);
                extent.CreateTest("MyFirstTest")
                .Log(Status.Pass, "This is a logging event for MyFirstTest, and it passed!");
                //test = extent.CreateTest("GoogleCloudCreation"); // Start test in ExtentReports
                log.Info("Test execution started.");

                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                googleCloudPage.PricingLink.Click();

                googleCloudPage.SearchInput.Click();
                googleCloudPage.SearchInput.SendKeys("Google Cloud Platform Pricing Calculator" + Keys.Enter);

                var links = _driver.FindElements(By.CssSelector("div.gs-title a.gs-title"));
                IWebElement specificLink = links.FirstOrDefault(link => link.Text == "Google Cloud Pricing Calculator");

                if (specificLink != null)
                {                   
                    specificLink.Click();
                }
                else
                { 
                    Assert.Fail();
                }


                computeEnginePage.AddToEstimateBtn.Click();

                computeEnginePage.ComputeEngine.Click();

                computeEnginePage.CookieBtnOk.Click();

                computeEnginePage.InstanceNumber.Clear();
                computeEnginePage.InstanceNumber.SendKeys("4");

                computeEnginePage.MachineType.Click();
                _driver.FindElement(By.XPath("//li[@data-value='n1-standard-8']")).Click();

                computeEnginePage.AddGPUsButton.Click();

                computeEnginePage.GPUModel.Click();
                _driver.FindElement(By.XPath("//*[@id=\"ow4\"]/div/div/div/div/div/div/div[1]/div/div[2]/div[3]/div[23]/div/div[1]/div/div/div/div[2]/ul/li[3]")).Click();
                Thread.Sleep(1000);

                computeEnginePage.LocalSSD.Click();
                _driver.FindElement(By.XPath("//*[@id=\"ow4\"]/div/div/div/div/div/div/div[1]/div/div[2]/div[3]/div[27]/div/div[1]/div/div/div/div[2]/ul/li[3]")).Click();
                Thread.Sleep(1000);


                computeEnginePage.OneYearCommittment.Click();

                computeEnginePage.OpenDetailedView.Click();

                detailedViewPage.NewBrowserSwitch();
                               
                Assert.Multiple(() =>
                {
                    Assert.That(detailedViewPage.PriceCalculation.Text, Is.EqualTo("Total estimated cost"));
                    Assert.That(detailedViewPage.MachineTypeSummary.Text.Contains("n1-standard-8"));
                    Assert.That(detailedViewPage.GPUModelSummary.Text, Is.EqualTo("NVIDIA Tesla V100"));
                    Assert.That(detailedViewPage.LocalSSDSummary.Text, Is.EqualTo("2x375 GB"));
                    Assert.That(detailedViewPage.NumberOfInstancesSummary.Text, Is.EqualTo("4"));
                });
                extent.Flush();
            }

            [TearDown]
            public void Cleanup()
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    // Capture screenshot on test failure
                    string screenshotPath = $"{DateTime.Now:yyyyMMddHHmmssfff}_failure.png";
                    Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                    screenshot.SaveAsFile(screenshotPath); // Save screenshot as PNG

                    // Add screenshot to HTML report
                    test.AddScreenCaptureFromPath(screenshotPath);
                }

                _driver.Quit();
                
            }
        }
    }
}