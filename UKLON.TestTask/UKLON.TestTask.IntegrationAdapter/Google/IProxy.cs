using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKLON.TestTask.IntegrationAdapter.Google
{
    public interface IProxy
    {
        List<Data> GetSpreadsheetData<Data>(string spreadsheetId, string range)
            where Data : new();
    }
}
