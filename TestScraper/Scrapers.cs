using TestScraper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestScraper
{
    public class Scraper
    {
        public List<Stock> Scrape()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\Users\klync\Downloads\geckodriver-v0.23.0-win32");
            service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";

            IWebDriver driver = new FirefoxDriver(service);
            driver.Url = "https://login.yahoo.com/?.src=finance&.intl=us&authMechanism=primary&done=https%3A%2F%2Ffinance.yahoo.com%2Fscreener%2Fpredefined&eid=100&add=1";

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);

            driver.FindElement(By.Id("login-username")).SendKeys("" + Keys.Enter);
            driver.FindElement(By.Id("login-passwd")).SendKeys("" + Keys.Enter);

            driver.FindElement(By.XPath("//a[contains(text(), 'My Portfolio')]")).Click();
            driver.FindElement(By.XPath("//*[@id='main']/section/section/div[2]/table/tbody/tr[2]/td[1]/a")).Click();

            IList<IWebElement> symbol = driver.FindElements(By.ClassName("_61PYt"));
            //Console.WriteLine("Info on stocks in Katelynn's Portfolio: " + stockNames.Count);

            List<Stock> stockList = new List<Stock>();

            for (int i = 0; i < symbol.Count; i++)
            {
                Stock stock = new Stock();
                stock.Symbol = symbol[i].Text;

                stockList.Add(stock);
            }

            driver.Close();
            return stockList;
        }
    }
}
