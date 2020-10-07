using Data.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StreetConsole.Extensions.String;
using System;
using System.Collections.Generic;

namespace StreetConsole.Data.GetData
{
    public class WebDriverData
    {
        private const string EMAIL = "radcentr2@mail.ru";
        private const string PASSWORD = "c3WCq42Q";
        private readonly IWebDriver _browser;
        private readonly List<string> _streetData;
        private readonly List<StreetRoad> _streets;

        public List<StreetRoad> GetStreets
        {
            get
            {
                if (_streets == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    return _streets; ;
                }
            }
        }

        public WebDriverData()
        {
            _browser = new ChromeDriver();
            _streetData = new List<string>();
            _streets = new List<StreetRoad>();
        }

        private void EnterTheApp()
        {
            _browser.Url = "http://178.124.171.41:44339/Streets/";

            _browser.FindElement(By.Id("Email")).SendKeys(EMAIL);
            _browser.FindElement(By.Id("Password")).SendKeys(PASSWORD);
            _browser.FindElement(By.CssSelector(".active.btn.shdw_cnt")).Submit();
            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
        }

        public void GetListofStreets()
        {
            EnterTheApp();

            var list = _browser.FindElement(By.ClassName("treeSt")).FindElements(By.TagName("li"));

            foreach (var l in list)
            {
                _streetData.Add(l.Text);
            }
        }

        public void ConvertStringToStreets()
        {
            for (var i = 0; i < _streetData.Count; i++)
            {
                var roadElements = _streetData[i].SplitStringIntoRoadElements();
                var lengthOfStreet = roadElements[1].ConvertStringToInt();

                _streets.Add(
                    new StreetRoad()
                    {
                        StreetName = roadElements[0],
                        BeginOfStreet = lengthOfStreet[0],
                        EndOfStreet = lengthOfStreet[1],
                        StreetLenght = lengthOfStreet[1] - lengthOfStreet[0]
                    });
            }
        }

        public void PrintStreets()
        {
            foreach (var s in _streets)
            {
                Console.WriteLine(s.StreetName);
            }
        }
    }
}
