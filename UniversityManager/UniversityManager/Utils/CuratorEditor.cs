using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using UniversityManager.ExitEnums;
using UniversityManager.Structures;

namespace UniversityManager.Utils
{
    public static class CuratorEditor
    {
        public static CuratorExitEnum AddMark(string fullName, string subjectName, string mark, string markType, DateTime date, User curator)
        {
            List<User> users = TablesReader.ReadUsers();
            if (users is null)
            {
                return CuratorExitEnum.ERROR_USERS_IS_NULL;
            }

            User student = TablesReader.ReadUserInGroup(users, fullName);
            if (student is null)
            {
                return CuratorExitEnum.ERROR_USER_IS_NOT_STUDENT;
            }

            List<List<int>> usersGroups = TablesReader.ReadUsersGroups();
            if (usersGroups is null)
            {
                return CuratorExitEnum.ERROR_USERS_GROUPS_IS_NULL;
            }

            Dictionary<int, int> userGroupsMap = new Dictionary<int, int>();

            for (int i = 0; i < usersGroups.Count; i++)
            {
                userGroupsMap.Add(usersGroups[i][1], usersGroups[i][2]);
            }

            if (userGroupsMap[student.Id] != userGroupsMap[curator.Id])
            {
                return CuratorExitEnum.ERROR_ACCESS_DENIED;
            }

            List<Subject> subjects = TablesReader.ReadSubjects();
            if (subjects is null)
            {
                return CuratorExitEnum.ERROR_SUBJECTS_IS_NULL;
            }

            Subject subject = TablesReader.ReadSubject(subjects, subjectName);
            if (subject is null)
            {
                return CuratorExitEnum.ERROR_SUBJECT_NOT_VALID;
            }

            List<SubjectForGroup> subjectForGroups = TablesReader.ReadSubjectsForGroup();
            if (subjectForGroups is null)
            {
                return CuratorExitEnum.ERROR_SUBJECTS_IS_NULL;
            }

            bool IsSubjectForGroup = false;
            SubjectForGroup subjectForGroup = new SubjectForGroup();
            foreach (SubjectForGroup subjectGroup in subjectForGroups)
            {
                if (subjectGroup.SubjectId == subject.Id &&
                    userGroupsMap[curator.Id] == subjectGroup.GroupsId)
                {
                    IsSubjectForGroup = true;
                    subjectForGroup = subjectGroup;
                    break;
                }
            }

            int _markType = 0;

            switch (markType)
            {
                case "Залік":
                    _markType = 1;
                    break;
                case "Екзамен":
                    _markType = 2;
                    break;
            }

            if (!IsSubjectForGroup)
            {
                return CuratorExitEnum.ERROR_SUBJECT_NOT_FOR_THIS_GROUP;
            }

            List<MarkList> marks = TablesReader.ReadMarkList();

            int usersGroupId = 0;
            for (int i = 0; i < usersGroups.Count; i++)
            {
                if (usersGroups[i][1] == student.Id)
                {
                    usersGroupId = usersGroups[i][0];
                }
            }

            if (marks is null)
            {
                return InsertMark(1, subjectForGroup.Id, usersGroupId, Convert.ToInt32(mark), date, _markType)
                    ? CuratorExitEnum.SUCCEEDED
                    : CuratorExitEnum.ERROR_SOMETHING_WENT_WRONG;
            }

            int lastIndex = marks.Count - 1;
            int newId = marks[lastIndex].Id + 1;

            return InsertMark(newId, subjectForGroup.Id, usersGroupId, Convert.ToInt32(mark), date, _markType)
                    ? CuratorExitEnum.SUCCEEDED
                    : CuratorExitEnum.ERROR_SOMETHING_WENT_WRONG;

        }

        public static string GetStudentInfo(string fullName, User curator)
        {
            List<User> users = TablesReader.ReadUsers();
            if (users is null)
            {
                return CuratorExitEnum.ERROR_USERS_IS_NULL.GetDescription();
            }

            User student = TablesReader.ReadUserInGroup(users, fullName);
            if (student is null)
            {
                return CuratorExitEnum.ERROR_USER_IS_NOT_STUDENT.GetDescription();
            }

            List<List<int>> usersGroups = TablesReader.ReadUsersGroups();
            if (usersGroups is null)
            {
                return CuratorExitEnum.ERROR_USERS_GROUPS_IS_NULL.GetDescription();
            }

            Dictionary<int, int> userGroupsMap = new Dictionary<int, int>();

            for (int i = 0; i < usersGroups.Count; i++)
            {
                userGroupsMap.Add(usersGroups[i][1], usersGroups[i][2]);
            }

            if (userGroupsMap[student.Id] != userGroupsMap[curator.Id])
            {
                return CuratorExitEnum.ERROR_ACCESS_DENIED.GetDescription();
            }

            return StudentEditor.GetStudentInfo(student);
        }

        public static void ShowGroupList(DataGrid dataGrid, User curator)
        {
            List<List<int>> usersGroups = TablesReader.ReadUsersGroups();
            if (usersGroups is null)
            {
                return;
            }

            Dictionary<int, int> userGroupsMap = new Dictionary<int, int>();

            for (int i = 0; i < usersGroups.Count; i++)
            {
                userGroupsMap.Add(usersGroups[i][1], usersGroups[i][2]);
            }

            List<User> users = TablesReader.ReadUsers();

            UserForTable.StartNewIndexer();

            List<UserForTable> students = new List<UserForTable>();

            for (int i = 0; i < usersGroups.Count; i++)
            {
                User tmp = TablesReader.ReadUserById(usersGroups[i][1]);
                if (usersGroups[i][2] == userGroupsMap[curator.Id] &&
                    tmp.Type != 2 && tmp.Type != 4)
                {
                    foreach (User user in users)
                    {
                        if (user.Id == usersGroups[i][1])
                        {
                            students.Add(new UserForTable(user.FullName, user.EMail, user.PhoneNumber, user.Type));
                            break;
                        }
                    }
                }
            }

            dataGrid.ItemsSource = students;
        }

        private static bool InsertMark(int id, string subjectForGroup, int usersGroupsId, int mark, DateTime dateTime, int markType)
        {
            string date = dateTime.ToString("yyyy-MM-dd");
            DataBaseConnector connector = new DataBaseConnector();
            string query = "insert into mydb.marklist(`idMarkList`, `SubjectListForGroup_SubjectListForGroupcol`, `UsersGroups_idUsersGroups`, `Mark`, `Date`, `MarkType_idMarkType`) " +
                $"values({id}, '{subjectForGroup}', {usersGroupsId}, {mark}, '{date}', {markType});";
            if (connector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connector.Connection);
                command.ExecuteNonQuery();
                connector.CloseConnection();
                return true;
            }
            return false;
        }
    }
}
