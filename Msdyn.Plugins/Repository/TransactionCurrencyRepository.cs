

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;
using Msdyn.Plugins.Models;


namespace Msdyn.Plugins.Integration.Repository
{
    public sealed class TransactionCurrencyRepository : RepositoryBase<TransactionCurrency>
    {
        public TransactionCurrencyRepository(IOrganizationService organizationService) : base(organizationService)
        {
        }

        public TransactionCurrencyRepository(IOrganizationService organizationService, ITracingService tracingService) :
            base(organizationService, tracingService)
        {
        }

       

    }
}
