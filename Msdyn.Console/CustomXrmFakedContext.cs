using FakeXrmEasy;
using Microsoft.Xrm.Sdk;

namespace Msdyn.Console
{
    public class CustomXrmFakedContext : XrmFakedContext
    {
        private readonly IOrganizationService _service;

        public CustomXrmFakedContext(IOrganizationService service)
        {
            _service = service;
        }

        public override IOrganizationService GetOrganizationService()
        {
            return _service;
        }
    }
}
