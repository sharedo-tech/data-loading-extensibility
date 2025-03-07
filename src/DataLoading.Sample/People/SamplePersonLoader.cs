using System.Threading.Tasks;
using Sharedo.Core.Case.Data.Entities.Ods;
using Sharedo.Core.Case.Data.Services.Ods;
using Sharedo.Core.Case.DataLoading.Etl.Loads;
using Sharedo.Core.Case.DataLoading.Etl.Scopes.Ods.People;
using Sharedo.Core.DateAndTime;

namespace DataLoading.Sample.People
{
    /// <summary>
    /// An example loader for the existing "People" scope.  No registration is required the system will automatically detect any new loaders.
    /// </summary>
    /// <param name="peopleCommands">The people commands.</param>
    [LoaderInfo("aardvark-people-loader")]
    public class SamplePersonLoader(IPeopleCommands peopleCommands) : IDataloadLoader<PersonContext>
    {
        /// <summary>
        /// Load the person into ShareDo.
        /// </summary>
        /// <param name="context">The context for the person.</param>
        /// <returns>The <see cref="LoadResultArgs"/> result.</returns>
        public Task<LoadResultArgs> Load(PersonContext context)
        {
            var person = MapToEntity(context);

            peopleCommands.SavePerson(person);

            var res = new LoadResultArgs()
                .SetProcessed(person.Id);

            return Task.FromResult(res);
        }

        private Person MapToEntity(PersonContext context)
        {
            var res = new Person
            {
                FirstName = context.Entity.Firstname,
                Surname = context.Entity.Surname,
                MiddleNameOrInitial = context.Entity.MiddlenameOrInitial,
                DateOfBirth = PureDate.FromDateTime(context.Entity.DateOfBirth),
                DateOfDeath = PureDate.FromDateTime(context.Entity.DateOfDeath),
                NINumber = context.Entity.NiNumber,
                PrimaryTeamId = context.Entity.PrimaryTeamId,
                Reference = context.Entity.PersonReference,

                // change presence identity to show person has been loaded by this loader.
                PresenceIdentity = $"sample-{context.Entity.Firstname}.{context.Entity.Surname}@sample.com"
            };

            if (int.TryParse(context.Entity.Gender, out int genderId))
            {
                res.GenderId = genderId;
            }

            if (int.TryParse(context.Entity.Title, out int titleId))
            {
                res.TitleId = titleId;
            }

            return res;
        }
    }
}
