using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;


// System.Data.SQLite.SQLiteException      SQL EXCEPTION
// exil_code:   0=succes | -1=failure

/*

 */

namespace dotnet3._1_in_docker
{
    public class DBControls
    {       
        private const string connectionString = "Data Source=LeadsDB.db;Version=3;"; // přesunout connectionString do appsettings.json

         public static List<Entry> loadLeads(out int exit_code)
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {         
                    var output = db.Query<Entry>("select * from Leads");
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


        public static void saveLeads(Entry lead, out int exit_code)              //REMADE WITH DYNAMICPARAMETERS FOR EDUCATIONAL PURPOSES AND TO HAVE ALL METHODS UNITED
        {                                                                           
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {
                    exit_code = 0;

                    string sql = "insert into Leads " +
                        "(first_name, last_name, mobile, email, location_type, location_string, status)" +
                        " values " +
                        "(@first_name, @last_name, @mobile, @email, @location_type, @location_string, @status)";
                    
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@first_name", lead.first_name, DbType.String, ParameterDirection.Input);
                    parameter.Add("@last_name", lead.last_name, DbType.String, ParameterDirection.Input);
                    parameter.Add("@mobile", lead.mobile, DbType.String, ParameterDirection.Input);
                    parameter.Add("@email", lead.email, DbType.String, ParameterDirection.Input);
                    parameter.Add("@location_type", lead.location_type, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@location_string", lead.location_string, DbType.String, ParameterDirection.Input);
                    parameter.Add("@status", lead.status, DbType.Int32, ParameterDirection.Input);

                    db.Execute(sql, parameter);                }

                catch (System.Data.SQLite.SQLiteException e)
                {
                    exit_code = -1;
                    Console.WriteLine("saveLeads() ERROR = " + e);
                }              
            }
        }
      
        public static Entry fetchLeadByID(int id, out int exit_code) //REMADE WITH DYNAMICPARAMETERS FOR EDUCATIONAL PURPOSES AND TO HAVE ALL METHODS UNITED
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {
                    string sql = "select * from Leads where ID=@ID";

                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@ID", id, DbType.Int32);

                    var output = db.Query<Entry>(sql, parameter).ToList();
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

        public static void editLeadByID(int id, Entry lead, out int exit_code) //REMADE WITH DYNAMICPARAMETERS FOR EDUCATIONAL PURPOSES AND TO HAVE ALL METHODS UNITED
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {
                    string sql = "update Leads set first_name=@first_name, last_name=@last_name, mobile=@mobile, email=@email, location_type=@location_type, location_string=@location_string, status=@status where id=@id";

                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@id", id, DbType.Int32);
                    parameter.Add("@first_name", lead.first_name, DbType.String, ParameterDirection.Input);
                    parameter.Add("@last_name", lead.last_name, DbType.String, ParameterDirection.Input);
                    parameter.Add("@mobile", lead.mobile, DbType.String, ParameterDirection.Input);
                    parameter.Add("@email", lead.email, DbType.String, ParameterDirection.Input);
                    parameter.Add("@location_type", lead.location_type, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@location_string", lead.location_string, DbType.String, ParameterDirection.Input);
                    parameter.Add("@status", lead.status, DbType.Int32, ParameterDirection.Input);

                    int rowsAffected = db.Execute(sql, parameter);

                    if (rowsAffected > 0)
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

        public static void removeLeadByID(int id, out int exit_code) //REMADE WITH DYNAMICPARAMETERS FOR EDUCATIONAL PURPOSES AND TO HAVE ALL METHODS UNITED
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {
                    string sql = "delete from Leads where id=@id";
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@id", id, DbType.Int32);
                    int affectedRows = db.Execute(sql, parameter);
                    if (affectedRows > 0)
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

        public static void markLeadByID(int id, string comment, out int exit_code) //REMADE WITH DYNAMICPARAMETERS FOR EDUCATIONAL PURPOSES AND TO HAVE ALL METHODS UNITED
        {
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                try
                {
                    string sql = "update Leads set status=2, communication=@comment where id=@id";

                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@id", id, DbType.Int32);
                    parameter.Add("@comment", comment, DbType.String);

                    int affectedRows = db.Execute(sql, parameter);
                    if (affectedRows > 0)
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
