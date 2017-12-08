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
    [Table("SolutionProjectLists", Schema = nameof(VisualStudio))]
    public class SolutionProjectList : IUniqueList<SolutionProject, SolutionProjectListAssociation>
    {
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

        public void SetRecords(List<SolutionProject> records)
        {
            SolutionProjectListAssociations = UniqueListAssociationsFactory<SolutionProject, SolutionProjectListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<SolutionProject>.Create(records, r => r.SolutionProjectId));
        }
    }
}
