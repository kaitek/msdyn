
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace Msdyn.Plugins.Models
{
    [EntityLogicalName(EntityLogicalName)]
    public class TransactionCurrency : Entity
    {
        public TransactionCurrency() : base(EntityLogicalName) { }
        public const string EntityLogicalName = "transactioncurrency";
        [AttributeLogicalName(FieldNames.Id)]
        public new Guid Id
        {
            get => GetAttributeValue<Guid>(FieldNames.Id);
            set => SetAttributeValue(FieldNames.Id, (base.Id = value));
        }

        public Guid TransactionCurrencyId
        {
            get => GetAttributeValue<Guid>(FieldNames.Id);
            set => SetAttributeValue(FieldNames.Id, (base.Id = value));
        }

        //[AttributeLogicalName(FieldNames.CurrencyName)]
        //public string CurrencyName
        //{
        //    get => GetAttributeValue<string>(FieldNames.CurrencyName);
        //    set => SetAttributeValue(FieldNames.CurrencyName, value);
        //}

        [AttributeLogicalName(FieldNames.CurrencyName)]
        public string CurrencyName
        {
            get => GetAttributeValue<string>(FieldNames.CurrencyName);
            set => SetAttributeValue(FieldNames.CurrencyName, value);
        }

        [AttributeLogicalName(FieldNames.IsoCode)]
        public string IsoCode
        {
            get => GetAttributeValue<string>(FieldNames.IsoCode);
            set => SetAttributeValue(FieldNames.IsoCode, value);
        }

        public struct FieldNames
        {
            public const string Id = "transactioncurrencyid";
            public const string CurrencyName = "currencyname";
            public const string IsoCode = "isocurrencycode";
        }
    }
}
