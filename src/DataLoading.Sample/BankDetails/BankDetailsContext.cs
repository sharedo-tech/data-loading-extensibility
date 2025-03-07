using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sharedo.Core.Case.DataLoading.Etl.Contexts;
using Sharedo.Core.Case.DataLoading.Etl.Data;
using Sharedo.Core.Case.DataLoading.Etl.Scopes.Ods;

namespace DataLoading.Sample.BankDetails
{
    /// <summary>
    /// The bank details context used to provide contextual data when processing and loading the bank details entity.
    /// </summary>
    /// <param name="args">The entity context args.</param>
    /// <param name="ods">The associated ods entity using these bank details.</param>
    public class BankDetailsContext(
            EntityContextArgs<BankDetailsImportEntity> args,
            OdsImportEntity ods)
        : EntityContext<BankDetailsImportEntity>(args)
    {
        /// <summary>
        /// The ods entity to be associated with these bank details.
        /// </summary>
        public OdsImportEntity Ods { get; } = ods;
    }

    /// <summary>
    /// The bank details context loader, loads all of the arguments to return collection of contexts for the batch of requested import entities.
    /// </summary>
    /// <param name="importQueries">The data loading import queries, used to retrieve other import entities within data load.</param>
    public class BankDetailsContextLoader(IImportQueries importQueries) : IEntityContextLoader<BankDetailsContext, BankDetailsImportEntity>
    {
        public async Task<IList<BankDetailsContext>> Load(IEnumerable<EntityContextArgs<BankDetailsImportEntity>> request)
        {
            var odsReferences = request.Select(args => args.Entity.OdsReference);

            var odsEntities = await importQueries.GetEntities<OdsImportEntity>(odsReferences)
                .ConfigureAwait(false);

            var res = request
                .Select(args => new BankDetailsContext(
                    args,
                    odsEntities.FirstOrDefault(p => p.Reference == args.Entity.OdsReference)))
                .ToList();

            return res;
        }
    }
}
