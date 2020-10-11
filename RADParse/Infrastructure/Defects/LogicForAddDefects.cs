using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RADParse.Infrastructure.Defects
{
    public class LogicForAddDefects
    {
        private List<int> _locations;

        public LogicForAddDefects()
        {
            _locations = new List<int>();
        }

        /// <summary>
        /// The method divides length of a street into equal sections
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public List<int> GetSections(int length)
        {
            if (length <= 50)
            {
                var location = length / 2;

                _locations.Add(location);
            }
            else if (length <= 150 & length > 50)
            {
                var location = length / 3;

                for (var i = 1; i < 4; i++)
                {
                    _locations.Add(location * i);
                }
            }
            else if (length <= 250 & length >150)
            {
                var location = length / 4;

                for (var i = 1; i < 5; i++)
                {
                    _locations.Add(location * i);
                }
            }
            else if (length <= 500 & length > 250)
            {
                var location = length / 5;

                for (var i = 1; i < 6; i++)
                {
                    _locations.Add(location * i);
                }
            }
            else if (length <= 1000 & length > 500)
            {
                var location = length / 6;

                for (var i = 1; i < 7; i++)
                {
                    _locations.Add(location * i);
                }
            }
            else if (length <= 2000 & length > 1000)
            {
                var location = length / 8;

                for (var i = 1; i < 9; i++)
                {
                    _locations.Add(location * i);
                }
            }
            else if (length <= 4000 & length > 2000)
            {
                var location = length / 15;

                for (var i = 1; i < 16; i++)
                {
                    _locations.Add(location * i);
                }
            }
            else if (length > 4000)
            {
                var location = length / 25;

                for (var i = 1; i < 26; i++)
                {
                    _locations.Add(location * i);
                }
            }

            return _locations;
        }
    }
}