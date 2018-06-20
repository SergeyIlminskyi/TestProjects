using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;

namespace UKLON.TestTask.IntegrationAdapter.Google
{
    public class Proxy
    {
        public void Temp()
        {
            SpreadsheetsService myService = new SpreadsheetsService("exampleCo-exampleApp-1");
            myService.setUserCredentials("pav@gmail.com", "");

            SpreadsheetQuery query = new SpreadsheetQuery();
            SpreadsheetFeed feed = myService.Query(query);

            Console.WriteLine("Your spreadsheets: ");
            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                Console.WriteLine(entry.Title.Text);
            }
        }
    }
}
