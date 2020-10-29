using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class CarritoCompras
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.google.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheCarritoComprasTest()
        {
            driver.Navigate().GoToUrl("http://localhost:82/ecommerce/");
            driver.FindElement(By.LinkText("Products")).Click();
            driver.FindElement(By.Name("btnorder")).Click();
            Assert.AreEqual("1 Item added in the cart.", driver.FindElement(By.XPath("//section[@id='cart_items']/div/div[2]/label")).Text);
            driver.FindElement(By.LinkText("Products")).Click();
            driver.FindElement(By.XPath("(//button[@name='btnorder'])[2]")).Click();
            Assert.AreEqual("1 Item added in the cart.", driver.FindElement(By.XPath("//section[@id='cart_items']/div/div[2]/label")).Text);
            driver.FindElement(By.LinkText("Products")).Click();
            driver.FindElement(By.XPath("(//button[@name='btnorder'])[3]")).Click();
            Assert.AreEqual("1 Item added in the cart.", driver.FindElement(By.XPath("//section[@id='cart_items']/div/div[2]/label")).Text);
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
