using System;
using System.IO;
using RestSharp;

namespace Notify_Station_Caller
{
    class Program
    {
        private static string WorkingDir = @"C:\ProgramData\Notify_Station\";
        static void Main(string[] args)
        {
            string id = "";
            string api = File.ReadAllText(WorkingDir + "api");
            try
            {
                id = args[0];
            }catch
            {
                Console.WriteLine("Error: Invalid syntax.  Please provide an ID to call.");
            }
            if (id == "")
            {
                Console.WriteLine("Error: Invalid syntax.  Please provide an ID to call.");
            }else
            {
                RestClient client;
                client = new RestClient(api);
                var request = new RestRequest(@"v1/call.php", Method.GET);
                request.AddParameter("id", id);
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                if (content.ToString() == "Success=True")
                {
                    Console.WriteLine("Call successfully sent.");
                }else
                {
                    Console.WriteLine("Unknown error: ");
                    Console.WriteLine(response.Content.ToString());
                }
            }
        }

    }
}
