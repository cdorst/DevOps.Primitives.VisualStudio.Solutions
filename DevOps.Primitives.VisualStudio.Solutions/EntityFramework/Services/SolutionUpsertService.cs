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
    public class SolutionUpsertService<TDbContext> : UpsertService<TDbContext, Solution>
        where TDbContext : VisualStudioSolutionsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;
        private readonly IUpsertUniqueListService<TDbContext, SolutionFolder, SolutionFolderList, SolutionFolderListAssociation> _folders;

        public SolutionUpsertService(ICacheService<Solution> cache, TDbContext database, ILogger<UpsertService<TDbContext, Solution>> logger, IUpsertService<TDbContext, AsciiStringReference> strings, IUpsertUniqueListService<TDbContext, SolutionFolder, SolutionFolderList, SolutionFolderListAssociation> folders)
            : base(cache, database, logger, database.Solutions)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(Solution)}={record.Guid}:{record.NameId}:{record.SolutionFolderListId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
            _folders = folders ?? throw new ArgumentNullException(nameof(folders));
        }

        protected override async Task<Solution> AssignUpsertedReferences(Solution record)
        {
            record.Name = await _strings.UpsertAsync(record.Name);
            record.NameId = record.Name?.AsciiStringReferenceId ?? record.NameId;
            record.SolutionFolderList = await _folders.UpsertAsync(record.SolutionFolderList);
            record.SolutionFolderListId = record.SolutionFolderList?.SolutionFolderListId ?? record.SolutionFolderListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Solution record)
        {
            yield return record.Name;
            yield return record.SolutionFolderListId;
        }

        protected override Expression<Func<Solution, bool>> FindExisting(Solution record)
            => existing
                => ((existing.SolutionFolderListId == null && record.SolutionFolderListId == null) || (existing.SolutionFolderListId == record.SolutionFolderListId))
                && existing.Guid == record.Guid
                && existing.NameId == record.NameId;
    }
}
