using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.Data;

/// <summary>
/// Summary description for Functions
/// </summary>
public class Functions
{
    private Database db;
    public Functions()
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Harry_Potter.mdf;Integrated Security=True";
        string provider = "System.Data.SqlClient";
        db = Database.OpenConnectionString(connectionString, provider);
    }

    /// <summary>
    /// Updates/Inserts upvotes and downvotes into the database
    /// </summary>
    /// <param name="voter">Username of voter</param>
    /// <param name="type">Type of content being voted</param>
    /// <param name="is_voted_as">Current vote of current user on current content</param>
    /// <param name="vote">New vote of current user</param>
    /// <param name="Forum_Id">The Id of the forum where the content is posted</param>
    /// <param name="Post_Id">The Id of the post where the content is posted</param>
    /// <param name="Comment_Id">The Id of the comment where the content is posted</param>
    /// <param name="Reply_Id">The Id of the reply where the content is posted</param>
    public void Vote(string voter, string type, string is_voted_as, string vote, int Forum_Id, int Post_Id, int Comment_Id, int Reply_Id)
    {
        if (type != "Post" && type != "Comment" && type != "Reply")
        {
            return;
        }

        switch (vote)
        {
            case "Upvote":
                switch (is_voted_as)
                {
                    case "Upvoted":
                        if (type == "Post")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Post_Post_Id = @1 AND Comment_Comment_Id IS NULL AND Username_Username = @2", "Retracted", Post_Id, voter);
                            db.Execute("UPDATE Post SET Upvote = Upvote - 1 WHERE Post_Id = @0", Post_Id);
                            db.Close();
                        }
                        else if (type == "Comment")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Comment_Comment_Id = @1 AND Reply_Reply_Id IS NULL AND Username_Username = @2", "Retracted", Comment_Id, voter);
                            db.Execute("UPDATE Comments SET Upvote = Upvote - 1 WHERE Comment_Id = @0", Comment_Id);
                            db.Close();
                        }
                        else if (type == "Reply")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Reply_Reply_Id = @1 AND Username_Username = @2", "Retracted", Reply_Id, voter);
                            db.Execute("UPDATE Replies SET Upvote = Upvote - 1 WHERE Reply_Id = @0", Reply_Id);
                            db.Close();
                        }
                        break;

                    case "Downvoted":
                        if (type == "Post")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Post_Post_Id = @1 AND Comment_Comment_Id IS NULL AND Username_Username = @2", "Upvoted", Post_Id, voter);
                            db.Execute("UPDATE Post SET Downvote = Downvote - 1, Upvote = Upvote + 1 WHERE Post_Id = @0", Post_Id);
                            db.Close();
                        }
                        else if (type == "Comment")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Comment_Comment_Id = @1 AND Reply_Reply_Id IS NULL AND Username_Username = @2", "Upvoted", Comment_Id, voter);
                            db.Execute("UPDATE Comments SET Upvote = Upvote + 1, Downvote = Downvote - 1 WHERE Comment_Id = @0", Comment_Id);
                            db.Close();
                        }
                        else if (type == "Reply")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Reply_Reply_Id = @1 AND Username_Username = @2", "Upvoted", Reply_Id, voter);
                            db.Execute("UPDATE Replies SET Upvote = Upvote + 1, Downvote = Downvote - 1 WHERE Reply_Id = @0", Reply_Id);
                            db.Close();
                        }
                        break;

                    case "Retracted":
                        if (type == "Post")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Post_Post_Id = @1 AND Comment_Comment_Id IS NULL AND Username_Username = @2", "Upvoted", Post_Id, voter);
                            db.Execute("UPDATE Post SET Upvote = Upvote + 1 WHERE Post_Id = @0", Post_Id);
                            db.Close();
                        }
                        else if (type == "Comment")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Comment_Comment_Id = @1 AND Reply_Reply_Id IS NULL AND Username_Username = @2", "Upvoted", Comment_Id, voter);
                            db.Execute("UPDATE Comments SET Upvote = Upvote + 1 WHERE Comment_Id = @0", Comment_Id);
                            db.Close();
                        }
                        else if (type == "Reply")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Reply_Reply_Id = @1 AND Username_Username = @2", "Upvoted", Reply_Id, voter);
                            db.Execute("UPDATE Replies SET Upvote = Upvote + 1 WHERE Reply_Id = @0", Reply_Id);
                            db.Close();
                        }
                        break;

                    case "":
                        if (type == "Post")
                        {
                            db.Execute("INSERT INTO IsVoted(Forum_Forum_Id, Post_Post_Id, Username_Username, Voted) VALUES(@0, @1, @2, @3)", Forum_Id, Post_Id, voter, "Upvoted");
                            db.Execute("UPDATE Post SET Upvote = Upvote + 1 WHERE Post_Id = @0", Post_Id);
                            db.Close();
                        }
                        else if (type == "Comment")
                        {
                            db.Execute("INSERT INTO IsVoted(Forum_Forum_Id, Post_Post_Id, Comment_Comment_Id, Username_Username, Voted) VALUES(@0, @1, @2, @3, @4)", Forum_Id, Post_Id, Comment_Id, voter, "Upvoted");
                            db.Execute("UPDATE Comments SET Upvote = Upvote + 1 WHERE Comment_Id = @0", Comment_Id);
                            db.Close();
                        }
                        else if (type == "Reply")
                        {
                            db.Execute("INSERT INTO IsVoted(Forum_Forum_Id, Post_Post_Id, Comment_Comment_Id, Reply_Reply_Id, Username_Username, Voted) VALUES(@0, @1, @2, @3, @4, @5)", Forum_Id, Post_Id, Comment_Id, Reply_Id, voter, "Upvoted");
                            db.Execute("UPDATE Replies SET Upvote = Upvote + 1 WHERE Reply_Id = @0", Reply_Id);
                            db.Close();
                        }
                        break;
                }
                break;

            case "Downvote":
                switch (is_voted_as)
                {
                    case "Upvoted":
                        if (type == "Post")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Post_Post_Id = @1 AND Comment_Comment_Id IS NULL AND Username_Username = @2", "Downvoted", Post_Id, voter);
                            db.Execute("UPDATE Post SET Upvote = Upvote - 1, Downvote = Downvote + 1 WHERE Post_Id = @0", Post_Id);
                            db.Close();
                        }
                        else if (type == "Comment")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Comment_Comment_Id = @1 AND Reply_Reply_Id IS NULL AND Username_Username = @2", "Downvoted", Comment_Id, voter);
                            db.Execute("UPDATE Comments SET Upvote = Upvote - 1, Downvote = Downvote + 1 WHERE Comment_Id = @0", Comment_Id);
                            db.Close();
                        }
                        else if (type == "Reply")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Reply_Reply_Id = @1 AND Username_Username = @2", "Downvoted", Reply_Id, voter);
                            db.Execute("UPDATE Replies SET Upvote = Upvote - 1, Downvote = Downvote + 1 WHERE Reply_Id = @0", Reply_Id);
                            db.Close();
                        }
                        break;

                    case "Downvoted":
                        if (type == "Post")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Post_Post_Id = @1 AND Comment_Comment_Id IS NULL AND Username_Username = @2", "Retracted", Post_Id, voter);
                            db.Execute("UPDATE Post SET Downvote = Downvote - 1 WHERE Post_Id = @0", Post_Id);
                            db.Close();
                        }
                        else if (type == "Comment")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Comment_Comment_Id = @1 AND Reply_Reply_Id IS NULL AND Username_Username = @2", "Retracted", Comment_Id, voter);
                            db.Execute("UPDATE Comments SET Downvote = Downvote - 1 WHERE Comment_Id = @0", Comment_Id);
                            db.Close();
                        }
                        else if (type == "Reply")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Reply_Reply_Id = @1 AND Username_Username = @2", "Retracted", Reply_Id, voter);
                            db.Execute("UPDATE Replies SET Downvote = Downvote - 1 WHERE Reply_Id = @0", Reply_Id);
                            db.Close();
                        }
                        break;

                    case "Retracted":
                        if (type == "Post")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Post_Post_Id = @1 AND Comment_Comment_Id IS NULL AND Username_Username = @2", "Downvoted", Post_Id, voter);
                            db.Execute("UPDATE Post SET Downvote = Downvote + 1 WHERE Post_Id = @0", Post_Id);
                            db.Close();
                        }
                        else if (type == "Comment")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Comment_Comment_Id = @1 AND Reply_Reply_Id IS NULL AND Username_Username = @2", "Downvoted", Comment_Id, voter);
                            db.Execute("UPDATE Comments SET Downvote = Downvote + 1 WHERE Comment_Id = @0", Comment_Id);
                            db.Close();
                        }
                        else if (type == "Reply")
                        {
                            db.Execute("UPDATE IsVoted SET Voted = @0 WHERE Reply_Reply_Id = @1 AND Username_Username = @2", "Downvoted", Reply_Id, voter);
                            db.Execute("UPDATE Replies SET Downvote = Downvote + 1 WHERE Reply_Id = @0", Reply_Id);
                            db.Close();
                        }
                        break;

                    case "":
                        if (type == "Post")
                        {
                            db.Execute("INSERT INTO IsVoted(Forum_Forum_Id, Post_Post_Id, Username_Username, Voted) VALUES(@0, @1, @2, @3)", Forum_Id, Post_Id, voter, "Downvoted");
                            db.Execute("UPDATE Post SET Downvote = Downvote + 1 WHERE Post_Id = @0", Post_Id);
                            db.Close();
                        }
                        else if (type == "Comment")
                        {
                            db.Execute("INSERT INTO IsVoted(Forum_Forum_Id, Post_Post_Id, Comment_Comment_Id, Username_Username, Voted) VALUES(@0, @1, @2, @3, @4)", Forum_Id, Post_Id, Comment_Id, voter, "Downvoted");
                            db.Execute("UPDATE Comments SET Downvote = Downvote + 1 WHERE Comment_Id = @0", Comment_Id);
                            db.Close();
                        }
                        else if (type == "Reply")
                        {
                            db.Execute("INSERT INTO IsVoted(Forum_Forum_Id, Post_Post_Id, Comment_Comment_Id, Reply_Reply_Id, Username_Username, Voted) VALUES(@0, @1, @2, @3, @4, @5)", Forum_Id, Post_Id, Comment_Id, Reply_Id, voter, "Downvoted");
                            db.Execute("UPDATE Replies SET Downvote = Downvote + 1 WHERE Reply_Id = @0", Reply_Id);
                            db.Close();
                        }
                        break;
                }
                break;
        }
    }
    /// <summary>
    /// Checks the visitors rank and returns it as a string
    /// </summary>
    /// <param name="mail">the mail of the visitor that is given by the session </param>
    /// <returns>the visitors rank as a string</returns>
    public string Rank (string mail)
    {
        var getUser = "SELECT Rank FROM [Profiel] WHERE Email = @0";
        string rank = db.QuerySingle(getUser, mail).rank;
        if (rank == "Student")
        {
            return "Student";
        }
        else if (rank == "Teacher")
        {
            return "Teacher";
        }
        else if (rank == "Owner")
        {
            return "Owner";
        }
        else
        {
            return "NoRank";
        }
    }

    /// <summary>
    /// Gets the postions of the houses
    /// </summary>
    /// <param name="Gr">Total points of Gryffindor</param>
    /// <param name="Hu">Total points of HufflePuff</param>
    /// <param name="Ra">Total points of Ravenclaw</param>
    /// <param name="Sl">Total points of SLytherin</param>
    /// <returns>Returns an integer array containing all positions</returns>
    public int[] GetPositions(int Gr, int Hu, int Ra, int Sl)
    {
        int[] position_Array = new int[4];

        int Gr_Pos = 0;
        int Hu_Pos = 0;
        int Ra_Pos = 0;
        int Sl_Pos = 0;

        int[] points_Array = new int[4];

        points_Array[0] = Gr;
        points_Array[1] = Hu;
        points_Array[2] = Ra;
        points_Array[3] = Sl;

        for (int x = 0; x < points_Array.Length; x++)
        {
            int position_int = 1;
            int position_from_first = 0;
            for (int i = 0; i <= 3; i++)
            {
                if (points_Array[x] < points_Array[i] && x != i)
                {
                    position_from_first++;
                }
            }
            position_int += position_from_first;
            switch (x)
            {
                case 0:
                    Gr_Pos = position_int;
                    break;
                case 1:
                    Hu_Pos = position_int;
                    break;
                case 2:
                    Ra_Pos = position_int;
                    break;
                case 3:
                    Sl_Pos = position_int;
                    break;
            }
        }
        position_Array[0] = Gr_Pos;
        position_Array[1] = Hu_Pos;
        position_Array[2] = Ra_Pos;
        position_Array[3] = Sl_Pos;
        return position_Array;
    }
}
