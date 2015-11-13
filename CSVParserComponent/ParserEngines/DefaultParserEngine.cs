using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace CSVParserComponent.ParserEngines
{
    /// <summary>
    /// Parser Engine that handles escaped fields
    /// </summary>
    public sealed class DefaultParserEngine : IParserEngine
    {
        #region Private Members

        private char _delimiter { get; set; }
        private char _quote { get; set; }

        #endregion

        #region IParserEngine methods


        public IAsyncOperation<IList<string>> ExtractFields(char delimiter, char quote, string csvLine)
        {

            return Task.Run<IList<string>>(async () =>
            {

                var fieldValues = new List<string>();

                this._delimiter = delimiter;
                this._quote = quote;

                if (csvLine != string.Empty)
                {
                    await AnalyzeField(fieldValues, csvLine, false);
                }

                var fieldValuesList = new List<string>(fieldValues);

                return fieldValuesList;


            }).AsAsyncOperation();


        }

        public IAsyncOperation<IList<string>> ExtractRecords(char lineDelimiter, string csvText)
        {

            return Task.Run<IList<string>>(async () => {


                var lines = csvText.Split(lineDelimiter);

                var linesList = new List<string>(lines);

                return linesList;

            
            }).AsAsyncOperation();


 
        }

        #endregion

        #region Private Methods

        private async Task AnalyzeField(List<string> lineArray, string text, bool insideQuotes)
        {
            var delimiterLocation = text.IndexOf(this._delimiter);
            var quoteLocation = text.IndexOf(this._quote.ToString());
            var endPoint = 0;


            var fieldValue = string.Empty;

            if (text.Length == 0 || (delimiterLocation == -1 && quoteLocation == -1))
            {

                if (text.Length > 0)
                {

                    // final field

                    // only thing left to do is capture last field

                    fieldValue = text.Substring(0).Replace("\n", string.Empty).Replace("\r", string.Empty);

                    lineArray.Add(fieldValue);
                }

                return;
            }

            if (quoteLocation == -1 || delimiterLocation < quoteLocation && delimiterLocation != -1 && insideQuotes == false)
            {
                //if (delimiterLocation == -1)
                //{
                //    fieldValue = text.Replace("\"", string.Empty).Replace("\r", string.Empty);

                //    lineArray.Add(fieldValue);

                //    AnalyzeField(lineArray, string.Empty, false);

                //}
                //else
                //{
                // delimiter found
                fieldValue = text.Substring(0, delimiterLocation);

                endPoint = delimiterLocation;
                //}
            }
            else if ((delimiterLocation > quoteLocation && insideQuotes == false) || delimiterLocation == -1)
            {
                if (quoteLocation == 0)
                {
                    // we've found start quote
                    var nextQuoteLocation = text.Substring(1).IndexOf(this._quote.ToString());



                    fieldValue = text.Substring(1, nextQuoteLocation);

                    endPoint = nextQuoteLocation + 2; // acounts for ", (vs just ,)


                }
                else
                {
                    fieldValue = text.Substring(0, quoteLocation);

                    endPoint = quoteLocation;
                }
            }


            if (fieldValue != string.Empty)
            {
                lineArray.Add(fieldValue);

                string workingString = text.Substring(endPoint + 1);

                AnalyzeField(lineArray, workingString, false);

            }




            return;


        }

        
        #endregion

    }
}
