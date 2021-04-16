using Microsoft.Xrm.Sdk;
using Msdyn.Plugins.Core;
using Msdyn.Plugins.Helper;
using Msdyn.Plugins.Integration.Repository;
using Msdyn.Plugins.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Msdyn.Plugins.Integration.Plugins
{
    //(NB)
    //[CrmPluginRegistration(
    //    MessageNameEnum.Create
    //  , TimeEntry.EntityLogicalName
    //  , StageEnum.PostOperation
    //  , ExecutionModeEnum.Synchronous
    //  , ""
    //  , "msdyn_timeentry_PostCreateAsyncPlugin"
    //  , 1
    //  , IsolationModeEnum.None
    //  )]
    public sealed class msdyn_timeentry_PostCreateAsyncPlugin : PluginBase
    {
        public override void ExecuteAction(LocalPluginContext context)
        {
            TimeEntry model =
              context.Target.ToEntity<TimeEntry>();

            if(model.Msdyn_start.HasValue && model.Msdyn_end.HasValue)
            {
                context.TracingService.Trace($"start {context.Message} in msdyn_timeentry_postcreateasyncplugin");

                IOrganizationServiceFactory factory = (IOrganizationServiceFactory)context.ServiceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService organizationService = factory.CreateOrganizationService(null);

                TimeEntryRepository timeEntryRepository = new TimeEntryRepository(organizationService);
                //(NB) we suppose dateStart is before dateEnd
                DateTime dateStart = model.Msdyn_start.Value;
                DateTime dateEnd = model.Msdyn_end.Value;

                List<DateTime> dateList = new List<DateTime>
                {
                };

                while(dateStart < dateEnd)
                {
                    // we saving pure date - without hours & minutes etc 
                    dateList.Add(new DateTime(dateStart.Year, dateStart.Month, dateStart.Day));
                    dateStart.AddDays(1);
                }

                List<Entity> batch = new List<Entity>();

                dateList.ForEach(item =>
                {
                    //push to create after date validation
                    if (!timeEntryRepository.ValidateDate(item))
                    {
                        Entity entity = new Entity(TimeEntry.EntityLogicalName);
                        entity[TimeEntry.FieldNames.Msdyn_date] = item;
                        batch.Add(entity);
                    }
                });

                //On creation of a Time Entry record the plugin should evaluate
                //if the start and end date contain different values from each other.
                //In the event that the start and end date are different
                //then a time entry record should be created for every date in the date range from start to end date.
                //The plugin should also ensure that there are no duplicate time entry records created per date.

                if (batch.Any())
                {
                    context.TracingService.Trace($"before create records");
                    BatchOperation.CreateRecords(batch, context.OrganizationService, context.TracingService);
                    context.TracingService.Trace($"after create records");    
                }

                context.TracingService.Trace($"exit {context.Message} in msdyn_timeentry_postcreateasyncplugin");

            }
        }
    }
}
