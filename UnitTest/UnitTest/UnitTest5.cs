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
    public class Reportes
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
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void TheReportesTest()
        {
            driver.Navigate().GoToUrl("http://localhost:82/ECommerce/admin/products/");
            driver.FindElement(By.LinkText("Report")).Click();
            driver.FindElement(By.Id("date_pickerfrom")).Click();
            driver.FindElement(By.XPath("//div[3]/table/thead/tr/th/i")).Click();
            driver.FindElement(By.XPath("//tr[5]/td")).Click();
            driver.FindElement(By.Id("date_pickerto")).Click();
            driver.FindElement(By.XPath("//div[3]/div[3]/table/thead/tr/th/i")).Click();
            driver.FindElement(By.XPath("//div[3]/div[3]/table/tbody/tr[5]/td[3]")).Click();
            driver.FindElement(By.Name("submit")).Click();
            Assert.AreEqual("Inclusive Dates: From : 09/28/2020 - To : 09/30/2020", driver.FindElement(By.XPath("//span[@id='printout']/div/div/div")).Text);
            driver.FindElement(By.XPath("//button[@onclick='tablePrint();']")).Click();
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