using System;

namespace ListViewSample
{
    public class Person
    {
        public string Name { get; set; }

        public string Description
        {
            get
            {
                return string.Format("{0}'s description", Name);
            }
        }

        public int ImageResourceId { get; set; }

        public string ImageResourceFileName { get; set; }
    }
}

