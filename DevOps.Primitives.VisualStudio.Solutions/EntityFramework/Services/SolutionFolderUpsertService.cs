using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Solutions.EntityFramework.Services
{
    public class SolutionFolderUpsertService<TDbContext> : UpsertService<TDbContext, SolutionFolder>
        where TDbContext : VisualStudioSolutionsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;
        private readonly IUpsertUniqueListService<TDbContext, SolutionProject, SolutionProjectList, SolutionProjectListAssociation> _projects;

        public SolutionFolderUpsertService(ICacheService<SolutionFolder> cache, TDbContext database, ILogger<UpsertService<TDbContext, SolutionFolder>> logger, IUpsertService<TDbContext, AsciiStringReference> strings, IUpsertUniqueListService<TDbContext, SolutionProject, SolutionProjectList, SolutionProjectListAssociation> projects)
            : base(cache, database, logger, database.SolutionFolders)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(SolutionFolder)}={record.Guid}:{record.NameId}:{record.SolutionProjectListId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
            _projects = projects ?? throw new ArgumentNullException(nameof(projects));
        }

        protected override async Task<SolutionFolder> AssignUpsertedReferences(SolutionFolder record)
        {
            record.Name = await _strings.UpsertAsync(record.Name);
            record.NameId = record.Name?.AsciiStringReferenceId ?? record.NameId;
            record.SolutionProjectList = await _projects.UpsertAsync(record.SolutionProjectList);
            record.SolutionProjectListId = record.SolutionProjectList?.SolutionProjectListId ?? record.SolutionProjectListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(SolutionFolder record)
        {
            yield return record.Name;
            yield return record.SolutionProjectListId;
        }

        protected override Expression<Func<SolutionFolder, bool>> FindExisting(SolutionFolder record)
            => existing
                => ((existing.SolutionProjectListId == null && record.SolutionProjectListId == null) || (existing.SolutionProjectListId == record.SolutionProjectListId))
                && existing.Guid == record.Guid
                && existing.NameId == record.NameId;
    }
}
