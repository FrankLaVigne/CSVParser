using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



    }
}
