using Microsoft.VisualStudio.TestTools.UnitTesting;
using Html_Parser;
using System;
using AngleSharp;
using System.IO;
namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public static string mainString;
        [TestMethod]
        public void TestMethod1()
        {
            
            string code = startProgram();
            //System.Console.WriteLine(code);
            IGeneralServices _generalServices = new GeneralServices(mainString);
            _generalServices.parser(code);
            string output = _generalServices.Show();
            output = BeautifyText(output);
            Assert.AreEqual(mainString, output);
        }
        static string startProgram()
        {
           
            mainString = System.IO.File.ReadAllText("../../../input.txt");
            mainString = BeautifyText(mainString);
            string text = System.IO.File.ReadAllText("../../../input.txt")
                .Replace(Environment.NewLine, "");
            /// transform the input in a single line
            
            return text;

        }
        public static string BeautifyText(string input)
        {
            var parser = new AngleSharp.Html.Parser.HtmlParser();  
            var document = parser.ParseDocument(input);
            var sw = new StringWriter();
            document.ToHtml(sw, new AngleSharp.Html.PrettyMarkupFormatter());
            return sw.ToString();
        }
    }
}
    

    

