namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// TextFontFamily
    /// </summary>
    public class TextFontFamily
   {
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        private TextFontFamily(string value) { Value = value; }

        /// <summary>
        /// TimesNewRoman
        /// </summary>
        public static TextFontFamily TimesNewRoman { get { return new TextFontFamily("Times New Roman"); } }

        /// <summary>
        /// Arial
        /// </summary>
        public static TextFontFamily Arial { get { return new TextFontFamily("Arial"); } }
    }
}
