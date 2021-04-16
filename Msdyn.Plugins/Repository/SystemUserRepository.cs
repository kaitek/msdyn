using Microsoft.Xrm.Sdk;
using Msdyn.Plugins.Models;

namespace Msdyn.Plugins.Integration.Repository
{
    public sealed class SystemUserRepository : RepositoryBase<SystemUser>
    {
        public SystemUserRepository(IOrganizationService organizationService) : base(organizationService)
        {
        }

        public SystemUserRepository(IOrganizationService organizationService, ITracingService tracingService) :
            base(organizationService, tracingService)
        {
        }
    }
}
