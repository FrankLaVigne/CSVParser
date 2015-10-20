using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCSV_Parser_App
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

        public List<Dictionary<string, string>> Parse()
        {
            var parsedResult = new List<Dictionary<string, string>>();
            var records = RawText.Split(this.LineDelimiter);

            foreach (var record in records)
            {
                var fields = record.Split(this.Delimiter);
                var recordItem = new Dictionary<string, string>();
                var i = 0;

                foreach (var field in fields)
                {
                    recordItem.Add(i.ToString(), field);
                    i++;
                }

                parsedResult.Add(recordItem);
            }
            return parsedResult;
        }

    }
}
