using Microsoft.Xrm.Sdk;
using System;

namespace Msdyn.Plugins.Core
{
    public class LocalPluginContext
    {
        public const string TargetName = "Target";
        public const string PreImageName = "PreImage";
        public const string PostImageName = "PostImage";

        private readonly Lazy<IOrganizationService> _organizationServiceAsSystem;
        private EntityReference _targetReference;
        private Entity _mergedTarget;
        private Entity _target;

        public IOrganizationService OrganizationServiceAsSystem => _organizationServiceAsSystem.Value;

        internal EntityReference TargetReference => _targetReference ??
                                                    (_targetReference =
                                                        PluginExecutionContext.InputParameters.ContainsKey(TargetName)
                                                            ? (EntityReference)PluginExecutionContext.InputParameters[TargetName]
                                                            : null);
        internal Guid UserId => PluginExecutionContext.UserId;
        public Guid InitiatingUserId => PluginExecutionContext.InitiatingUserId;
        public string Message => PluginExecutionContext.MessageName;
        public Entity PreImage =>
            PluginExecutionContext.PreEntityImages.ContainsKey(PreImageName)
                ? PluginExecutionContext.PreEntityImages[PreImageName] : null;

        public Entity PostImage =>
            PluginExecutionContext.PostEntityImages.ContainsKey(PostImageName)
                ? PluginExecutionContext.PostEntityImages[PostImageName] : null;

        public T GetTarget<T>() where T : Entity => Target?.ToEntity<T>();
        public T GetPreImage<T>() where T : Entity => PreImage?.ToEntity<T>();
        public T GetPostImage<T>() where T : Entity => PostImage?.ToEntity<T>();

        public IServiceProvider ServiceProvider
        {
            get;
        }

        public IOrganizationService OrganizationService
        {
            get;
        }

        public IPluginExecutionContext PluginExecutionContext
        {
            get;
        }

        public ITracingService TracingService
        {
            get;
        }
        /// <summary>
        /// Returns the current plugin execution Target
        /// </summary>
        public Entity Target
        {
            get
            {
                if (_target != null || !PluginExecutionContext.InputParameters.ContainsKey(TargetName)) return _target;
                var target = PluginExecutionContext.InputParameters[TargetName];
                switch (target)
                {
                    case Entity entity:
                        _target = entity;
                        break;
                    case EntityReference reference:
                        _target = new Entity(reference.LogicalName, reference.Id);
                        break;
                }
                return _target;
            }
        }


        /// <summary>
        /// Returns a merged version of the target so that it contains the preimage and any changed values
        /// </summary>
        internal Entity MergedTarget
        {
            get
            {
                if (_mergedTarget == null)
                {

                    // Get preimage
                    Entity preImage = null;
                    if (PluginExecutionContext.PreEntityImages.ContainsKey(PreImageName))
                    {
                        preImage = PluginExecutionContext.PreEntityImages[PreImageName];
                    }

                    _mergedTarget = PluginUtils.MergeTargetAndImage<Entity>(Target, preImage);
                }
                return _mergedTarget;
            }
        }
        public LocalPluginContext()
        {
        }

        public LocalPluginContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException("serviceProvider");

            // Obtain the execution context service from the service provider.
            PluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Obtain the tracing service from the service provider.
            TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the Organization Service factory service from the service provider
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            // Use the factory to generate the Organization Service.
            OrganizationService = factory.CreateOrganizationService(PluginExecutionContext.UserId);
            _organizationServiceAsSystem = new Lazy<IOrganizationService>(() => factory.CreateOrganizationService(null));
            ServiceProvider = ServiceProvider;
        }

        public void Trace(string message, bool doTrace = true)
        {
            if (!doTrace)
                return;

            if (string.IsNullOrWhiteSpace(message) || TracingService == null)
            {
                return;
            }

            if (PluginExecutionContext == null && TracingService != null)
            {
                TracingService.Trace(message);
            }
            else
            {
                TracingService.Trace("{0} : (Correlation Id: {1}, Initiating User: {2})", message, PluginExecutionContext.CorrelationId, PluginExecutionContext.InitiatingUserId);
            }
        }

        public T GetInputParameter<T>(string name) => (T)PluginExecutionContext.InputParameters[name];

        public void SetOutputParameter(string name, object value) => PluginExecutionContext.OutputParameters[name] = value;
    }
}
