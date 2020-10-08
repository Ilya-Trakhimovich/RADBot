using Data.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RADParse.Infrastructure.Defects
{
    public class DefectsAddingMechanism
    {
        private IWebDriver _browser;
        private const string LOGIN = "radcentr2@mail.ru";
        private const string PASSWORD = "c3WCq42Q";

        public DefectsAddingMechanism()
        {
            _browser = new ChromeDriver();
        }

        private IWebDriver EnterToSystem()
        {
            _browser.Url = "http://178.124.171.41:44339/Streets/";
            _browser.FindElement(By.Id("Email")).SendKeys(LOGIN);
            _browser.FindElement(By.Id("Password")).SendKeys(PASSWORD);
            _browser.FindElement(By.CssSelector(".active.btn.shdw_cnt")).Submit();

            return _browser;
        }

        private void EnterToLog()
        {
            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);

            _browser.FindElement(By.LinkText("Сбор данных сезонных осмотров")).Click();

            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);

            _browser.FindElement(By.LinkText("Просмотр и редактирование дефектов")).Click();
        }

        private void SelectStreet(StreetRoad street)
        {
            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);

            _browser.FindElement(By.CssSelector(".streets-input.ui-autocomplete-input")).SendKeys(street.StreetName);

            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(200);

            Actions act = new Actions(_browser);

            act.MoveToElement(_browser.FindElement(By.CssSelector("li.ui-menu-item"))).Perform();

            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _browser.FindElement(By.Id("ui-active-menuitem")).Click();

            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void AddDefectsToRoads(StreetRoad street)
        {
            EnterToLog();
            SelectStreet(street);

            var isStreetHasDefects = CheckForStreetDefects();

            if (!isStreetHasDefects)
            {
                _browser.FindElement(By.CssSelector(".shdw_cnt.btn")).Click();

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                _browser.FindElement(By.Id("LocationMeter")).SendKeys("250");

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                _browser.FindElement(By.Id("Volume")).SendKeys("25");

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
            }
            else
            {
                _browser.Navigate().Back();
            }
        }

        private bool CheckForStreetDefects()
        {
            try
            {
                var br = _browser.FindElement(By.CssSelector("tr.row"));

                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }
    }
}