using System;
using System.Threading.Tasks;
using Sharedo.Core.Case.DataLoading.Etl.Contexts;
using Sharedo.Core.Case.DataLoading.Etl.Processings;
using Sharedo.Core.Case.Modules.ODS.BankDetails.Data;
using Sharedo.Core.Logging;

namespace DataLoading.Sample.BankDetails
{
    /// <summary>
    /// An example processing step which will attempt to match the bank details with an existing bankDetails entity in ShareDo
    /// </summary>
    /// <param name="bankDetailQueries"></param>
    /// <param name="log"></param>
    [ProcessingStepInfo("bank-details-matcher", "Matches bank details using the match unique id", 1)]
    public class BankDetailsMatcher(
        IBankDetailQueries bankDetailQueries,
        ILogger log) : IProcessingStep<BankDetailsContext>
    {
        /// <summary>
        /// Invoke the processing step.
        /// </summary>
        /// <param name="next">Func to call the next processing step in pipeline.</param>
        /// <param name="context">The bank details context.</param>
        /// <returns>The task.</returns>
        public async Task Invoke(Func<BankDetailsContext, Task> next, BankDetailsContext context)
        {
            Guid? id = null;

            if (context.Entity.MatchUniqueId.HasValue)
            {
                var existingBankDetails = bankDetailQueries.GetEntity(context.Entity.MatchUniqueId.Value);

                // if entity found, set context as matched and do not call next()
                if (existingBankDetails != null)
                {
                    log.Information("PersonMatcher:Invoke Found matching bank details '{id}' for matched unique id {matchUniqueId}", existingBankDetails.Id, context.Entity.MatchUniqueId);

                    context.SetIsMatched(id.Value);
                    return;
                }
            }

            // no entity is found so call next step
            await next(context)
                .ConfigureAwait(false);
        }
    }
}
