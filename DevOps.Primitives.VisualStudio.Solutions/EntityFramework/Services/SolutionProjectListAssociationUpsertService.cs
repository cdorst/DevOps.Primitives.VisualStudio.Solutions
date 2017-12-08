using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.VisualStudio.Solutions.EntityFramework.Services
{
    public class SolutionProjectListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, SolutionProjectListAssociation>
        where TDbContext : VisualStudioSolutionsDbContext
    {
        public SolutionProjectListAssociationUpsertService(ICacheService<SolutionProjectListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, SolutionProjectListAssociation>> logger)
            : base(cache, database, logger, database.SolutionProjectListAssociations)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(SolutionProjectListAssociation)}={record.SolutionProjectId}:{record.SolutionProjectListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(SolutionProjectListAssociation record)
        {
            yield return record.SolutionProject;
            yield return record.SolutionProjectList;
        }

        protected override Expression<Func<SolutionProjectListAssociation, bool>> FindExisting(SolutionProjectListAssociation record)
            => existing
                => existing.SolutionProjectId == record.SolutionProjectId
                && existing.SolutionProjectListId == record.SolutionProjectListId;
    }
}
