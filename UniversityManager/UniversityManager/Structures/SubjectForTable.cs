using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManager.Structures
{
    public class SubjectForTable
    {
        public string Id { get; }
        public string SubjectName { get; }
        public int Semester { get; }
        public string ExamType { get; }
        public int Hours { get; }

        public SubjectForTable(string id, string subjectName, int semester,
            string examType, int hours)
        {
            Id = id;
            SubjectName = subjectName;
            Semester = semester;
            ExamType = examType;
            Hours = hours;
        }
    }
}
