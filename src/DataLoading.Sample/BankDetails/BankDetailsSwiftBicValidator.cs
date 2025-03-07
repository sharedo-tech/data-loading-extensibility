using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sharedo.Core.Case.DataLoading.Etl.DataLoads;
using Sharedo.Core.Case.DataLoading.Etl.Validations;

namespace DataLoading.Sample.BankDetails
{
    /// <summary>
    /// Validator to check the bank details swift bix code is valid format.
    /// Validator must have <see cref="ValidatorInfoAttribute"/> for the system to recognise it.
    /// </summary>
    [ValidatorInfo("bank-details-swift-bic", "Validates the bank details swift bic code")]
    public class BankDetailsSwiftBicValidator : IEntityValidater<BankDetailsContext>
    {
        private readonly Regex SwiftCodeRegex = new("^([a-zA-Z]){4}([a-zA-Z]){2}([0-9a-zA-Z]){2}([0-9a-zA-Z]{3})?$");

        /// <summary>
        /// Build message for the UI to diplay group of issues returned from this validator.
        /// </summary>
        /// <param name="context">The bank details context.</param>
        /// <returns>The formatted message.</returns>
        public string BuildMessage(ValidatedGroupContext ctx) => $"{ctx.Count} bank details have invalid swift bic codes";

        /// <summary>
        /// Validate the bank details context.
        /// </summary>
        /// <param name="context">The bank details context.</param>
        /// <returns>All detected issues.</returns>
        public async IAsyncEnumerable<IssueArgs> Validate(BankDetailsContext context)
        {
            if (!SwiftCodeRegex.Match(context.Entity.BankSwiftBic).Success)
            {
                yield return IssueArgs.Error($"The swift code {context.Entity.BankSwiftBic} is invalid");
            }

            await Task.CompletedTask;
        }
    }
}
