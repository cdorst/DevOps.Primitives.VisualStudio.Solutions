using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    [ProtoContract]
    [Table("SolutionFolderLists", Schema = nameof(VisualStudio))]
    public class SolutionFolderList : IUniqueList<SolutionFolder, SolutionFolderListAssociation>
    {
        [Key]
        [ProtoMember(1)]
        public int SolutionFolderListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<SolutionFolderListAssociation> SolutionFolderListAssociations { get; set; }

        public List<SolutionFolderListAssociation> GetAssociations() => SolutionFolderListAssociations;

        public void SetRecords(List<SolutionFolder> records)
        {
            SolutionFolderListAssociations = UniqueListAssociationsFactory<SolutionFolder, SolutionFolderListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<SolutionFolder>.Create(records, r => r.SolutionFolderId));
        }
    }
}
