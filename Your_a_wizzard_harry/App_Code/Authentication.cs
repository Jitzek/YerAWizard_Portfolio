using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.Data;

/// <summary>
/// 
/// </summary>
public class Authentication
{
    private Database db;
    private string mail;
    private string rank;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mail"></param>
    public Authentication(string mail)
    {
        dbConfig dbConfig = new dbConfig();
        db = dbConfig.GetDatabase();
        this.mail = mail;
        rank = "Owner";
        //this.rank = db.QuerySingle("SELECT Rank FROM Profile_Table WHERE Email = @0", mail);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsBlocked()
    {
        return false;
        //return db.QuerySingle("SELECT Is_Blocked FROM Profile_Table WHERE Email = @0", mail).Is_Blocked;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsStudent()
    {
        return (rank == "Student") ? true : false;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsTeacher()
    {
        return (rank == "Teacher") ? true : false;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsOwner()
    {
        return (rank == "Owner") ? true : false;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string[] getUser()
    {
        string[] test = new string[2];
        test[0] = "1";
        test[1] = "Henk";
        return test;
        //return db.QueryValue("SELECT Id, Username FROM Profile_Table WHERE Email = @0", mail);
    }
}
