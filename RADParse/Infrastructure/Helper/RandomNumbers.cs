﻿using System;
using System.Collections.Generic;

namespace RADParse.Infrastructure.Helper
{
    public class RandomNumbers
    {
        private  readonly List<int> _listOfDef;

        public RandomNumbers()
        {
            _listOfDef = new List<int>() { 18, 19, 20, 21, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 39, 40, 41, 43, 44, 45, 47, 48, 49, 51, 54, 55, 56, 57, 58 };
        }

        public List<int> GetListOfRndDefects(int count)
        {
            List<int> rndListOfDefForSection = new List<int>();
            var rndDef = new Random();

            for (var i = 0; i < count; i++)
            {
                var index = rndDef.Next(30 - i);

                rndListOfDefForSection.Add(_listOfDef[index]);
                _listOfDef.Remove(_listOfDef[index]);
            }

            return rndListOfDefForSection;
        }
    }
}