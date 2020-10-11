using Data.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using RADParse.Enum.DefectGroups;
using RADParse.Enum.Defects;
using RADParse.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace RADParse.Infrastructure.Defects
{
    public class DefectsAddingMechanism
    {
        private IWebDriver _browser;
        private const string LOGIN = "radcentr2@mail.ru";
        private const string PASSWORD = "c3WCq42Q";
        private readonly LogicForAddDefects _logic;
        private readonly Actions _act;

        public DefectsAddingMechanism()
        {
            _browser = new ChromeDriver();
            _logic = new LogicForAddDefects();
            _act = new Actions(_browser);
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

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                if (_browser.FindElement(By.CssSelector("li.ui-menu-item")).Enabled)
                {
                    _act.MoveToElement(_browser.FindElement(By.CssSelector("li.ui-menu-item"))).Perform();
                }
                else
                {
                    CloseBrowser();
                }

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                _browser.FindElement(By.Id("ui-active-menuitem")).Click();

                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            }
            catch
            {
                CloseBrowser();
            }
        }

        /// <summary>
        /// The method doesnt work. Not all sections are filled with defects
        /// </summary>
        /// <param name="street"></param>
        // private void AddHelper(List<int> sections, int startIndex, int endIndex)
        //{
        //    for (var i = 0; i < sections.Count; i++)
        //    {
        //        Thread.Sleep(100);
        //        var rndCountOfDefects = new Random().Next(startIndex, endIndex);
        //        var rndDef = new RandomNumbers().GetListOfRndDefects(rndCountOfDefects);

        //        for (int j = 0; j < rndCountOfDefects; j++)
        //        {
        //            Thread.Sleep(100);

        //            _browser.FindElement(By.CssSelector(".shdw_cnt.btn")).Click();
        //            Thread.Sleep(100);
        //            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //            _browser.FindElement(By.Id("LocationMeter")).SendKeys(sections[i].ToString());
        //            Thread.Sleep(100);
        //            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //            _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
        //            Thread.Sleep(100);
        //            _browser.FindElement(By.XPath("/html/body/div[3]/div[2]/form/div/table/tbody/tr[4]/td[2]/a/img")).Click();
        //            Thread.Sleep(100);
        //            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        //            var defectGroups = _browser.FindElements(By.CssSelector("button.btn.btn-primary.btn-block"));
        //            Thread.Sleep(100);
        //            defectGroups[(int)DefectGroupsEnum.RoadPavement].Submit();
        //            Thread.Sleep(100);
        //            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        //            var blocks = _browser.FindElements(By.CssSelector("div.select-block"));
        //            Thread.Sleep(100);
        //            blocks[rndDef[j]].Click();
        //            Thread.Sleep(100);

        //            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);

        //            if (rndDef[j] == (int)DefectsEnum.Sweating_1_8_1 | rndDef[j] == (int)DefectsEnum.Sweating_1_9_1)
        //            {
        //                var rndValue = new Random().Next(20, 45);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }
        //            else if (rndDef[j] == (int)DefectsEnum.Potholes_1_10_1 | rndDef[j] == (int)DefectsEnum.Potholes_1_10_2)
        //            {
        //                var rndValue = new Random().Next(3, 8);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }
        //            else if (rndDef[j] == (int)DefectsEnum.Patches_1_14_1 |
        //                     rndDef[j] == (int)DefectsEnum.IrregularitiesPatch_1_15_1 |
        //                     rndDef[j] == (int)DefectsEnum.IrregularitiesPatch_1_15_2)
        //            {
        //                var rndValue = new Random().Next(20, 40);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }
        //            else if (rndDef[j] == (int)DefectsEnum.Peeling_1_16_1 | rndDef[j] == (int)DefectsEnum.Chipping_1_17_1)
        //            {
        //                var rndValue = new Random().Next(30, 60);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }
        //            else if (rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_1 |
        //                     rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_2 |
        //                     rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_3)
        //            {
        //                var rndValue = new Random().Next(30, 70);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }
        //            else if (rndDef[j] == (int)DefectsEnum.RareCracks_1_19_1 |
        //                     rndDef[j] == (int)DefectsEnum.RareCracks_1_19_2 |
        //                     rndDef[j] == (int)DefectsEnum.RareCracks_1_19_3)
        //            {
        //                var rndValue = new Random().Next(60, 100);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }
        //            else if (rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_1 |
        //                     rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_2 |
        //                     rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_3)
        //            {
        //                var rndValue = new Random().Next(60, 100);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }
        //            else if (rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_1 |
        //                     rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_2 |
        //                     rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_3)
        //            {
        //                var rndValue = new Random().Next(60, 100);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }
        //            else if (rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_1 |
        //                     rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_2 |
        //                     rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_3)
        //            {
        //                var rndValue = new Random().Next(60, 110);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }
        //            else if (rndDef[j] == (int)DefectsEnum.DestructionOfSeams_1_27_1 |
        //                     rndDef[j] == (int)DefectsEnum.CurbDefects_1_30_1)
        //            {
        //                var rndValue = new Random().Next(10, 20);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }
        //            else if (rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_31_1 |
        //                     rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_32_1 |
        //                     rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_33_1 |
        //                     rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_34_1
        //                     )
        //            {
        //                var rndValue = new Random().Next(10, 20);
        //                Thread.Sleep(100);
        //                _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
        //            }

        //            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //            Thread.Sleep(100);
        //            _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
        //            Thread.Sleep(100);
        //            _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        //        }
        //    }
        //}

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

                    if (street.StreetLenght < 200)
                    {
                        // AddHelper(sections, 2, 5); // the method doesnt work - not all sections are filled with defects

                        for (var i = 0; i < sections.Count; i++)
                        {
                            Thread.Sleep(100);
                            var rndCountOfDefects = new Random().Next(2, 5);
                            var rndDef = new RandomNumbers().GetListOfRndDefects(rndCountOfDefects);

                            for (int j = 0; j < rndCountOfDefects; j++)
                            {
                                Thread.Sleep(100);

                                _browser.FindElement(By.CssSelector(".shdw_cnt.btn")).Click();
                                Thread.Sleep(100);
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                _browser.FindElement(By.Id("LocationMeter")).SendKeys(sections[i].ToString());
                                Thread.Sleep(100);
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
                                Thread.Sleep(100);
                                _browser.FindElement(By.XPath("/html/body/div[3]/div[2]/form/div/table/tbody/tr[4]/td[2]/a/img")).Click();
                                Thread.Sleep(100);
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                                var defectGroups = _browser.FindElements(By.CssSelector("button.btn.btn-primary.btn-block"));
                                Thread.Sleep(100);
                                defectGroups[(int)DefectGroupsEnum.RoadPavement].Submit();
                                Thread.Sleep(100);
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                                var blocks = _browser.FindElements(By.CssSelector("div.select-block"));
                                Thread.Sleep(100);
                                blocks[rndDef[j]].Click();
                                Thread.Sleep(100);

                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);

                                if (rndDef[j] == (int)DefectsEnum.Sweating_1_8_1 | rndDef[j] == (int)DefectsEnum.Sweating_1_9_1)
                                {
                                    var rndValue = new Random().Next(20, 45);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Potholes_1_10_1 | rndDef[j] == (int)DefectsEnum.Potholes_1_10_2)
                                {
                                    var rndValue = new Random().Next(3, 8);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Patches_1_14_1 |
                                         rndDef[j] == (int)DefectsEnum.IrregularitiesPatch_1_15_1 |
                                         rndDef[j] == (int)DefectsEnum.IrregularitiesPatch_1_15_2)
                                {
                                    var rndValue = new Random().Next(20, 40);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Peeling_1_16_1 | rndDef[j] == (int)DefectsEnum.Chipping_1_17_1)
                                {
                                    var rndValue = new Random().Next(30, 60);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_1 |
                                         rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_2 |
                                         rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_3)
                                {
                                    var rndValue = new Random().Next(30, 70);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.RareCracks_1_19_1 |
                                         rndDef[j] == (int)DefectsEnum.RareCracks_1_19_2 |
                                         rndDef[j] == (int)DefectsEnum.RareCracks_1_19_3)
                                {
                                    var rndValue = new Random().Next(60, 100);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_1 |
                                         rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_2 |
                                         rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_3)
                                {
                                    var rndValue = new Random().Next(60, 100);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_1 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_2 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_3)
                                {
                                    var rndValue = new Random().Next(60, 100);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_1 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_2 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_3)
                                {
                                    var rndValue = new Random().Next(60, 110);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.DestructionOfSeams_1_27_1 |
                                         rndDef[j] == (int)DefectsEnum.CurbDefects_1_30_1)
                                {
                                    var rndValue = new Random().Next(10, 20);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_31_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_32_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_33_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_34_1
                                         )
                                {
                                    var rndValue = new Random().Next(10, 20);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }

                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                Thread.Sleep(100);
                                _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
                                Thread.Sleep(100);
                                _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                            }
                        }
                    }
                    else
                    {
                        //  AddHelper(sections, 4, 9); // the method doesnt work - not all sections are filled with defects

                        for (var i = 0; i < sections.Count; i++)
                        {
                            Thread.Sleep(100);
                            var rndCountOfDefects = new Random().Next(4, 8);
                            var rndDef = new RandomNumbers().GetListOfRndDefects(rndCountOfDefects);

                            for (int j = 0; j < rndCountOfDefects; j++)
                            {
                                Thread.Sleep(100);
                                _browser.FindElement(By.CssSelector(".shdw_cnt.btn")).Click();
                                Thread.Sleep(100);
                                //_browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                _browser.FindElement(By.Id("LocationMeter")).SendKeys(sections[i].ToString());
                                Thread.Sleep(100);
                                //_browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
                                Thread.Sleep(100);
                                _browser.FindElement(By.XPath("/html/body/div[3]/div[2]/form/div/table/tbody/tr[4]/td[2]/a/img")).Click();
                                Thread.Sleep(100);
                                //_browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                                var defectGroups = _browser.FindElements(By.CssSelector("button.btn.btn-primary.btn-block"));
                                Thread.Sleep(100);
                                defectGroups[(int)DefectGroupsEnum.RoadPavement].Submit();
                                Thread.Sleep(100);
                                //_browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                                var blocks = _browser.FindElements(By.CssSelector("div.select-block"));
                                Thread.Sleep(100);
                                blocks[rndDef[j]].Click();
                                Thread.Sleep(100);
                                // _browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);

                                if (rndDef[j] == (int)DefectsEnum.Sweating_1_8_1 | rndDef[j] == (int)DefectsEnum.Sweating_1_9_1)
                                {
                                    var rndValue = new Random().Next(20, 45);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Potholes_1_10_1 | rndDef[j] == (int)DefectsEnum.Potholes_1_10_2)
                                {
                                    var rndValue = new Random().Next(3, 8);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Patches_1_14_1 |
                                         rndDef[j] == (int)DefectsEnum.IrregularitiesPatch_1_15_1 |
                                         rndDef[j] == (int)DefectsEnum.IrregularitiesPatch_1_15_2)
                                {
                                    var rndValue = new Random().Next(20, 40);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.Peeling_1_16_1 | rndDef[j] == (int)DefectsEnum.Chipping_1_17_1)
                                {
                                    var rndValue = new Random().Next(30, 60);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_1 |
                                         rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_2 |
                                         rndDef[j] == (int)DefectsEnum.IndividualCracks_1_18_3)
                                {
                                    var rndValue = new Random().Next(30, 70);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.RareCracks_1_19_1 |
                                         rndDef[j] == (int)DefectsEnum.RareCracks_1_19_2 |
                                         rndDef[j] == (int)DefectsEnum.RareCracks_1_19_3)
                                {
                                    var rndValue = new Random().Next(60, 100);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_1 |
                                         rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_2 |
                                         rndDef[j] == (int)DefectsEnum.FrequentCracks_1_21_3)
                                {
                                    var rndValue = new Random().Next(60, 100);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_1 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_2 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_23_3)
                                {
                                    var rndValue = new Random().Next(60, 100);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_1 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_2 |
                                         rndDef[j] == (int)DefectsEnum.CrackMesh_1_25_3)
                                {
                                    var rndValue = new Random().Next(60, 110);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.DestructionOfSeams_1_27_1 |
                                         rndDef[j] == (int)DefectsEnum.CurbDefects_1_30_1)
                                {
                                    var rndValue = new Random().Next(10, 20);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }
                                else if (rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_31_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_32_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_33_1 |
                                         rndDef[j] == (int)DefectsEnum.TechnologicalDefect_1_34_1
                                         )
                                {
                                    var rndValue = new Random().Next(10, 20);
                                    Thread.Sleep(100);
                                    _browser.FindElement(By.Id("Volume")).SendKeys(rndValue.ToString());
                                }

                                //_browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                                Thread.Sleep(100);
                                _browser.FindElement(By.CssSelector(".imgLink.shdw_cnt")).Submit();
                                Thread.Sleep(100);
                                //_browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
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
