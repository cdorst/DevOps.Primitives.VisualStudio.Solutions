﻿using DevOps.Primitives.Strings;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    [ProtoContract]
    [Table("Solutions", Schema = nameof(VisualStudio))]
    public class Solution
    {
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
        public List<SolutionFolder> SolutionFolders { get; set; }

        public StringBuilder GetSolutionBuilder()
        {
            var builder = new StringBuilder()
                .AppendLine("Microsoft Visual Studio Solution File, Format Version 12.00")
                .AppendLine("# Visual Studio 15")
                .AppendLine("VisualStudioVersion = 15.0.27004.2010")
                .AppendLine("MinimumVisualStudioVersion = 10.0.40219.1");
            foreach (var folder in SolutionFolders)
                builder.AppendLine(folder.GetSlnProjectDeclaration());
            var projects = SolutionFolders.SelectMany(f => f.Projects);
            foreach (var project in projects)
                builder.AppendLine(project.GetSlnProjectDeclaration());
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
            foreach (var folder in SolutionFolders)
                foreach (var project in folder.Projects)
                    builder.AppendLine(GetNestingAssignment(folder, project));
            return builder
                .AppendLine("\tEndGlobalSection")
                .AppendLine("\tGlobalSection(ExtensibilityGlobals) = preSolution")
                .AppendLine(GetSolutionGuidLine())
                .AppendLine("\tEndGlobalSection")
                .AppendLine("EndGlobal")
                .AppendLine();
        }

        private static string GetNestingAssignment(SolutionFolder folder, SolutionProject project)
            => SlnDeclarations.GetNestedProjectAssignment(
                folder.Guid,
                project.Guid);

        private string GetSolutionGuidLine()
            => SlnDeclarations.GetSolutionGuidLine(Guid);
    }
}