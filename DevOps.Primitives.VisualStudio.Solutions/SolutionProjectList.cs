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
    [Table("SolutionProjectLists", Schema = nameof(VisualStudio))]
    public class SolutionProjectList : IUniqueList<SolutionProject, SolutionProjectListAssociation>
    {
        public SolutionProjectList() { }
        public SolutionProjectList(in List<SolutionProjectListAssociation> associations, in AsciiStringReference listIdentifier = default)
        {
            SolutionProjectListAssociations = associations;
            ListIdentifier = listIdentifier;
        }
        public SolutionProjectList(in SolutionProjectListAssociation associations, in AsciiStringReference listIdentifier = default)
            : this(new List<SolutionProjectListAssociation> { associations }, in listIdentifier)
        {
        }
        public SolutionProjectList(in SolutionProject solutionProject, in AsciiStringReference listIdentifier = default)
            : this(new SolutionProjectListAssociation(solutionProject), in listIdentifier)
        {
        }
        public SolutionProjectList(in Guid guid, in AsciiStringReference name, in AsciiStringReference pathRelativeToSolution, in AsciiStringReference listIdentifier = default)
            : this(new SolutionProject(in guid, in name, in pathRelativeToSolution), in listIdentifier)
        {
        }
        public SolutionProjectList(in Guid guid, in string name, in string pathRelativeToSolution, in AsciiStringReference listIdentifier = default)
            : this(new SolutionProject(in guid, in name, in pathRelativeToSolution), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int SolutionProjectListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<SolutionProjectListAssociation> SolutionProjectListAssociations { get; set; }

        public List<SolutionProjectListAssociation> GetAssociations() => SolutionProjectListAssociations;

        public void SetRecords(in List<SolutionProject> records)
        {
            SolutionProjectListAssociations = UniqueListAssociationsFactory<SolutionProject, SolutionProjectListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<SolutionProject>.Create(in records, r => r.SolutionProjectId));
        }
    }
}
