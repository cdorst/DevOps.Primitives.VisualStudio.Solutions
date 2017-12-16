using DevOps.Primitives.Strings;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    [ProtoContract]
    [Table("Solutions", Schema = nameof(VisualStudio))]
    public class Solution
    {
        public Solution() { }
        public Solution(Guid guid, AsciiStringReference name, AsciiMaxStringReference versionBlock, SolutionFolderList solutionFolderList = null)
        {
            Guid = guid;
            Name = name;
            VersionBlock = versionBlock;
            SolutionFolderList = solutionFolderList;
        }
        public Solution(Guid guid, string name, string versionBlock, SolutionFolderList solutionFolderList = null)
            : this(guid, new AsciiStringReference(name), new AsciiMaxStringReference(versionBlock), solutionFolderList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int SolutionId { get; set; }

        [ProtoMember(2)]
        public Guid Guid { get; set; }

        [ProtoMember(3)]
        public AsciiStringReference Name { get; set; }
        [ProtoMember(4)]
        public int NameId { get; set; }

        [ProtoMember(5)]
        public AsciiMaxStringReference VersionBlock { get; set; }
        [ProtoMember(6)]
        public int VersionBlockId { get; set; }

        [ProtoMember(7)]
        public SolutionFolderList SolutionFolderList { get; set; }
        [ProtoMember(8)]
        public int? SolutionFolderListId { get; set; }

        public StringBuilder GetSolutionBuilder()
        {
            var builder = new StringBuilder().AppendLine(VersionBlock.Value);
            var folders = new List<SolutionFolder>();
            var projects = new List<SolutionProject>();
            if (SolutionFolderList != null)
            {
                folders.AddRange(SolutionFolderList.GetAssociations().Select(each => each.GetRecord()));
                foreach (var folder in folders)
                    builder.AppendLine(folder.GetSlnProjectDeclaration());
                projects.AddRange(folders.SelectMany(f => f.GetProjects()));
                foreach (var project in projects)
                    builder.AppendLine(project.GetSlnProjectDeclaration());
            }
            builder
                .AppendLine("Global")
                .AppendLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution")
                .AppendLine("\t\tDebug|Any CPU = Debug|Any CPU")
                .AppendLine("\t\tRelease|Any CPU = Release|Any CPU")
                .AppendLine("\tEndGlobalSection")
                .AppendLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
            foreach (var project in projects)
                builder.AppendLine(project.GetSlnProjectConfigurationPlatforms());
            builder
                .AppendLine("\tEndGlobalSection")
                .AppendLine("\tGlobalSection(SolutionProperties) = preSolution")
                .AppendLine("\t\tHideSolutionNode = FALSE")
                .AppendLine("\tEndGlobalSection")
                .AppendLine("\tGlobalSection(NestedProjects) = preSolution");
            foreach (var folder in folders)
                foreach (var project in folder.GetProjects() ?? new SolutionProject[] { })
                    builder.AppendLine(GetNestingAssignment(folder, project));
            return builder
                .AppendLine("\tEndGlobalSection")
                .AppendLine("\tGlobalSection(ExtensibilityGlobals) = preSolution")
                .AppendLine(GetSolutionGuidLine(Guid))
                .AppendLine("\tEndGlobalSection")
                .AppendLine("EndGlobal")
                .AppendLine();
        }

        private static string GetNestingAssignment(SolutionFolder folder, SolutionProject project)
            => SlnDeclarations.GetNestedProjectAssignment(
                folder.Guid,
                project.Guid);

        private static string GetSolutionGuidLine(Guid guid)
            => SlnDeclarations.GetSolutionGuidLine(guid);
    }
}
