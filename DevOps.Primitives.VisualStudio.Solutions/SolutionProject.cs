using DevOps.Primitives.Strings;
using ProtoBuf;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    [ProtoContract]
    [Table("SolutionProjects", Schema = nameof(VisualStudio))]
    public class SolutionProject
    {
        [Key]
        [ProtoMember(1)]
        public int SolutionProjectId { get; set; }

        [ProtoMember(2)]
        public Guid Guid { get; set; }

        [ProtoMember(3)]
        public AsciiStringReference Name { get; set; }
        [ProtoMember(4)]
        public int NameId { get; set; }

        [ProtoMember(5)]
        public AsciiStringReference PathRelativeToSolution { get; set; }
        [ProtoMember(6)]
        public int PathRelativeToSolutionId { get; set; }

        public string GetSlnProjectConfigurationPlatforms()
            => SlnDeclarations.GetGlobalProjectConfigurationPlatforms(Guid);

        public string GetSlnProjectDeclaration()
            => SlnDeclarations.GetProjectDeclaration(SlnGuidTypes.Project,
                name: Name.Value,
                path: GetSlnProjectDeclarationPath(),
                guid: Guid);

        public string GetCsprojFileName() => $"{Name.Value}.csproj";

        private string GetSlnProjectDeclarationPath()
        {
            var path = PathRelativeToSolution.Value;
            var fileName = GetCsprojFileName();
            if (!path.EndsWith(fileName)) path = Path.Combine(path, fileName);
            return path;
        }
    }
}
