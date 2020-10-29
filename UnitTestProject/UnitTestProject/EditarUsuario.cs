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
    public class EditarUsuario
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
            driver.FindElement(By.Name("user_email")).SendKeys("janobe");
            driver.FindElement(By.Name("user_pass")).Clear();
            driver.FindElement(By.Name("user_pass")).SendKeys("janobe1234");
            driver.FindElement(By.Name("btnLogin")).Click();
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            driver.FindElement(By.LinkText("Howdy, Janobe Palacios")).Click();
            driver.FindElement(By.LinkText("Edit My Profile")).Click();
            driver.FindElement(By.Id("U_PASS")).Click();
            driver.FindElement(By.Id("U_PASS")).Clear();
            driver.FindElement(By.Id("U_PASS")).SendKeys("janobe1234");
            driver.FindElement(By.Id("U_ROLE")).Click();
            new SelectElement(driver.FindElement(By.Id("U_ROLE"))).SelectByText("Administrator");
            driver.FindElement(By.Id("U_ROLE")).Click();
            driver.FindElement(By.Name("save")).Click();
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void TheEditarUsuarioTest()
        {
            driver.FindElement(By.LinkText("Users")).Click();
            driver.FindElement(By.XPath("//table[@id='dash-table']/tbody/tr[2]/td[4]/a/span")).Click();
            driver.FindElement(By.Id("U_PASS")).Click();
            driver.FindElement(By.Id("U_PASS")).Clear();
            driver.FindElement(By.Id("U_PASS")).SendKeys("janobe1234");
            driver.FindElement(By.Id("U_ROLE")).Click();
            new SelectElement(driver.FindElement(By.Id("U_ROLE"))).SelectByText("Staff");
            driver.FindElement(By.XPath("//option[@value='Staff']")).Click();
            driver.FindElement(By.Name("save")).Click();
            Assert.AreEqual("[Janobe Palacios] has been updated!", driver.FindElement(By.XPath("//div[@id='page-wrapper']/div/div/label")).Text);
            Assert.AreEqual("Staff", driver.FindElement(By.XPath("//table[@id='dash-table']/tbody/tr/td[3]")).Text);
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
