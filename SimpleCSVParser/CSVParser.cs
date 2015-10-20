using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCSVParser
{
    public class CSVParser
    {

        private const char DEFAULT_DELIMITER = ',';
        private const char DEFAULT_LINE_DELIMITER = '\n';

        public char Delimiter { get; set; }
        public char LineDelimiter { get; set; }
        public string RawText { get; set; }

        public CSVParser()
        {
            this.Delimiter = DEFAULT_DELIMITER;
            this.LineDelimiter = DEFAULT_LINE_DELIMITER;
        }


    }
}
