using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Msdyn.Plugins.Helper
{
    static class BatchOperation
    {
        public static void CreateRecords(
            List<Entity> list
          , IOrganizationService service
          , ITracingService tracingService
          , int packCount = 99
          )
        {
            int created = 0;
            try
            {
                bool executeRequest = false;
                if (list != null && list.Any())
                {
                    ExecuteMultipleRequest multipleRequest = new ExecuteMultipleRequest()
                    {
                        Settings = new ExecuteMultipleSettings()
                        {
                            ContinueOnError = true,
                            ReturnResponses = false
                        },
                        Requests = new OrganizationRequestCollection()
                    };

                    foreach (Entity item in list)
                    {
                        executeRequest = true;

                        multipleRequest.Requests.Add(new CreateRequest { Target = item });
                        if (multipleRequest.Requests.Count > packCount)
                        {
                            ExecuteMultipleResponse multipleResponse = (ExecuteMultipleResponse)service.Execute(multipleRequest);
                            executeRequest = false;
                            created += multipleRequest.Requests.Count;
                            multipleRequest.Requests.Clear();
                        }
                    }
                    if (executeRequest)
                    {
                        ExecuteMultipleResponse multipleResponse = (ExecuteMultipleResponse)service.Execute(multipleRequest);
                        created += multipleRequest.Requests.Count;
                        multipleRequest.Requests.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                if (tracingService != null)
                    tracingService.Trace(ex.ToString());

               
                if (ex.InnerException != null)
                {
                    if (tracingService != null)
                        tracingService.Trace(ex.InnerException.ToString());
                }
            }
            finally
            {
                if (tracingService != null)
                    tracingService.Trace($"{created} items were created");

            }
        }
    }
}
