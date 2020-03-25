using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalLanguage.NN
{
    public class Config
    {
        public int Priority { get; set; } = 100;

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public bool IsImportingGraph { get; set; }
    }
}
