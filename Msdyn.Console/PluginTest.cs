using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Msdyn.Plugins.Integration.Plugins;
using System;

namespace Msdyn.Console
{
    public class msdyn_timeentry_PostCreateAsyncPluginTest
    {
        static public void TestOnCreate(IOrganizationService service)
        {
            var context = new CustomXrmFakedContext(service);
            var pluginContext = new XrmFakedPluginExecutionContext { InputParameters = new ParameterCollection(), OutputParameters = new ParameterCollection() };

            Plugins.Models.TimeEntry entry = new Plugins.Models.TimeEntry
            {
                 Msdyn_start = DateTime.UtcNow.AddDays(-10),
                 Msdyn_end =  DateTime.UtcNow.AddDays(10),
            };
            pluginContext.InputParameters.Add("Target", entry);
            context.ExecutePluginWith(pluginContext, new msdyn_timeentry_PostCreateAsyncPlugin());
        }

        
    }
}
