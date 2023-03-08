using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Net;
using HtmlAgilityPack;

string dbname = "database.db3";

Console.WriteLine("Введите поисковый запрос");
string input = Console.ReadLine();

Console.WriteLine("Введите количество страниц");
int pages = Convert.ToInt32(Console.ReadLine());

string[] urls = new string[pages];
for(int i = 0; i < pages; i++)
{
    urls[i] = $"https://www.ozon.ru/search/?from_global=true&page={i+1}&text={input}";
}

if (!File.Exists(dbname))
{
    SQLiteConnection.CreateFile(dbname);
}

using (var db = new SQLiteConnection("Data Source=" + dbname))
{
    string cmdtext = "CREATE TABLE if not exists Products(id int primary key, name text not null, price int not null, rate double not null, vars text not null, url text not null)";
    SQLiteCommand cmd = new SQLiteCommand(cmdtext, db);

    db.Open();
    cmd.ExecuteNonQuery();
}

var web = new HtmlWeb();
var doc = new HtmlDocument();
int id = 0;

for (int i = 0; i < pages;i++)
{
    for (int j = 0; j < 36;j++)
    {
        doc = web.Load(urls[i]);
        var coll = doc.DocumentNode.SelectSingleNode("//*[@id='layoutPage']").InnerText;
        Console.WriteLine(coll);
    }
}