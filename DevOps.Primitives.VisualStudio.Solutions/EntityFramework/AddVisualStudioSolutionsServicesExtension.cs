using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings.EntityFramework;
using DevOps.Primitives.VisualStudio.Solutions.EntityFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevOps.Primitives.VisualStudio.Solutions.EntityFramework
{
    public static class AddVisualStudioSolutionsServicesExtension
    {
        public static IServiceCollection AddVisualStudioProjectsServices<TDbContext>(this IServiceCollection services)
            where TDbContext : VisualStudioSolutionsDbContext
            => services
                .AddUniqueStringsServices<TDbContext>()
                .AddScoped<IUpsertService<TDbContext, Solution>, SolutionUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, SolutionFolder>, SolutionFolderUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, SolutionFolderList>, SolutionFolderListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, SolutionFolderListAssociation>, SolutionFolderListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, SolutionProject>, SolutionProjectUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, SolutionProjectList>, SolutionProjectListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, SolutionProjectListAssociation>, SolutionProjectListAssociationUpsertService<TDbContext>>();
    }
}
