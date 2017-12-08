using System;
using System.Text;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    public static class SlnDeclarations
    {
        public static string GetProjectDeclaration(Guid projectType, string name, string path, Guid guid)
            => $"Project(\"{Brace(projectType)}\") = \"{name}\", \"{path}\", \"{Brace(guid)}\"{Environment.NewLine}EndProject";

        public static string GetGlobalProjectConfigurationPlatforms(Guid guid)
            => new StringBuilder($"{GlobalSectionItem(guid)}.Debug|Any CPU.ActiveCfg = Debug|Any CPU")
                .AppendLine($"{GlobalSectionItem(guid)}.Debug|Any CPU.Build.0 = Debug|Any CPU")
                .AppendLine($"{GlobalSectionItem(guid)}.Release|Any CPU.ActiveCfg = Release|Any CPU")
                .AppendLine($"{GlobalSectionItem(guid)}.Release|Any CPU.Build.0 = Release|Any CPU")
                .ToString();

        public static string GetNestedProjectAssignment(Guid folder, Guid project)
            => $"{Brace(project)} = {Brace(folder)}";

        public static string GetSolutionGuidLine(Guid guid)
            => $"\t\tSolutionGuid = {Brace(guid)}";

        private static string GlobalSectionItem(Guid guid) => $"\t\t{Brace(guid)}";

        private static string Brace(Guid guid) => $"{{{Uppercase(guid)}}}";

        private static string Uppercase(Guid guid) => guid.ToString().ToUpper();
    }
}
