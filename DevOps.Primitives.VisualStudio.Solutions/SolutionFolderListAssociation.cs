using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    [ProtoContract]
    [Table("SolutionFolderListAssociations", Schema = nameof(VisualStudio))]
    public class SolutionFolderListAssociation : IUniqueListAssociation<SolutionFolder>
    {
        public SolutionFolderListAssociation() { }
        public SolutionFolderListAssociation(in SolutionFolder solutionFolder, in SolutionFolderList solutionFolderList = default)
        {
            SolutionFolder = solutionFolder;
            SolutionFolderList = solutionFolderList;
        }
        public SolutionFolderListAssociation(in Guid guid, in AsciiStringReference name, in SolutionProjectList projectList = default, in SolutionFolderList solutionFolderList = default)
            : this(new SolutionFolder(in guid, in name, in projectList), in solutionFolderList)
        {
        }
        public SolutionFolderListAssociation(in Guid guid, in string name, in SolutionProjectList projectList = default, in SolutionFolderList solutionFolderList = default)
            : this(new SolutionFolder(in guid, in name, in projectList), in solutionFolderList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int SolutionFolderListAssociationId { get; set; }

        [ProtoMember(2)]
        public SolutionFolder SolutionFolder { get; set; }
        [ProtoMember(3)]
        public int SolutionFolderId { get; set; }

        [ProtoMember(4)]
        public SolutionFolderList SolutionFolderList { get; set; }
        [ProtoMember(5)]
        public int SolutionFolderListId { get; set; }

        public SolutionFolder GetRecord() => SolutionFolder;

        public void SetRecord(in SolutionFolder record)
        {
            SolutionFolder = record;
            SolutionFolderId = record.SolutionFolderId;
        }
    }
}
