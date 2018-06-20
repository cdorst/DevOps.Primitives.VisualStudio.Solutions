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
        public SolutionProjectListAssociation(in SolutionProject solutionProject, in SolutionProjectList solutionProjectList = default)
        {
            SolutionProject = solutionProject;
            SolutionProjectList = solutionProjectList;
        }
        public SolutionProjectListAssociation(in Guid guid, in AsciiStringReference name, in AsciiStringReference pathRelativeToSolution, in SolutionProjectList solutionProjectList = default)
            : this(new SolutionProject(in guid, in name, in pathRelativeToSolution), in solutionProjectList)
        {
        }
        public SolutionProjectListAssociation(in Guid guid, in string name, in string pathRelativeToSolution, in SolutionProjectList solutionProjectList = default)
            : this(new SolutionProject(in guid, in name, in pathRelativeToSolution), in solutionProjectList)
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

        public void SetRecord(in SolutionProject record)
        {
            SolutionProject = record;
            SolutionProjectId = record.SolutionProjectId;
        }
    }
}
