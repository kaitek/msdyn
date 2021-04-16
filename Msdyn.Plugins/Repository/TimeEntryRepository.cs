using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Msdyn.Plugins.Helper;
using Msdyn.Plugins.Models;
using System;
using System.Linq;

namespace Msdyn.Plugins.Integration.Repository
{
    public sealed class TimeEntryRepository : RepositoryBase<TimeEntry>
    {
        public TimeEntryRepository(IOrganizationService organizationService) : base(organizationService)
        {
        }

        public TimeEntryRepository(IOrganizationService organizationService, ITracingService tracingService) :
            base(organizationService, tracingService)
        {
        }

        public bool ValidateDate(DateTime date)
        {
            var query = new QueryExpression(TimeEntry.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(
                 TimeEntry.FieldNames.Id
             ),
                //Distinct = true,
                TopCount = 1,
                //NoLock = true
            };

            //(NB) SHOULD WE KEEP IN MIND UTC date
            //date format
            QueryExpressionOperations.AddEqualityEqualCondition(query, TimeEntry.FieldNames.Msdyn_date, $"{date:yyyy-MM-dd}");

            TimeEntry item = OrganizationService.RetrieveMultiple(query).Entities.Select(e => e.ToEntity<TimeEntry>()).FirstOrDefault();
            return item != null;
        }


    }

   
}
