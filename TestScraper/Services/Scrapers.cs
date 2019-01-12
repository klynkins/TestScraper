using TestScraper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestScraper.Services
{
    public class Scraper
    {
        public List<Stock> Scrape()
        {
            var chromeDriver = new ChromeDriver("C:\\Users\\klync\\Source\\Repos\\TestScraper");

            chromeDriver.Navigate().GoToUrl("https://login.yahoo.com");
            chromeDriver.Manage().Window.Maximize();

            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            chromeDriver.FindElement(By.Id("login-username")).SendKeys("" + Keys.Enter);
            chromeDriver.FindElement(By.Id("login-passwd")).SendKeys("" + Keys.Enter);

            // navigate to my portfolio page
            chromeDriver.Url = "https://finance.yahoo.com/portfolio/p_0/view/v1";

            // close pop-up alert
            var closePopup = chromeDriver.FindElementByXPath("//dialog[@id = '__dialog']/section/button");
            closePopup.Click();

            IWebElement list = chromeDriver.FindElementByTagName("tbody");
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> stocks = list.FindElements(By.TagName("tr"));
            int count = stocks.Count();

            List<Stock> stockList = new List<Stock>();
            for (int i = 1; i <= count; i++)
            {
                var symbol = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[1]/span/a").GetAttribute("innerText");
                
                Stock stock = new Stock();
                stock.Symbol = symbol;
                Console.WriteLine(stock);
                stockList.Add(stock);
                
            }
            return stockList;
        }
    }
}