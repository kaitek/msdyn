using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.Net;


namespace Msdyn.Console
{
    class Program
    {
        private static IOrganizationService GetService()
        {
            var crmLink = "https://***.crm4.dynamics.com";
            var login = "****@****.com";
            var password = "S3#*****";

            var connectionString =
                $"AuthType=Office365;" +
                $"Url={crmLink};" +
                $"UserName={login};" +
                $"Password={password};" +
                $"RequireNewInstance=True";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            CrmServiceClient conn = new CrmServiceClient(connectionString);


            return conn.OrganizationWebProxyClient != null ? conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Start program!");


            var service = GetService();
            if (service != null)
            {
                System.Console.WriteLine("Connect successfull!");
            }
            else
            {
                System.Console.WriteLine("Connect failed!");
                return;
            }
            msdyn_timeentry_PostCreateAsyncPluginTest.TestOnCreate(service);

            System.Console.WriteLine("Done!");
        }

     
    }
}
