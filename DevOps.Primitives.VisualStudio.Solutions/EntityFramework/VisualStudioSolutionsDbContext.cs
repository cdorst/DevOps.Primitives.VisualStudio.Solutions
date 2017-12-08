using DevOps.Primitives.Strings.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Primitives.VisualStudio.Solutions.EntityFramework
{
    public class VisualStudioSolutionsDbContext : UniqueStringsDbContext
    {
        public VisualStudioSolutionsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Solution> Solutions { get; set; }
        public DbSet<SolutionFolder> SolutionFolders { get; set; }
        public DbSet<SolutionProject> SolutionProjects { get; set; }
    }
}
