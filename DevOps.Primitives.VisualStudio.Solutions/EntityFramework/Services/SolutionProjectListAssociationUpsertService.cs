using Common.EntityFrameworkServices;
using DevOps.Primitives.VisualStudio.Solutions;
using DevOps.Primitives.VisualStudio.Solutions.EntityFramework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Projects.EntityFramework.Services
{
    public class SolutionProjectListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, SolutionFolderListAssociation>
        where TDbContext : VisualStudioSolutionsDbContext
    {
        public SolutionProjectListAssociationUpsertService(ICacheService<SolutionFolderListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, SolutionFolderListAssociation>> logger)
            : base(cache, database, logger, database.SolutionFolderListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(SolutionFolderListAssociation)}={record.SolutionFolderId}:{record.SolutionFolderListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(SolutionFolderListAssociation record)
        {
            yield return record.SolutionFolder;
            yield return record.SolutionFolderList;
        }

        protected override Expression<Func<SolutionFolderListAssociation, bool>> FindExisting(SolutionFolderListAssociation record)
            => existing
                => existing.SolutionFolderId == record.SolutionFolderId
                && existing.SolutionFolderListId == record.SolutionFolderListId;
    }
}
