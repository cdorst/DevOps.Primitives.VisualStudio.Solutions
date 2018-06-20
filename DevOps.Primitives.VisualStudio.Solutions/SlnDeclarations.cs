using System;
using static System.String;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    public static class SlnDeclarations
    {
        public static string GetProjectDeclaration(in Guid projectType, in string name, in string path, in Guid guid)
            => Concat("Project(\"", Brace(in projectType), "\") = \"", name, "\", \"", path, "\", \"", Brace(in guid), "\"\r\nEndProject");

        public static string GetGlobalProjectConfigurationPlatforms(in Guid guid)
        {
            var item = GlobalSectionItem(in guid);
            return Concat(item,  ".Debug|Any CPU.ActiveCfg = Debug|Any CPU\r\n", item, ".Debug|Any CPU.Build.0 = Debug|Any CPU\r\n", item, ".Release|Any CPU.ActiveCfg = Release|Any CPU\r\n", item, ".Release|Any CPU.Build.0 = Release|Any CPU");
        }

        public static string GetNestedProjectAssignment(in Guid folder, in Guid project)
            => Concat(Brace(in project), " = ", Brace(in folder));

        public static string GetSolutionGuidLine(in Guid guid)
            => Concat("\t\tSolutionGuid = ", Brace(in guid));

        private static string GlobalSectionItem(in Guid guid)
            => Concat("\t\t", Brace(in guid));

        private static string Brace(in Guid guid)
            => Concat("{", guid.ToString().ToUpper(), "}");
    }
}
