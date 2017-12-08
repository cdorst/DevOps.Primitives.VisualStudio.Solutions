using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    [ProtoContract]
    [Table("SolutionFolderListAssociations", Schema = nameof(VisualStudio))]
    public class SolutionFolderListAssociation : IUniqueListAssociation<SolutionFolder>
    {
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

        public void SetRecord(SolutionFolder record)
        {
            SolutionFolder = record;
            SolutionFolderId = SolutionFolder.SolutionFolderId;
        }
    }
}
