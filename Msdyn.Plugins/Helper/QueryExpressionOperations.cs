using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Msdyn.Plugins.Helper
{
    public static class QueryExpressionOperations
    {

        public static void AddEqualityEqualCondition(QueryExpression query, string attributeName, object value)
        {
            if (value != null)
                query.Criteria.AddCondition(attributeName, ConditionOperator.Equal, $"{value}");
        }

        public static void AddEqualityNotEqualCondition(QueryExpression query, string attributeName, object value)
        {
            if (value != null)
                query.Criteria.AddCondition(attributeName, ConditionOperator.NotEqual, $"{value}");
        }

        public static void AddOrEqualityCondition(QueryExpression query, string[] attributes, object value)
        {
            if (value != null)
            {
                FilterExpression filter = new FilterExpression
                {
                    FilterOperator = LogicalOperator.Or
                };

                foreach (string attribute in attributes)
                {
                    filter.AddCondition(attribute, ConditionOperator.Equal, $"{value}");
                }

                query.Criteria.AddFilter(filter);
            }
        }

        public static void AddOrEqualityCondition(
              QueryExpression query
            , List<KeyValuePair<string, object>> keyValuePairs
            )
        {

            FilterExpression filter = new FilterExpression
            {
                FilterOperator = LogicalOperator.Or
            };

            foreach (KeyValuePair<string, object> keyValuePair in keyValuePairs)
            {
                if (keyValuePair.Value != null)
                {
                    filter.AddCondition(keyValuePair.Key, ConditionOperator.Equal, $"{keyValuePair.Value}");
                }
            }
            query.Criteria.AddFilter(filter);
        }

        public static void AddEqualityLinkCondition(
             QueryExpression query
           , string entityName
           , string linkFromAttribute
           , string linkToAttribute
           , string attributeName
           , Guid value)
        {
            query.AddLink(entityName, linkFromAttribute, linkToAttribute, JoinOperator.Inner)
                   .LinkCriteria.AddCondition(attributeName, ConditionOperator.Equal, $"{value}");

        }

        public static void AddEqualityIsNullCondition(QueryExpression query, string attributeName)
        {
            query.Criteria.AddCondition(attributeName, ConditionOperator.Null);
        }

        public static void AddEqualityIsNotNullCondition(QueryExpression query, string attributeName)
        {
            query.Criteria.AddCondition(attributeName, ConditionOperator.NotNull);
        }

        public static void AddEqualityOnOrAfterCondition(QueryExpression query, string attributeName, DateTime value)
        {
            query.Criteria.AddCondition(attributeName, ConditionOperator.OnOrAfter, $"{value:yyyy-MM-dd}");
        }

        public static void AddEqualityOnOrBeforeCondition(QueryExpression query, string attributeName, DateTime value)
        {
            query.Criteria.AddCondition(attributeName, ConditionOperator.OnOrBefore, $"{value:yyyy-MM-dd}");
        }

        public static void AddEqualityInCondition(QueryExpression query, string attributeName, Guid[] ids)
        {
            if (ids.Any())
            {
                query.Criteria.AddCondition(new ConditionExpression(attributeName, ConditionOperator.In, ids));  
            }
        }
    }
}
