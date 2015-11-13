using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace CSVParserComponent
{
    public sealed class CsvParser
    {
        #region Private Constants

        private const char DEFAULT_DELIMITER = ',';
        private const char DEFAULT_QUOTE = '"';
        private const char DEFAULT_LINE_DELIMITER = '\n';

        #endregion

        #region Public Properties

        /// <summary>
        /// Field Delimiter Char
        /// </summary>
        public char Delimiter { get; set; }

        /// <summary>
        /// Line Delimiter Char
        /// </summary>
        public char LineDelimiter { get; set; }

        /// <summary>
        /// Quote Char
        /// </summary>
        public char Quote { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasHeaderRow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RawText { get; set; }

        /// <summary>
        /// Parser engine reference
        /// </summary>
        public IParserEngine ParserEngine { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAsyncOperation<IEnumerable<IDictionary<string, string>>> Parse()
        {
            return Task.Run<IEnumerable<IDictionary<string, string>>>(async () =>
            {
                var fieldList = new List<string>();

                if (this.HasHeaderRow)
                {
                    fieldList = LoadFieldNamesFromHeaderRow();
                }
                else
                {
                    // TODO: figure this out later
                }

                List<Dictionary<string, string>> results = await ParseRows(fieldList);

                return results;

            }).AsAsyncOperation();

        }

        #endregion

        #region Private Methods

        private void InitializeFields()
        {
            this.Delimiter = DEFAULT_DELIMITER;
            this.HasHeaderRow = true;
            this.RawText = string.Empty;
            this.Quote = DEFAULT_QUOTE;
        }

        private async Task<List<Dictionary<string, string>>> ParseRows(List<string> fieldList)
        {
            List<Dictionary<string, string>> valueList = new List<Dictionary<string, string>>();

            var rawTextLines = await this.ParserEngine.ExtractRecords('\n', this.RawText);

            int startingRow = 0;

            if (this.HasHeaderRow)
            {
                startingRow = 1;
            }

            for (int i = startingRow; i < rawTextLines.Count; i++)
            {
                var line = rawTextLines[i];

                var lineDictionary = await ParseLineIntoDictionary(fieldList, line);

                if (lineDictionary != null)
                {
                    valueList.Add(lineDictionary);
                }
            }


            return valueList;
        }

        private async Task<IList<string>> ParseLine(string rawTextLine)
        {
            var lineArray = await this.ParserEngine.ExtractFields(this.Delimiter, this.Quote, rawTextLine);

            return lineArray;
        }

        private async Task<Dictionary<string, string>> ParseLineIntoDictionary(List<string> fieldList, string rawTextLine)
        {
            var lineArray = await ParseLine(rawTextLine);

            if (lineArray.Count == fieldList.Count)
            {
                var j = 0;
                var rowDictionary = new Dictionary<string, string>();

                // Easy one to one mapping
                foreach (var fieldName in fieldList)
                {
                    rowDictionary.Add(fieldName, lineArray[j]);
                    j++;
                }

                return rowDictionary;
            }
            else
            {
                // TODO: handle this 
                return null;
            }
        }

        private List<string> LoadFieldNamesFromHeaderRow()
        {
            var fieldList = new List<string>();

            var firstLine = this.RawText.Split('\n').FirstOrDefault();

            if (firstLine != null)
            {
                // TODO: last field name gets a \n at the end. If a field is supposed to have whitespace, this could be bad
                fieldList = firstLine.TrimEnd().Split(this.Delimiter).ToList();
            }

            return fieldList;
        }

        #endregion


    }
}
