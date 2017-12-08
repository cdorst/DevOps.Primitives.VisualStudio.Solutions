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
        public DbSet<SolutionProjectList> SolutionProjectLists { get; set; }
        public DbSet<SolutionProjectListAssociation> SolutionProjectListAssociations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddIndexes(modelBuilder);
        }

        private void AddIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Solution>()
                .HasIndex(e => new { e.Guid, e.NameId, e.SolutionFolderListId }).IsUnique();
            modelBuilder.Entity<SolutionFolder>()
                .HasIndex(e => new { e.Guid, e.NameId, e.SolutionProjectListId }).IsUnique();
            modelBuilder.Entity<SolutionFolderList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<SolutionFolderListAssociation>()
                .HasIndex(e => new { e.SolutionFolderId, e.SolutionFolderListId }).IsUnique();
            modelBuilder.Entity<SolutionProject>()
                .HasIndex(e => new { e.Guid, e.NameId, e.PathRelativeToSolutionId }).IsUnique();
            modelBuilder.Entity<SolutionProjectList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<SolutionProjectListAssociation>()
                .HasIndex(e => new { e.SolutionProjectId, e.SolutionProjectListId }).IsUnique();
        }
    }
}
