using DataLoading.Sample.BankDetails;
using Sharedo.Core.Case.DataLoading.Etl.Configuration;

namespace DataLoading.Sample
{
    /// <summary>
    /// The configuration builder for this data loading sample.
    /// </summary>
    public class SampleScopesConfiguration : IDataLoadConfigurationBuilder
    {
        public static readonly string BankDetailsScopeName = "bank-details";

        public void Build(ConfigurationBuilder builder)
        {
            builder
                // create a scope, providing the import entity, system name and display name.
                .Scope<BankDetailsImportEntity>(BankDetailsScopeName, "Bank Details")
                // explicitly define the source table name
                .WithSourceTable("source.bankDetails")
                // explicitly define the import table name within ShareDo
                .WithImportTable("import.bankDetails")
                // the display category
                .WithCategory("Aardvark")
                /// the default loader to use
                .WithDefaultLoader<BankDetailsLoader>()
                // the context and context loader to use when processing and loading
                .WithContext<BankDetailsContext, BankDetailsContextLoader>()
                // set priority to 100 so that is loaded after ods entities
                .WithPriority(100);
        }
    }
}
