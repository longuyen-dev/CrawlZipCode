using System;
using System.Text;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using HtmlAgilityPack;
namespace crawlerTest
{
    public class EmptyClass
    {
        public static void Main(string[] args)
        {
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8 // Set UTF8 to display vietnamese.
            };
            String url = "https://japan-postcode.810popo.net/saitamaken/yoshikawashi/3420037.html";
        }
    }
}
