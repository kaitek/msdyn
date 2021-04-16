using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Msdyn.Plugins.Integration.Repository
{
    public abstract class RepositoryBase<T> where T : Entity, new()
    {
        protected IOrganizationService OrganizationService { get; private set; }

        protected ITracingService TracingService { get; private set; }

        public RepositoryBase(IOrganizationService organizationService)
        {
            OrganizationService = organizationService;
        }

        public RepositoryBase(IOrganizationService organizationService, ITracingService tracingService)
        {
            OrganizationService = organizationService;
            TracingService = tracingService;
        }

        public Guid Create(T entity)
        {
            return OrganizationService.Create(entity);
        }

        public void Update(T entity)
        {
            OrganizationService.Update(entity);
        }

        /// <summary>
        /// Bulk entities update
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateMultiple(IEnumerable<T> entities, bool continueOnError = false)
        {
            if (entities != null && entities.Count() > 0)
            {
                if (entities.Count() == 1 && !continueOnError)
                {
                    Update(entities.First());
                }
                else
                {
                    var request = new ExecuteMultipleRequest()
                    {
                        Settings = new ExecuteMultipleSettings()
                        {
                            ContinueOnError = continueOnError,
                            ReturnResponses = true
                        },
                        Requests = new OrganizationRequestCollection()
                    };
                    request.Requests.AddRange(entities.Select(n => new UpdateRequest() { Target = n }));
                    try
                    {
                        var response = (ExecuteMultipleResponse)OrganizationService.Execute(request);
                    }
                    catch (FaultException<OrganizationServiceFault> ex)
                    {
                        string errorString = $"UpdateMultiple request failed for the entity {ex.Detail} and the reason being: {ex.Detail.Message}";
                        throw new Exception(errorString);
                    }
                }
            }
        }

        public void Delete(T entity)
        {
            Delete(entity.ToEntityReference());
        }
        public void Delete(EntityReference entityReference)
        {
            OrganizationService.Delete(entityReference.LogicalName, entityReference.Id);
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="entityId">primary id</param>
        /// <returns></returns>
        public T GetById(Guid entityId)
        {
            return OrganizationService.Retrieve(new T().LogicalName, entityId, new ColumnSet(true)).ToEntity<T>();
        }

        /// <summary>
        /// Get List entity by List ids
        /// </summary>
        /// <param name="listIds">List of id</param>
        /// <returns></returns>
        public List<T> GetByListId(List<Guid> listIds)
        {
            List<T> listResult = new List<T>();
            foreach (var id in listIds)
            {
                T entity = OrganizationService.Retrieve(new T().LogicalName, id, new ColumnSet(true)).ToEntity<T>();
                listResult.Add(entity);
            }
            return listResult;
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="entityId">primary id</param>
        /// <param name="allColumns"></param>
        /// <returns></returns>
        public T GetById(Guid entityId, bool allColumns)
        {
            return OrganizationService.Retrieve(new T().LogicalName, entityId, new ColumnSet(allColumns)).ToEntity<T>();
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="entityId">primary id</param>
        /// <returns></returns>
        public T GetById(Guid entityId, params string[] columns)
        {
            return OrganizationService.Retrieve(new T().LogicalName, entityId, new ColumnSet(columns)).ToEntity<T>();
        }
    }

}
