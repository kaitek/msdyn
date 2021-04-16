using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace Msdyn.Plugins.Models
{
    [EntityLogicalName(EntityLogicalName)]
    public class SystemUser : Entity
    {
        public SystemUser() : base(EntityLogicalName) { }
        public const string EntityLogicalName = "systemuser";
        [AttributeLogicalName(FieldNames.Id)]
        public new Guid Id
        {
            get => GetAttributeValue<Guid>(FieldNames.Id);
            set => SetAttributeValue(FieldNames.Id, (base.Id = value));
        }
        [AttributeLogicalName(FieldNames.DomainName)]
        public string DomainName
        {
            get => GetAttributeValue<string>(FieldNames.DomainName);
            set => SetAttributeValue(FieldNames.DomainName, value);
        }
        [AttributeLogicalName(FieldNames.FullName)]
        public string FullName
        {
            get => GetAttributeValue<string>(FieldNames.FullName);
            set => SetAttributeValue(FieldNames.FullName, value);
        }
        [AttributeLogicalName(FieldNames.BusinessUnitId)]
        public EntityReference BusinessUnitId
        {
            get => GetAttributeValue<EntityReference>(FieldNames.BusinessUnitId);
            set => SetAttributeValue(FieldNames.BusinessUnitId, value);
        }
        /// <summary>
        /// Имя
        /// </summary>
        [AttributeLogicalName(FieldNames.FirstName)]
        public string FirstName
        {
            get => GetAttributeValue<string>(FieldNames.FirstName);
            set => SetAttributeValue(FieldNames.FirstName, value);
        }
        /// <summary>
        /// Фамилия
        /// </summary>
        [AttributeLogicalName(FieldNames.LastName)]
        public string LastName
        {
            get => GetAttributeValue<string>(FieldNames.LastName);
            set => SetAttributeValue(FieldNames.LastName, value);
        }
        /// <summary>
        /// Отчество
        /// </summary>
        [AttributeLogicalName(FieldNames.MiddleName)]
        public string MiddleName
        {
            get => GetAttributeValue<string>(FieldNames.MiddleName);
            set => SetAttributeValue(FieldNames.MiddleName, value);
        }
        [AttributeLogicalName(FieldNames.InternalEmailAddress)]
        public string InternalEmailAddress
        {
            get => GetAttributeValue<string>(FieldNames.InternalEmailAddress);
            set => SetAttributeValue(FieldNames.InternalEmailAddress, value);
        }
        [AttributeLogicalName(FieldNames.Address1_Telephone1)]
        public string Address1_Telephone1
        {
            get => GetAttributeValue<string>(FieldNames.Address1_Telephone1);
            set => SetAttributeValue(FieldNames.Address1_Telephone1, value);
        }
        [AttributeLogicalName(FieldNames.JobTitle)]
        public string JobTitle
        {
            get => GetAttributeValue<string>(FieldNames.JobTitle);
            set => SetAttributeValue(FieldNames.JobTitle, value);
        }

        [AttributeLogicalName(FieldNames.IsDisabled)]
        public bool? IsDisabled
        {
            get => GetAttributeValue<bool?>(FieldNames.IsDisabled);
            set => SetAttributeValue(FieldNames.IsDisabled, value);
        }
        
       

        [AttributeLogicalName(FieldNames.AccessMode)]
        public AccessModeEnum? AccessMode
        {
            get
            {
                var option = GetAttributeValue<OptionSetValue>(FieldNames.AccessMode);
                return option == null ? null : (AccessModeEnum?)Enum.Parse(typeof(AccessModeEnum), option.Value.ToString());
            }
            set => SetAttributeValue(FieldNames.AccessMode, value.HasValue ? new OptionSetValue((int)value) : null);
        }
        [AttributeLogicalName(FieldNames.CalType)]
        public CalTypeEnum? CalType
        {
            get
            {
                var option = GetAttributeValue<OptionSetValue>(FieldNames.CalType);
                return option == null ? null : (CalTypeEnum?)Enum.Parse(typeof(CalTypeEnum), option.Value.ToString());
            }
            set => SetAttributeValue(FieldNames.CalType, value.HasValue ? new OptionSetValue((int)value) : null);
        }

        [AttributeLogicalName(FieldNames.MobilePhone)]
        public string MobilePhone
        {
            get => GetAttributeValue<string>(FieldNames.MobilePhone);
            set => SetAttributeValue(FieldNames.MobilePhone, value);
        }

        [AttributeLogicalName(FieldNames.ParentSystemUserId)]
        public EntityReference ParentSystemUserId
        {
            get => GetAttributeValue<EntityReference>(FieldNames.ParentSystemUserId);
            set => SetAttributeValue(FieldNames.ParentSystemUserId, value);
        }

        

        [AttributeLogicalName(FieldNames.Title)]
        public string Title
        {
            get => GetAttributeValue<string>(FieldNames.Title);
            set => SetAttributeValue(FieldNames.Title, value);
        }

       

        //private IEnumerable<Role> roles;
        //[RelationshipSchemaName(RelationshipsNames.SystemUserRolesAssociation)]
        //public IEnumerable<Role> Roles
        //{
        //    get => roles = roles ?? GetRelatedEntities<Role>(RelationshipsNames.SystemUserRolesAssociation, null);
        //    set => roles = value;
        //}

        //private IEnumerable<Team> teams;
        //[RelationshipSchemaName(RelationshipsNames.SystemUserTeamsAssociation)]
        //public IEnumerable<Team> Teams
        //{
        //    get => teams = teams ?? GetRelatedEntities<Team>(RelationshipsNames.SystemUserTeamsAssociation, null);
        //    set => teams = value;
        //}

        public struct FieldNames
        {
            public const string Id = "systemuserid";
            public const string DomainName = "domainname";
            public const string FullName = "fullname";
            public const string BusinessUnitId = "businessunitid";
            public const string FirstName = "firstname";
            public const string LastName = "lastname";
            public const string MiddleName = "middlename";
            public const string InternalEmailAddress = "internalemailaddress";
            public const string Address1_Telephone1 = "address1_telephone1";
            public const string JobTitle = "jobtitle";
            public const string IsDisabled = "isdisabled";

            public const string AccessMode = "accessmode";
            public const string CalType = "caltype";
            public const string MobilePhone = "mobilephone";

            public const string ParentSystemUserId = "parentsystemuserid";

            public const string Title = "title";

        }
        public struct RelationshipsNames
        {
            public const string SystemUserTeamsAssociation = "teammembership_association";
            public const string SystemUserRolesAssociation = "systemuserroles_association";
        }
        public enum AccessModeEnum : int
        {
            Administrative = 1
        }
        public enum CalTypeEnum : int
        {
            Administrative = 1
        }
    }
}
