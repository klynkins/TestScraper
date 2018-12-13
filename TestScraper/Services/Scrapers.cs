using TestScraper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScraper.Services
{
    public class Scraper
    {
        public List<Stock> Scrape()
        {
            // Create a driver instance for chromedriver
            IWebDriver driver = new ChromeDriver(@"C:\Users\klync\Source\Repos\TestScraper");

            //Navigate to google page
            driver.Navigate().GoToUrl("https://login.yahoo.com/?.src=finance&.intl=us&authMechanism=primary&done=https%3A%2F%2Ffinance.yahoo.com%2Fscreener%2Fpredefined&eid=100&add=1");

            //Maximize the window
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);

            driver.FindElement(By.Id("login-username")).SendKeys("" + Keys.Enter);
            driver.FindElement(By.Id("login-passwd")).SendKeys("" + Keys.Enter);

            driver.FindElement(By.XPath("//a[contains(text(), 'My Portfolio')]")).Click();
            driver.FindElement(By.XPath("//tr[@data-key='p_0']//td[1]")).Click();

            //Find the Search text box using xpath
            IList<IWebElement> symbol = driver.FindElements(By.ClassName("_61PYt"));

            List<Stock> stockList = new List<Stock>();

            for (int i = 0; i < symbol.Count; i++)
            {
                Stock stock = new Stock
                {
                    Symbol = symbol[i].Text
                };
                stockList.Add(stock);
            }

            //Close the browser
            driver.Close();
            return stockList;
        }
    }
}