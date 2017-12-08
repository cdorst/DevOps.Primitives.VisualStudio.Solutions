using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    [ProtoContract]
    [Table("SolutionProjectListAssociations", Schema = nameof(VisualStudio))]
    public class SolutionProjectListAssociation : IUniqueListAssociation<SolutionProject>
    {
        [Key]
        [ProtoMember(1)]
        public int SolutionProjectListAssociationId { get; set; }

        [ProtoMember(2)]
        public SolutionProject SolutionProject { get; set; }
        [ProtoMember(3)]
        public int SolutionProjectId { get; set; }

        [ProtoMember(4)]
        public SolutionProjectList SolutionProjectList { get; set; }
        [ProtoMember(5)]
        public int SolutionProjectListId { get; set; }

        public SolutionProject GetRecord() => SolutionProject;

        public void SetRecord(SolutionProject record)
        {
            SolutionProject = record;
            SolutionProjectId = SolutionProject.SolutionProjectId;
        }
    }
}
