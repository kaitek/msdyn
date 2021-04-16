using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace Msdyn.Plugins.Models
{
    [EntityLogicalName(EntityLogicalName)]
    public class TimeEntry : Entity
    {
        public TimeEntry() : base(EntityLogicalName) { }
        public const string EntityLogicalName = "msdyn_timeentry";

        [AttributeLogicalName(FieldNames.Id)]
        public new Guid Id
        {
            get => GetAttributeValue<Guid>(FieldNames.Id);
            set => SetAttributeValue(FieldNames.Id, (base.Id = value));
        }

        [AttributeLogicalName(FieldNames.Msdyn_date)]
        public DateTime? Msdyn_date
        {
            get => GetAttributeValue<DateTime?>(FieldNames.Msdyn_date);
            set => SetAttributeValue(FieldNames.Msdyn_date, value);
        }


        [AttributeLogicalName(FieldNames.Msdyn_start)]
        public DateTime? Msdyn_start
        {
            get => GetAttributeValue<DateTime?>(FieldNames.Msdyn_start);
            set => SetAttributeValue(FieldNames.Msdyn_start, value);
        }


        [AttributeLogicalName(FieldNames.Msdyn_end)]
        public DateTime? Msdyn_end
        {
            get => GetAttributeValue<DateTime?>(FieldNames.Msdyn_end);
            set => SetAttributeValue(FieldNames.Msdyn_end, value);
        }

        public struct FieldNames
        {
            public const string Id = "msdyn_timeentryid";
            public const string Msdyn_start = "msdyn_start";
            public const string Msdyn_end = "msdyn_end";
            public const string Msdyn_date = "msdyn_date";
        }
    }
}
