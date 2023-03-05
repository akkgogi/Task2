using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using AngleSharp;
using System.Net;


string dbname = "database.db3";

if (!File.Exists(dbname))
{
    SQLiteConnection.CreateFile(dbname);
}

using (var db = new SQLiteConnection("Data Source=" + dbname))
{
    string cmdtext = "CREATE TABLE Products(id int primary key, name text not null, price int not null, rate double not null";
    SQLiteCommand cmd = new SQLiteCommand(cmdtext, db);

    db.Open();
    cmd.ExecuteNonQuery();
}

