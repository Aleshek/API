using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Data.SQLite;

namespace dotnet3._1_in_docker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int exit_code;

            Entry test = new Entry() { first_name = "TdTest", last_name = "Ayaa", mobile = "1591591599", email = "aaaccc@fyff.com", location_string = "Pelhrimov", location_type = Entry.LocationType.Country, status = Entry.Status.Created };

            //DBControls.saveLeads(test, out exit_code);
            //DBControls.editLeadByID(55, test, out exit_code);
            //DBControls.removeLeadByID(55, out exit_code);
            DBControls.markLeadByID(54, "AYAYA", out exit_code);
            //Console.WriteLine("Fetched  by id: " + DBControls.fetchLeadByID(54, out exit_code).first_name);

            List<Entry> list = new List<Entry>(DBControls.loadLeads(out exit_code));           
            foreach(Entry lead in list)
            {
                Console.WriteLine(lead.id + " " + lead.first_name + " " + lead.last_name);
            }


            CreateHostBuilder(args).Build().Run();    
        }





        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
