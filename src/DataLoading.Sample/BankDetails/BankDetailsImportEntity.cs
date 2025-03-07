using System;
using Sharedo.Core.Case.DataLoading.Etl.Scopes;

namespace DataLoading.Sample.BankDetails
{
    /// <summary>
    /// The bank details import entity is class used to retrieve data from the source.* table.
    /// Import entities must extend <see cref="ImportEntity" /> 
    /// </summary>
    public class BankDetailsImportEntity : ImportEntity
    {
        public string OdsReference { get; set; }

        public Guid? MatchUniqueId { get; set; }

        public string FriendlyName { get; set; }

        public string AccountHoldersName { get; set; }

        public string BankName { get; set; }

        public string BankSwiftBic { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsBusinessAccount { get; set; }
    }
}
