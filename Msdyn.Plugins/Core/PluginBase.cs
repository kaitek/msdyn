using System;
using System.Globalization;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;

namespace Msdyn.Plugins.Core
{
    public abstract class PluginBase : IPlugin
    {
        private readonly PluginConfiguration _pluginConfiguration = new PluginConfiguration();
        public void Execute(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            // Construct the Local plug-in context.
            LocalPluginContext localContext = new LocalPluginContext(serviceProvider);

            localContext.Trace($"Entered {GetType()};Stage {localContext.PluginExecutionContext.Stage};MessageName {localContext.PluginExecutionContext.MessageName};PrimaryEntityName {localContext.PluginExecutionContext.PrimaryEntityName}", true);

            try
            {
                ExecuteAction(localContext);
            }
            catch (FaultException<OrganizationServiceFault> e)
            {
                localContext.Trace(string.Format(CultureInfo.InvariantCulture, $"PluginBaseException: {e}" ), true);
                // Handle the exception.
                throw;
            }
            catch (Exception ex)
            {
                localContext.Trace(string.Format(CultureInfo.InvariantCulture, $"BaseException: {ex}"), true);
                // Handle the exception.
                throw;
            }
            finally
            {
                localContext.Trace(string.Format(CultureInfo.InvariantCulture, $"Exit {0}", GetType()), _pluginConfiguration.GetAsBool("trace"));
            }
        }

        public abstract void ExecuteAction(LocalPluginContext localPluginContext);
    }
}
