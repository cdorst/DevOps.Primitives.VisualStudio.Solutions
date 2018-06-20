using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    [ProtoContract]
    [Table("SolutionFolders", Schema = nameof(VisualStudio))]
    public class SolutionFolder : IUniqueListRecord
    {
        public SolutionFolder() { }
        public SolutionFolder(in Guid guid, in AsciiStringReference name, in SolutionProjectList projectList = default)
        {
            Guid = guid;
            Name = name;
            SolutionProjectList = projectList;
        }
        public SolutionFolder(in Guid guid, in string name, in SolutionProjectList projectList = default)
            : this(in guid, new AsciiStringReference(in name), in projectList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int SolutionFolderId { get; set; }

        [ProtoMember(2)]
        public Guid Guid { get; set; }

        [ProtoMember(3)]
        public AsciiStringReference Name { get; set; }
        [ProtoMember(4)]
        public int NameId { get; set; }

        [ProtoMember(5)]
        public SolutionProjectList SolutionProjectList { get; set; }
        [ProtoMember(6)]
        public int? SolutionProjectListId { get; set; }

        public IEnumerable<SolutionProject> GetProjects()
            => SolutionProjectList?.GetAssociations().Select(each => each.GetRecord());

        public string GetSlnProjectDeclaration()
            => SlnDeclarations.GetProjectDeclaration(SlnGuidTypes.Folder,
                name: Name.Value,
                path: Name.Value,
                guid: Guid);
    }
}
