using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using UniversityManager.ExitEnums;
using UniversityManager.Structures;

namespace UniversityManager.Utils
{
    public static class MonitorEditor
    {
        public static MonitorExitEnum AddPass(DateTime dateTime, string fullName, string subjectName, User monitor)
        {
            List<User> users = TablesReader.ReadUsers();
            if (users is null)
            {
                return MonitorExitEnum.ERROR_USERS_IS_NULL;
            }

            User student = TablesReader.ReadUserInGroup(users, fullName);
            if (student is null)
            {
                return MonitorExitEnum.ERROR_USER_IS_NOT_STUDENT;
            }

            List<List<int>> usersGroups = TablesReader.ReadUsersGroups();
            if (usersGroups is null)
            {
                return MonitorExitEnum.ERROR_USERS_GROUPS_IS_NULL;
            }

            Dictionary<int, int> userGroupsMap = new Dictionary<int, int>();

            for (int i = 0; i < usersGroups.Count; i++)
            {
                userGroupsMap.Add(usersGroups[i][1], usersGroups[i][2]);
            }

            if (userGroupsMap[student.Id] != userGroupsMap[monitor.Id])
            {
                return MonitorExitEnum.ERROR_ACCESS_DENIED;
            }

            List<Subject> subjects = TablesReader.ReadSubjects();
            if (subjects is null)
            {
                return MonitorExitEnum.ERROR_SUBJECTS_IS_NULL;
            }

            Subject subject = TablesReader.ReadSubject(subjects, subjectName);
            if (subject is null)
            {
                return MonitorExitEnum.ERROR_SUBJECT_NOT_VALID;
            }

            List<SubjectForGroup> subjectForGroups = TablesReader.ReadSubjectsForGroup();
            if (subjectForGroups is null)
            {
                return MonitorExitEnum.ERROR_SUBJECTS_IS_NULL;
            }

            bool IsSubjectForGroup = false;
            SubjectForGroup subjectForGroup = new SubjectForGroup();
            foreach (SubjectForGroup subjectGroup in subjectForGroups)
            {
                if (subjectGroup.SubjectId == subject.Id &&
                    userGroupsMap[monitor.Id] == subjectGroup.GroupsId)
                {
                    IsSubjectForGroup = true;
                    subjectForGroup = subjectGroup;
                    break;
                }
            }

            if (!IsSubjectForGroup)
            {
                return MonitorExitEnum.ERROR_SUBJECT_NOT_FOR_THIS_GROUP;
            }

            List<LogOfPass> logOfPasses = TablesReader.ReadLogOfPasses();

            int usersGroupId = 0;
            for (int i = 0; i < usersGroups.Count; i++)
            {
                if (usersGroups[i][1] == student.Id)
                {
                    usersGroupId = usersGroups[i][0];
                }
            }

            if (logOfPasses is null)
            {
                return InsertPass(1, dateTime, subjectForGroup.Id, usersGroupId)
                    ? MonitorExitEnum.SUCCEEDED
                    : MonitorExitEnum.ERROR_SOMETHING_WENT_WRONG;
            }

            int lastIndex = logOfPasses.Count - 1;
            int newId = logOfPasses[lastIndex].Id + 1;

            return InsertPass(newId, dateTime, subjectForGroup.Id, usersGroupId)
                    ? MonitorExitEnum.SUCCEEDED
                    : MonitorExitEnum.ERROR_SOMETHING_WENT_WRONG;
        }

        public static string GetStudentInfo(string fullName, User monitor)
        {
            List<User> users = TablesReader.ReadUsers();
            if (users is null)
            {
                return MonitorExitEnum.ERROR_USERS_IS_NULL.GetDescription();
            }

            User student = TablesReader.ReadUserInGroup(users, fullName);
            if (student is null)
            {
                return MonitorExitEnum.ERROR_USER_IS_NOT_STUDENT.GetDescription();
            }

            List<List<int>> usersGroups = TablesReader.ReadUsersGroups();
            if (usersGroups is null)
            {
                return MonitorExitEnum.ERROR_USERS_GROUPS_IS_NULL.GetDescription();
            }

            Dictionary<int, int> userGroupsMap = new Dictionary<int, int>();

            for (int i = 0; i < usersGroups.Count; i++)
            {
                userGroupsMap.Add(usersGroups[i][1], usersGroups[i][2]);
            }

            if (userGroupsMap[student.Id] != userGroupsMap[monitor.Id])
            {
                return MonitorExitEnum.ERROR_ACCESS_DENIED.GetDescription();
            }

            return StudentEditor.GetStudentInfo(student);
        }

        public static MonitorExitEnum ReadLogOfPass(User monitor, DataGrid dataGrid, Label groupNumber, Label specialty,
            Label chair, Label educationDegree, Label educationForm)
        {
            List<LogOfPass> logOfPasses = TablesReader.ReadLogOfPasses();
            if (logOfPasses is null)
            {
                return MonitorExitEnum.ERROR_LOG_OF_PASSES_IS_NULL;
            }

            List<List<int>> usersGroups = TablesReader.ReadUsersGroups();
            if (usersGroups is null)
            {
                return MonitorExitEnum.ERROR_USERS_GROUPS_IS_NULL;
            }

            Dictionary<int, int> userGroupsMap = new Dictionary<int, int>();

            for (int i = 0; i < usersGroups.Count; i++)
            {
                userGroupsMap.Add(usersGroups[i][1], usersGroups[i][2]);
            }

            List<User> users = TablesReader.ReadUsers();

            Dictionary<int, StudentCountPass> studentCountPasses = new Dictionary<int, StudentCountPass>();

            for (int i = 0; i < usersGroups.Count; i++)
            {
                User tmp = TablesReader.ReadUserById(usersGroups[i][1]);
                if (usersGroups[i][2] == userGroupsMap[monitor.Id] &&
                    tmp.Type != 2 && tmp.Type != 4)
                {
                    string fullName = string.Empty;
                    foreach (User user in users)
                    {
                        if (user.Id == usersGroups[i][1])
                        {
                            fullName = user.FullName;
                            break;
                        }
                    }
                    studentCountPasses.Add(i, new StudentCountPass(fullName, 0));
                }
            }

            for (int i = 0; i < usersGroups.Count; i++)
            {
                foreach (LogOfPass logOfPass in logOfPasses)
                {
                    if (usersGroups[i][0] == logOfPass.UsersGroupsId &&
                        studentCountPasses.ContainsKey(usersGroups[i][0]))
                    {
                        studentCountPasses[i] = new StudentCountPass(studentCountPasses[i].FullName,
                            studentCountPasses[i].PassesCount + 1);
                    }
                }
            }
            dataGrid.ItemsSource = studentCountPasses.Values.ToList();

            List<Group> groups = TablesReader.ReadGroups();
            if (groups is null)
            {
                return MonitorExitEnum.ERROR_GROUPS_IS_NULL;
            }

            foreach (Group group in groups)
            {
                if (group.Id == userGroupsMap[monitor.Id])
                {
                    groupNumber.Content = "Група: " + group.Number;
                    specialty.Content = "Спеціальність: " + group.Specialty;
                    chair.Content = "Кафедра: " + group.Chair;
                    educationDegree.Content = "Ступінь: " + group.EducationDegree;
                    educationForm.Content = "Форма навчання: " + group.EducationForm;
                    break;
                }
            }
            return MonitorExitEnum.SUCCEEDED;
        }

        public static MonitorExitEnum AddAchievment(string fullName, string achieveName, User monitor)
        {
            List<User> users = TablesReader.ReadUsers();
            if (users is null)
            {
                return MonitorExitEnum.ERROR_USERS_IS_NULL;
            }

            User student = TablesReader.ReadUserInGroup(users, fullName);
            if (student is null)
            {
                return MonitorExitEnum.ERROR_USER_IS_NOT_STUDENT;
            }

            List<List<int>> usersGroups = TablesReader.ReadUsersGroups();
            if (usersGroups is null)
            {
                return MonitorExitEnum.ERROR_USERS_GROUPS_IS_NULL;
            }

            Dictionary<int, int> userGroupsMap = new Dictionary<int, int>();

            for (int i = 0; i < usersGroups.Count; i++)
            {
                userGroupsMap.Add(usersGroups[i][1], usersGroups[i][2]);
            }

            if (userGroupsMap[student.Id] != userGroupsMap[monitor.Id])
            {
                return MonitorExitEnum.ERROR_ACCESS_DENIED;
            }

            List<Achieve> achieveTypes = TablesReader.ReadAchievesTypes();
            if (achieveTypes is null)
            {
                return MonitorExitEnum.ERROR_ACHIEVETYPE_IS_NULL;
            }

            Achieve achieve = null;

            foreach (Achieve ach in achieveTypes)
            {
                if (ach.Name.Equals(achieveName))
                {
                    achieve = ach;
                }
            }

            int usersGroupId = 0;
            for (int i = 0; i < usersGroups.Count; i++)
            {
                if (usersGroups[i][1] == student.Id)
                {
                    usersGroupId = usersGroups[i][0];
                }
            }

            List<List<int>> achieves = TablesReader.ReadAchieves();
            if (achieves is null)
            {
                return InsertAchieve(1, usersGroupId, achieve.Id)
                    ? MonitorExitEnum.SUCCEEDED
                    : MonitorExitEnum.ERROR_SOMETHING_WENT_WRONG;
            }

            int lastIndex = achieves.Count - 1;
            int newId = achieves[lastIndex][0] + 1;

            return InsertAchieve(newId, usersGroupId, achieve.Id)
                    ? MonitorExitEnum.SUCCEEDED
                    : MonitorExitEnum.ERROR_SOMETHING_WENT_WRONG;
        }

        public static void ShowGroupList(DataGrid dataGrid, User monitor)
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
                if (usersGroups[i][2] == userGroupsMap[monitor.Id] &&
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

        private static bool InsertPass(int id, DateTime dateTime, string subjectForGroup, int usersGroupsId)
        {
            string date = dateTime.ToString("yyyy-MM-dd");
            DataBaseConnector connector = new DataBaseConnector();
            string query = "insert into mydb.logofpasses(`idLogOfVisits`, `Date`, `SubjectListForGroup_SubjectListForGroupcol`, `UsersGroups_idUsersGroups`) " +
                $"values({id}, '{date}', '{subjectForGroup}', {usersGroupsId});";
            if (connector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connector.Connection);
                command.ExecuteNonQuery();
                connector.CloseConnection();
                return true;
            }
            return false;
        }

        private static bool InsertAchieve(int id, int usersGroupsId, int achieveType)
        {
            DataBaseConnector connector = new DataBaseConnector();
            string query = "insert into mydb.sportssocialachievements(`idSportsSocialAchievements`, `UsersGroups_idUsersGroups`, `SpSocAchieveType_idSpSocAchieveType`) " +
                $"values({id}, {usersGroupsId}, {achieveType});";
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
