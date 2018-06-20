using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    [ProtoContract]
    [Table("SolutionFolderLists", Schema = nameof(VisualStudio))]
    public class SolutionFolderList : IUniqueList<SolutionFolder, SolutionFolderListAssociation>
    {
        public SolutionFolderList() { }
        public SolutionFolderList(in List<SolutionFolderListAssociation> associations, in AsciiStringReference listIdentifier = default)
        {
            SolutionFolderListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public SolutionFolderList(in SolutionFolderListAssociation associations, in AsciiStringReference listIdentifier = default)
            : this(new List<SolutionFolderListAssociation> { associations }, in listIdentifier)
        {
        }
        public SolutionFolderList(in SolutionFolder solutionFolder, in AsciiStringReference listIdentifier = default)
            : this(new SolutionFolderListAssociation(solutionFolder), in listIdentifier)
        {
        }
        public SolutionFolderList(in Guid guid, in AsciiStringReference name, in SolutionProjectList projectList = default, in AsciiStringReference listIdentifier = default)
            : this(new SolutionFolder(in guid, in name, in projectList), in listIdentifier)
        {
        }
        public SolutionFolderList(in Guid guid, in string name, in SolutionProjectList projectList = default, in AsciiStringReference listIdentifier = default)
            : this(new SolutionFolder(in guid, in name, in projectList), in listIdentifier)
        {
        }

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

        public void SetRecords(in List<SolutionFolder> records)
        {
            SolutionFolderListAssociations = UniqueListAssociationsFactory<SolutionFolder, SolutionFolderListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<SolutionFolder>.Create(in records, r => r.SolutionFolderId));
        }
    }
}
