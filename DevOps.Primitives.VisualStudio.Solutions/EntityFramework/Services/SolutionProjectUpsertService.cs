using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.VisualStudio.Solutions.EntityFramework.Services
{
    public class SolutionProjectUpsertService<TDbContext> : UpsertService<TDbContext, SolutionProject>
        where TDbContext : VisualStudioSolutionsDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public SolutionProjectUpsertService(ICacheService<SolutionProject> cache, TDbContext database, ILogger<UpsertService<TDbContext, SolutionProject>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.SolutionProjects)
        {
            CacheKey = record => $"{nameof(VisualStudio)}.{nameof(SolutionProject)}={record.Guid}:{record.NameId}:{record.PathRelativeToSolutionId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<SolutionProject> AssignUpsertedReferences(SolutionProject record)
        {
            record.Name = await _strings.UpsertAsync(record.Name);
            record.NameId = record.Name?.AsciiStringReferenceId ?? record.NameId;
            record.PathRelativeToSolution = await _strings.UpsertAsync(record.PathRelativeToSolution);
            record.PathRelativeToSolutionId = record.PathRelativeToSolution?.AsciiStringReferenceId ?? record.PathRelativeToSolutionId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(SolutionProject record)
        {
            yield return record.Name;
            yield return record.PathRelativeToSolution;
        }

        protected override Expression<Func<SolutionProject, bool>> FindExisting(SolutionProject record)
            => existing
                => existing.Guid == record.Guid
                && existing.NameId == record.NameId
                && existing.PathRelativeToSolutionId == record.PathRelativeToSolutionId;
    }
}
