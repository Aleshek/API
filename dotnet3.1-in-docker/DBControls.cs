using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;


// System.Data.SQLite.SQLiteException      SQL EXCEPTION
// exil_code:   0=succes | -1=failure

namespace dotnet3._1_in_docker
{
    public class DBControls
    {       
        private const string connectionString = "Data Source=LeadsDB.db;Version=3;";

         public static List<Entry> loadLeads(out int exit_code)
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {
                    var output = db.Query<Entry>("select * from Leads", new DynamicParameters());
                    exit_code = 0;
                    return output.ToList();
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    Console.WriteLine("leadLeads() ERROR = " + e);
                    exit_code = -1;
                    return null;
                }               
            }
        }


        public static void saveLeads(Entry lead, out int exit_code)
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {
                    exit_code = 0;
                    db.Execute("insert into Leads (first_name, last_name, mobile, email, location_type, location_string, status) values (@first_name, @last_name, @mobile, @email, @location_type, @location_string, @status)", lead);
                } catch (System.Data.SQLite.SQLiteException e)
                {
                    exit_code = -1;
                    Console.WriteLine("saveLeads() ERROR = " + e);
                }              
            }
        }

        public static Entry fetchLeadByID(int id, out int exit_code)
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {
                    var output = db.Query<Entry>("select * from Leads where ID=" + id, new DynamicParameters()).ToList();
                    try
                    {
                        exit_code = 0;
                        return output[0];//
                    } catch (System.ArgumentOutOfRangeException e)
                    {
                        exit_code = -1;
                        Console.WriteLine("fetchByID() ERROR = " + e);
                        return null;
                    }                    
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    Console.WriteLine("fetchByID() ERROR = " + e);
                    exit_code= - 1;
                    return null;
                }                
            }
        }

        public static void editLeadByID(int id, Entry lead, out int exit_code)
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {        
                    int check = db.Execute("update Leads set first_name=@first_name, last_name=@last_name, mobile=@mobile, email=@email, location_type=@location_type, location_string=@location_string, status=@status where id=" + id, lead);
                    if (check > 0)
                    {
                        exit_code = 0;
                    } else
                    {
                        exit_code = -1;
                        Console.WriteLine("* editLeadByID() ERROR = lead with this id does not exist");
                    }                         
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    exit_code = -1;
                    Console.WriteLine("editLeadByID() ERROR = " + e);
                }
            }
        }

        public static void removeLeadByID(int id, out int exit_code)
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {                    
                    int check = db.Execute("delete from Leads where id=" + id);
                    if (check > 0)
                    {
                        exit_code = 0;
                    } else
                    {
                        exit_code = -1;
                        Console.WriteLine("* removeLeadByID() ERROR = lead with this id does not exist");
                    }
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    exit_code = -1;
                    Console.WriteLine("removeLeadByID() ERROR = " + e);
                }                
            }
        }

        public static void markLeadByID(int id, string comment, out int exit_code)
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {

                    int check = db.Execute("update Leads set status=2, communication='" + comment + "' where id=" + id);
                    if (check > 0)
                    {
                        exit_code = 0;
                    } else
                    {
                        exit_code = -1;
                        Console.WriteLine("* markLeadByID() ERROR = lead with this id does not exist");
                    }        
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    exit_code = -1;
                    Console.WriteLine("markLeadByID() ERROR = " + e);
                }                
            }
        }


    }
}
