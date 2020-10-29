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
    public class ListaPedido
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
            driver.Navigate().GoToUrl("http://localhost:82/ecommerce/index.php?logout=1");
            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("U_USERNAME")).Click();
            driver.FindElement(By.Id("U_USERNAME")).Clear();
            driver.FindElement(By.Id("U_USERNAME")).SendKeys("clienteprueba");
            driver.FindElement(By.Id("U_PASS")).Clear();
            driver.FindElement(By.Id("U_PASS")).SendKeys("prueba123");
            driver.FindElement(By.Id("modalLogin")).Click();
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void TheListaPedidoTest()
        {
            driver.FindElement(By.LinkText("Account")).Click();
            driver.FindElement(By.XPath("//input[@type='search']")).Click();
            driver.FindElement(By.XPath("//input[@type='search']")).Clear();
            driver.FindElement(By.XPath("//input[@type='search']")).SendKeys("98");
            Assert.AreEqual("98", driver.FindElement(By.XPath("//table[@id='example']/tbody/tr/td[2]")).Text);
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
