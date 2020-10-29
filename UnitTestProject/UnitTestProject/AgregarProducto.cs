using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestClass]
    public class AgregarProducto
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        private bool acceptNextAlert = true;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.google.com/";
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                //driver.Quit();// quit does not close the window
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [TestInitialize]
        public void InitializeTest()
        {
            driver.Navigate().GoToUrl("http://localhost:82/ecommerce/admin");
            driver.FindElement(By.Name("user_email")).Click();
            driver.FindElement(By.Name("user_email")).Clear();
            driver.FindElement(By.Name("user_email")).SendKeys("adminprueba");
            driver.FindElement(By.Name("user_pass")).Clear();
            driver.FindElement(By.Name("user_pass")).SendKeys("admin");
            driver.FindElement(By.Name("btnLogin")).Click();
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            driver.Navigate().GoToUrl("http://localhost:82/ecommerce/admin/products/");
            driver.FindElement(By.XPath("(//input[@id='selector[]'])[7]")).Click();
            driver.FindElement(By.Name("delete")).Click();
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void TheAgregarProductoTest()
        {
            driver.Navigate().GoToUrl("http://localhost:82/ecommerce/admin/products/");
            driver.FindElement(By.XPath("//div[@id='page-wrapper']/div/div/div/div/h1/a")).Click();
            driver.FindElement(By.Id("OWNERNAME")).Click();
            driver.FindElement(By.Id("OWNERNAME")).Clear();
            driver.FindElement(By.Id("OWNERNAME")).SendKeys("prueba");
            driver.FindElement(By.Id("OWNERPHONE")).Clear();
            driver.FindElement(By.Id("OWNERPHONE")).SendKeys("54545454");
            driver.FindElement(By.Id("PRODESC")).Click();
            driver.FindElement(By.Id("PRODESC")).Clear();
            driver.FindElement(By.Id("PRODESC")).SendKeys("Red blouse designer");
            driver.FindElement(By.Id("CATEGORY")).Click();
            new SelectElement(driver.FindElement(By.Id("CATEGORY"))).SelectByText("WOMENS");
            driver.FindElement(By.Id("CATEGORY")).Click();
            driver.FindElement(By.Id("ORIGINALPRICE")).Click();
            driver.FindElement(By.Id("ORIGINALPRICE")).Clear();
            driver.FindElement(By.Id("ORIGINALPRICE")).SendKeys("150");
            driver.FindElement(By.Id("PROPRICE")).Click();
            driver.FindElement(By.Id("PROPRICE")).Clear();
            driver.FindElement(By.Id("PROPRICE")).SendKeys("110");
            driver.FindElement(By.Id("PROQTY")).Click();
            driver.FindElement(By.Id("PROQTY")).Clear();
            driver.FindElement(By.Id("PROQTY")).SendKeys("20");
            IWebElement UploadImg = driver.FindElement(By.Id("image"));
            UploadImg.SendKeys("C:\\prueba.jpg");
            driver.FindElement(By.Name("save")).Click();

            Assert.AreEqual("New Product created successfully!", driver.FindElement(By.XPath("//div[@id='page-wrapper']/div/div/label")).Text);
            Assert.AreEqual("Red blouse designer", driver.FindElement(By.XPath("//table[@id='dash-table']/tbody/tr[7]/td[4]")).Text);

            driver.Navigate().GoToUrl("http://localhost:82/ecommerce");
            driver.FindElement(By.LinkText("Products")).Click();
            Assert.AreEqual("Red blouse designer", driver.FindElement(By.XPath("//form[6]/div/div/div/div/p")).Text);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
