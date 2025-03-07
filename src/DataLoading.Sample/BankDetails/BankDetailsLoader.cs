using System.Threading.Tasks;
using Sharedo.Core;
using Sharedo.Core.Case.DataLoading.Etl.Loads;
using Sharedo.Core.Case.DataLoading.Etl.Scopes;
using Sharedo.Core.Case.Modules.ODS.BankDetails.Data;
using Sharedo.Core.Case.Modules.ODS.BankDetails.Entities;

namespace DataLoading.Sample.BankDetails
{
    /// <summary>
    /// The bank details loader loads entities from the import.* table into ShareDo.
    /// All implementations of <see cref="IDataloadLoader"/> must have a <see cref="LoaderInfoAttribute" /> attribute for the system to recognise it.
    /// </summary>
    [LoaderInfo("aardvark-bank-detail-loader")]
    public class BankDetailsLoader(
        IBankDetailCommands bankDetailCommands) : IDataloadLoader<BankDetailsContext>
    {
        /// <summary>
        /// Load the bank details into ShareDo.
        /// </summary>
        /// <param name="context">The context for the bank details.</param>
        /// <returns>The <see cref="LoadResultArgs"/> result.</returns>
        public Task<LoadResultArgs> Load(BankDetailsContext context)
        {
            // get the id of the loaded ods entity to associate with these bank details
            var odsId = context.Ods.GetRequiredProcessedId();

            var entity = MapToEntity(context);

            // create the bank details
            bankDetailCommands.Save(entity);

            // add the new bank details to the already laoded ods entity
            bankDetailCommands.AddBankDetailToEntity(odsId, entity.Id);

            var res = new LoadResultArgs()
                .SetProcessed(entity.Id);

            return Task.FromResult(res);
        }

        private BankDetailEntity MapToEntity(BankDetailsContext context)
        {
            return new BankDetailEntity
            {
                Id = Guids.NewComb(),
                FriendlyName = context.Entity.FriendlyName,
                AccountHoldersName = context.Entity.AccountHoldersName,
                BankName = context.Entity.BankName,
                BankSwiftBic = context.Entity.BankSwiftBic,
                IsBusinessAccount = context.Entity.IsBusinessAccount,
            };
        }
    }
}
