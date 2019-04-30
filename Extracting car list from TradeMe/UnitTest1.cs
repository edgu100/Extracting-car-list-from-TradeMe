using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.IO;

namespace Extracting_car_list_from_TradeMe
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int totalModels = 0;
            int totalMakes = 0;
            IWebDriver driver = new ChromeDriver();
            TextWriter tw = new StreamWriter("C:/Users/edgu1/ListOfCars.txt");
            driver.Navigate().GoToUrl("http://www.trademe.co.nz/motors");
            IWebElement carMakeElement = driver.FindElement(By.Id("sidebar-Make"));
            SelectElement carMake = new SelectElement(carMakeElement);
           

            foreach (IWebElement selectMake in carMake.Options)
            { 
                carMake.SelectByText("Any make");
                if (selectMake.Text.Equals("Any make")) continue;
                totalMakes++;
                tw.WriteLine(selectMake.Text);
                carMake.SelectByText(selectMake.Text);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("15"))));
                IWebElement carModelElement = driver.FindElement(By.Id("15"));
                SelectElement carModel = new SelectElement(carModelElement);

                foreach (IWebElement m in carModel.Options)
                {
                    if (m.Text.Equals("Any model")) continue;
                    totalModels++;
                    tw.WriteLine("--" + m.Text);
                }
            }

            tw.WriteLine("----------------------Totals-------------------");
            tw.WriteLine("total number of makes" + totalMakes);
            tw.WriteLine("total number of models" + totalModels);
        }
    }
}
