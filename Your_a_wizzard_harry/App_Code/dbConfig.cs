using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.Data;

/// <summary>
/// Summary description for dbConfig
/// </summary>
public class dbConfig
{
    private Database db;
    public dbConfig()
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Harry_Potter.mdf;Integrated Security=True";
        string provider = "System.Data.SqlClient";
        db = Database.OpenConnectionString(connectionString, provider);
    }

    public Database GetDatabase()
    {
        return db;
    }
}