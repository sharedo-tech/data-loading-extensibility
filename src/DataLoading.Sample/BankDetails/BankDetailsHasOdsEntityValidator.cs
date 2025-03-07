using System.Collections.Generic;
using System.Threading.Tasks;
using Sharedo.Core.Case.DataLoading.Etl.DataLoads;
using Sharedo.Core.Case.DataLoading.Etl.Validations;

namespace DataLoading.Sample.BankDetails
{
    /// <summary>
    /// Validator to check the bank details has an associated ods entity.
    /// Validator must have <see cref="ValidatorInfoAttribute"/> for the system to recognise it.
    /// </summary>
    [ValidatorInfo("bank-details-has-ods", "Validates the bank details has an associated ods entity")]
    public class BankDetailsHasOdsEntityValidator : IEntityValidater<BankDetailsContext>
    {
        /// <summary>
        /// Build message for the UI to diplay group of issues returned from this validator.
        /// </summary>
        /// <param name="context">The bank details context.</param>
        /// <returns>The formatted message.</returns>
        public string BuildMessage(ValidatedGroupContext context) => $"{context.Count} bank details do not have an associated ods entity";

        /// <summary>
        /// Validate the bank details context.
        /// </summary>
        /// <param name="context">The bank details context.</param>
        /// <returns>All detected issues.</returns>
        public async IAsyncEnumerable<IssueArgs> Validate(BankDetailsContext context)
        {
            if (context.Ods == null)
            {
                yield return IssueArgs.Error($"The bank details do not have a required ods with reference '{context.Entity.OdsReference}'");
            }

            await Task.CompletedTask;
        }
    }
}
