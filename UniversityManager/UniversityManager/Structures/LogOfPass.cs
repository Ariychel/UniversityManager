using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManager.Structures
{
    public class LogOfPass
    {
        public int Id { get; }
        public DateTime Date { get; }
        public string SubjectId { get; }
        public int UsersGroupsId { get; }

        public LogOfPass(int id, DateTime date, string subjectId,
            int usersGroupsId)
        {
            Id = id;
            Date = date;
            SubjectId = subjectId;
            UsersGroupsId = usersGroupsId;
        }
    }
}
