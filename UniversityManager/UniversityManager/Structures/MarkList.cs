using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManager.Structures
{
    public class MarkList
    {
        public int Id { get; }
        public string SubjectListId { get; }
        public int UsersGroupsId { get; }
        public int Mark { get; }
        public DateTime Date { get; }
        public int MarkType { get; }

        public MarkList(int id, string subjectListId, int usersGroupsId,
            int mark, DateTime date, int markType)
        {
            Id = id;
            SubjectListId = subjectListId;
            UsersGroupsId = usersGroupsId;
            Mark = mark;
            Date = date;
            MarkType = markType;
        }
    }
}
