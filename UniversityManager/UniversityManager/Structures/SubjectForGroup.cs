using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManager.Structures
{
    public class SubjectForGroup
    {
        public string Id { get; }
        public int GroupsId { get; }
        public int SubjectId { get; }
        public int Semesters { get; }
        public int ExamType { get; }
        public int Hours { get; }

        public SubjectForGroup() { }

        public SubjectForGroup(string id, int groupId, int subjectId,
            int semester, int examType, int hours)
        {
            Id = id;
            GroupsId = groupId;
            SubjectId = subjectId;
            Semesters = semester;
            ExamType = examType;
            Hours = hours;
        }
    }
}
