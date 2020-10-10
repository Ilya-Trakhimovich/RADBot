using Data.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using RADParse.Enum.DefectGroups;
using RADParse.Enum.Defects;
using RADParse.Infrastructure.Helper;
using System;
using System.Threading;

namespace RADParse.Infrastructure.Defects
{
    public class DefectsAddingMechanism
    {
        private IWebDriver _browser;
        private const string LOGIN = "radcentr2@mail.ru";
        private const string PASSWORD = "c3WCq42Q";
        private readonly LogicForAddDefects _logic;

        public DefectsAddingMechanism()
        {
            _browser = new ChromeDriver();
            _logic = new LogicForAddDefects();
        }

        public IWebDriver EnterToSystem()
        {
            _browser.Url = "http://178.124.171.41:44339/Streets/";
            _browser.FindElement(By.Id("Email")).SendKeys(LOGIN);
            _browser.FindElement(By.Id("Password")).SendKeys(PASSWORD);
            _browser.FindElement(By.CssSelector(".active.btn.shdw_cnt")).Submit();

            return _browser;
        }

        private void EnterToLog()
        {
            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _browser.FindElement(By.LinkText("Сбор данных сезонных осмотров")).Click();

            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _browser.FindElement(By.LinkText("Просмотр и редактирование дефектов")).Click();
        }

        private void SelectStreet(StreetRoad street)
        {
            try
            {
                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                _browser.FindElement(By.CssSelector(".streets-input.ui-autocomplete-input"));

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                _browser.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div[1]/input")).SendKeys(street.StreetName);

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                Actions act = new Actions(_browser);


                if (_browser.FindElement(By.CssSelector("li.ui-menu-item")).Enabled)
                {
                    act.MoveToElement(_browser.FindElement(By.CssSelector("li.ui-menu-item"))).Perform();
                }
                else
                {
                    CloseBrowser();
                }

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

                _browser.FindElement(By.Id("ui-active-menuitem")).Click();

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            catch
            {
                CloseBrowser();
            }
        }

        public void AddDefectsToRoads(StreetRoad street)
        {
            try
            {
                EnterToLog();
                SelectStreet(street);

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                var isStreetHasDefects = CheckForStreetDefects();

                if (!isStreetHasDefects)
                {
                    var sections = _logic.GetSections(street.StreetLenght);

                    if (street.StreetLenght < 100)
                    {
                        for (var i = 0; i < sections.Count; i++)
                        {
                            var rndCountOfDefects = new Random().Next(3, 6);
                            var rndDef = new RandomNumbers().GetListOfRndDefects(rndCountOfDefects);

                            for (int j = 0; j < rndCountOfDefects; j++)
                            {
                                _browser.FindElement(By.CssSelector(".shdw_cnt.btn")).Click();
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                _browser.FindElement(By.Id("LocationMeter")).SendKeys(sections[i].ToString());
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
                                _browser.FindElement(By.XPath("/html/body/div[3]/div[2]/form/div/table/tbody/tr[4]/td[2]/a/img")).Click();
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                                var defectGroups = _browser.FindElements(By.CssSelector("button.btn.btn-primary.btn-block"));
                                defectGroups[(int)DefectGroupsEnum.RoadPavement].Submit();
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                                var blocks = _browser.FindElements(By.CssSelector("div.select-block"));
                                blocks[rndDef[j]].Click();

                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);

                                if (rndDef[j] == (int)DefectsEnum.Sweating_1_8_1 | rndDef[j] == (int)DefectsEnum.Sweating_1_9_1)
                                {
                                    var rndValue = new Random().Next(30, 55);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Potholes_1_10_1 | rndDef[j] == (int)DefectsEnum.Potholes_1_10_2)
                                {
                                    var rndValue = new Random().Next(3, 10);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Patches_1_14_1 |
                                         rndDef[j] == (int)DefectsEnum.IrregularitiesPatch_1_15_1 |
                                         rndDef[j] == (int)DefectsEnum.IrregularitiesPatch_1_15_2)
                                {
                                    var rndValue = new Random().Next(20, 40);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Peeling_1_16_1 | rndDef[j] == (int)DefectsEnum.Chipping_1_17_1)
                                {
                                    var rndValue = new Random().Next(50, 70);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_1 |
                                         rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_2 |
                                         rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_3)
                                {
                                    var rndValue = new Random().Next(60, 100);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.RareCracks_1_19_1 |
                                         rndDef[j] == (int)DefectsEnum.RareCracks_1_19_2 |
                                         rndDef[j] == (int)DefectsEnum.RareCracks_1_19_3)
                                {
                                    var rndValue = new Random().Next(60, 100);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_1 |
                                         rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_2 |
                                         rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_3)
                                {
                                    var rndValue = new Random().Next(100, 150);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_1 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_2 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_3)
                                {
                                    var rndValue = new Random().Next(100, 150);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_1 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_2 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_3)
                                {
                                    var rndValue = new Random().Next(100, 150);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.DestructionOfSeams_1_27_1 |
                                         rndDef[j] == (int)DefectsEnum.CurbDefects_1_30_1)
                                {
                                    var rndValue = new Random().Next(10, 30);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_31_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_32_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_33_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_34_1
                                         )
                                {
                                    var rndValue = new Random().Next(10, 30);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }

                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

                                if (_browser.FindElement(By.CssSelector(".field-validation-error")) == null)
                                {
                                    _browser.FindElement(By.XPath("/html/body/div[3]/div[2]/form/div/div/button[2]")).Submit();
                                }
                            }
                        }
                    }
                    else
                    {
                        for (var i = 0; i < sections.Count; i++)
                        {
                            var rndCountOfDefects = new Random().Next(6, 10);
                            var rndDef = new RandomNumbers().GetListOfRndDefects(rndCountOfDefects);

                            for (int j = 0; j < rndCountOfDefects; j++)
                            {
                                _browser.FindElement(By.CssSelector(".shdw_cnt.btn")).Click();
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                _browser.FindElement(By.Id("LocationMeter")).SendKeys(sections[i].ToString());
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
                                _browser.FindElement(By.XPath("/html/body/div[3]/div[2]/form/div/table/tbody/tr[4]/td[2]/a/img")).Click();
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                                var defectGroups = _browser.FindElements(By.CssSelector("button.btn.btn-primary.btn-block"));
                                defectGroups[(int)DefectGroupsEnum.RoadPavement].Submit();
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                                var blocks = _browser.FindElements(By.CssSelector("div.select-block"));
                                blocks[rndDef[j]].Click();

                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);

                                if (rndDef[j] == (int)DefectsEnum.Sweating_1_8_1 | rndDef[j] == (int)DefectsEnum.Sweating_1_9_1)
                                {
                                    var rndValue = new Random().Next(30, 55);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Potholes_1_10_1 | rndDef[j] == (int)DefectsEnum.Potholes_1_10_2)
                                {
                                    var rndValue = new Random().Next(3, 10);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Patches_1_14_1 |
                                         rndDef[j] == (int)DefectsEnum.IrregularitiesPatch_1_15_1 |
                                         rndDef[j] == (int)DefectsEnum.IrregularitiesPatch_1_15_2)
                                {
                                    var rndValue = new Random().Next(20, 40);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Peeling_1_16_1 | rndDef[j] == (int)DefectsEnum.Chipping_1_17_1)
                                {
                                    var rndValue = new Random().Next(50, 70);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_1 |
                                         rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_2 |
                                         rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_3)
                                {
                                    var rndValue = new Random().Next(60, 100);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.RareCracks_1_19_1 |
                                         rndDef[j] == (int)DefectsEnum.RareCracks_1_19_2 |
                                         rndDef[j] == (int)DefectsEnum.RareCracks_1_19_3)
                                {
                                    var rndValue = new Random().Next(60, 100);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_1 |
                                         rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_2 |
                                         rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_3)
                                {
                                    var rndValue = new Random().Next(100, 150);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_1 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_2 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_3)
                                {
                                    var rndValue = new Random().Next(100, 150);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_1 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_2 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_3)
                                {
                                    var rndValue = new Random().Next(100, 150);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.DestructionOfSeams_1_27_1 |
                                         rndDef[j] == (int)DefectsEnum.CurbDefects_1_30_1)
                                {
                                    var rndValue = new Random().Next(10, 30);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_31_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_32_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_33_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_34_1
                                         )
                                {
                                    var rndValue = new Random().Next(10, 30);

                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }

                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch
            {
                CloseBrowser();
            }
        }

        public void CloseBrowser()
        {
            _browser.Quit();
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
