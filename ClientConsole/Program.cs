using Microsoft.Extensions.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace PowerPlatform.Dataverse.CodeSamples
{
    internal class Program
    {
        /// <summary>
        /// Contains the application's configuration settings. 
        /// </summary>
        private IConfiguration Configuration { get; }

        private static readonly string path = "appsettings.json";

        static void Main(string[] args)
        {
            Program app = new();

            // Grab the settings from the appsettings.json file
            var connectionString = app.Configuration.GetConnectionString("default");

            ServiceClient serviceClient = app.CreateServiceClient(connectionString);

            //Do Stuff

        }

        Program()
        {
            // Load the app's configuration settings from the JSON file.
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(path, optional: false, reloadOnChange: true)
                .Build();
        }

        private ServiceClient CreateServiceClient(string? connectionString)
        {
            if (connectionString is null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            // Create a Dataverse service client using the default connection string.
            Console.Write("Connecting to Dataverse environment...");
            ServiceClient serviceClient = new(connectionString);

            if (!serviceClient.IsReady)
            {
                throw serviceClient.LastException;
            }
            Console.WriteLine("done.");

            return serviceClient;
        }
    }
}