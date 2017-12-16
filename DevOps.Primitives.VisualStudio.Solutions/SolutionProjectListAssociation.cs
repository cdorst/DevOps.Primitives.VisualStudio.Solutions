using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.VisualStudio.Solutions
{
    [ProtoContract]
    [Table("SolutionProjectListAssociations", Schema = nameof(VisualStudio))]
    public class SolutionProjectListAssociation : IUniqueListAssociation<SolutionProject>
    {
        public SolutionProjectListAssociation() { }
        public SolutionProjectListAssociation(SolutionProject solutionProject, SolutionProjectList solutionProjectList = null)
        {
            SolutionProject = solutionProject;
            SolutionProjectList = solutionProjectList;
        }
        public SolutionProjectListAssociation(Guid guid, AsciiStringReference name, AsciiStringReference pathRelativeToSolution, SolutionProjectList solutionProjectList = null)
            : this(new SolutionProject(guid, name, pathRelativeToSolution), solutionProjectList)
        {
        }
        public SolutionProjectListAssociation(Guid guid, string name, string pathRelativeToSolution, SolutionProjectList solutionProjectList = null)
            : this(new SolutionProject(guid, name, pathRelativeToSolution), solutionProjectList)
        {
        }

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
