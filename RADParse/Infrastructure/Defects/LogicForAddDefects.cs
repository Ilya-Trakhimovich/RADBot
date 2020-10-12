using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RADParse.Infrastructure.Defects
{
    public class LogicForAddDefects
    {
        private readonly List<int> _locations;

        public LogicForAddDefects()
        {
            _locations = new List<int>();
        }

        /// <summary>
        /// The method divides length of a street into equal sections
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public List<int> GetSections(int length, int start)
        {
            if (length <= 50)
            {
                var proportion = length / 2;
                var location = start + proportion;

                _locations.Add(location);
            }
            else if (length <= 150 & length > 50)
            {
                var proportion = length / 3;
                var location = start + proportion;

                for (var i = 0; i < 3; i++)
                {
                    _locations.Add(location);

                    location += proportion;                    
                }
            }
            else if (length <= 250 & length >150)
            {
                var proportion = length / 4;
                var location = start + proportion;

                for (var i = 0; i < 4; i++)
                {
                    _locations.Add(location);

                    location += proportion;                    
                }
            }
            else if (length <= 500 & length > 250)
            {
                var proportion = length / 5;
                var location = start + proportion;

                for (var i = 0; i < 5; i++)
                {
                    _locations.Add(location);

                    location += proportion;                    
                }
            }
            else if (length <= 1000 & length > 500)
            {
                var proportion = length / 6;
                var location = start + proportion;

                for (var i = 0; i < 6; i++)
                {
                    _locations.Add(location);

                    location += proportion;                    
                }
            }
            else if (length <= 2000 & length > 1000)
            {
                var proportion = length / 8;
                var location = start + proportion;

                for (var i = 0; i < 8; i++)
                {
                    _locations.Add(location);

                    location += proportion;                    
                }
            }
            else if (length <= 4000 & length > 2000)
            {
                var proportion = length / 15;
                var location = start + proportion;

                for (var i = 0; i < 15; i++)
                {
                    _locations.Add(location);

                    location += proportion;                    
                }
            }
            else if (length > 4000)
            {
                var proportion = length / 25;
                var location = start + proportion;

                for (var i = 0; i < 25; i++)
                {
                    _locations.Add(location);

                    location += proportion;                    
                }
            }

            return _locations;
        }
    }
}