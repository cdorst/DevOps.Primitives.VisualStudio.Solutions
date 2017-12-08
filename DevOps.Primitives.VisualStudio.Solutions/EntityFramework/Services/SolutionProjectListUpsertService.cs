using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using DevOps.Primitives.VisualStudio.Solutions;
using DevOps.Primitives.VisualStudio.Solutions.EntityFramework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class SolutionProjectListUpsertService<TDbContext> : UpsertService<TDbContext, SolutionProjectList>
        where TDbContext : VisualStudioSolutionsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public SolutionProjectListUpsertService(ICacheService<SolutionProjectList> cache, TDbContext database, ILogger<UpsertService<TDbContext, SolutionProjectList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.SolutionProjectLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(SolutionProjectList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<SolutionProjectList> AssignUpsertedReferences(SolutionProjectList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(SolutionProjectList record)
        {
            yield return record.ListIdentifier;
            yield return record.SolutionProjectListAssociations;
        }

        protected override Expression<Func<SolutionProjectList, bool>> FindExisting(SolutionProjectList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
