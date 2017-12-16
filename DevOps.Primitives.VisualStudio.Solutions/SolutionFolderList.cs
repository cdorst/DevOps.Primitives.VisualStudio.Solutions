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
        public SolutionFolderList(List<SolutionFolderListAssociation> associations, AsciiStringReference listIdentifier = null)
        {
            SolutionFolderListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public SolutionFolderList(SolutionFolderListAssociation associations, AsciiStringReference listIdentifier = null)
            : this(new List<SolutionFolderListAssociation> { associations }, listIdentifier)
        {
        }
        public SolutionFolderList(SolutionFolder solutionFolder, AsciiStringReference listIdentifier = null)
            : this(new SolutionFolderListAssociation(solutionFolder), listIdentifier)
        {
        }
        public SolutionFolderList(Guid guid, AsciiStringReference name, SolutionProjectList projectList = null, AsciiStringReference listIdentifier = null)
            : this(new SolutionFolder(guid, name, projectList), listIdentifier)
        {
        }
        public SolutionFolderList(Guid guid, string name, SolutionProjectList projectList = null, AsciiStringReference listIdentifier = null)
            : this(new SolutionFolder(guid, name, projectList), listIdentifier)
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

        public void SetRecords(List<SolutionFolder> records)
        {
            SolutionFolderListAssociations = UniqueListAssociationsFactory<SolutionFolder, SolutionFolderListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<SolutionFolder>.Create(records, r => r.SolutionFolderId));
        }
    }
}
