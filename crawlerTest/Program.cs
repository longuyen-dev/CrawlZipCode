using System;
using System.Text;
using System.Linq;
using System.IO;
using System.Collections.Generic;

using HtmlAgilityPack;

namespace crawlerTest
{
    class MainClass
    {
        HtmlWeb htmlWeb = new HtmlWeb()
        {
            AutoDetectEncoding = false,
            OverrideEncoding = Encoding.UTF8 // Set UTF8 to display vietnamese.
        };

        List<DataWeb> items = new List<DataWeb>();
        List<DataWeb> listShi = new List<DataWeb>();
        List<ListPostCode> listPostCodes = new List<ListPostCode>();

        public static void Main(string[] args)
        {
            String url = "https://japan-postcode.810popo.net/saitamaken/";
            //String url = "https://japan-postcode.810popo.net/saitamaken/yoshikawashi/3420037.html";



            MainClass mainClass = new MainClass();


            //mainClass.LoadPostCode(url);

            mainClass.LoadWebsite(url,0);

            foreach(DataWeb d in mainClass.items){
                Console.WriteLine(d.text);
                String newUrl = url + d.link;
                Console.WriteLine(newUrl);
                mainClass.LoadWebsite(newUrl, 1);



                foreach (DataWeb b in mainClass.listShi){
                    Console.WriteLine("---"+b.text);
                    String getPostLink = newUrl + b.link;

                    mainClass.LoadPostCode(getPostLink,b.text);

                }
                mainClass.listShi.Clear();
            }

            TextWriter tw = new StreamWriter("listPostCode.txt");
            foreach (ListPostCode l in mainClass.listPostCodes){
                tw.WriteLine("City: "+ l.City+ "- Post Code: "+l.PostCode);
            }
            tw.Close();



        }// Main
        void LoadWebsite(String url, int f){
            //Load website and put html to Document.
            HtmlDocument document = htmlWeb.Load(url);
            var listUl = document.DocumentNode.SelectNodes("//div[@class='links']/ul/li").ToList();


            foreach (var item in listUl)
            {
                var linkNode = item.SelectSingleNode(".//a");
                var text = linkNode.InnerText;
                var link = linkNode.Attributes["href"].Value;
                String newUrl = url + "/" + link;
                if(f == 0){
                    items.Add(new DataWeb(link, text));
                }else{
                    listShi.Add(new DataWeb(link, text));
                }

            }
        }//Load website
        void LoadPostCode(String url,String city){
            HtmlDocument document = htmlWeb.Load(url);
            var listDiv = document.DocumentNode.SelectSingleNode("//div[@class='address-onebyone']/p");

            var postCode = listDiv.InnerText;
            Console.WriteLine("Post Code: "+postCode);
            listPostCodes.Add(new ListPostCode(city, postCode));
        }


    }
    class DataWeb
    {
        public String link, text;
        public DataWeb(String l, String t){
            link = l;
            text = t;
        }
    }
    class ListPostCode{
        public String City, PostCode;
        public ListPostCode(String c, String p){
            City = c;
            PostCode = p;
        }
    }
}
