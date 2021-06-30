namespace SyntaxSolutions.PdfBuilder
{
   public class TextFontFamily
   {
        public string Value { get; set; }
        private TextFontFamily(string value) { Value = value; }
        public static TextFontFamily TimesNewRoman { get { return new TextFontFamily("Times New Roman"); } }
        public static TextFontFamily Arial { get { return new TextFontFamily("Arial"); } }
    }
}
