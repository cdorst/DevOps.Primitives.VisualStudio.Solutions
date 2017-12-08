using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Solutions.EntityFramework.Services
{
    public class SolutionFolderListUpsertService<TDbContext> : UpsertService<TDbContext, SolutionFolderList>
        where TDbContext : VisualStudioSolutionsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public SolutionFolderListUpsertService(ICacheService<SolutionFolderList> cache, TDbContext database, ILogger<UpsertService<TDbContext, SolutionFolderList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.SolutionFolderLists)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(SolutionFolderList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<SolutionFolderList> AssignUpsertedReferences(SolutionFolderList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(SolutionFolderList record)
        {
            yield return record.ListIdentifier;
            yield return record.SolutionFolderListAssociations;
        }

        protected override Expression<Func<SolutionFolderList, bool>> FindExisting(SolutionFolderList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
