using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using UniversityManager.ExitEnums;
using UniversityManager.Structures;

namespace UniversityManager.Utils
{
    public static class DeanEmployeeEditor
    {
        public static DEExitEnum AddUserToGroup(string groupNumber, string fullName)
        {
            List<User> users = TablesReader.ReadUsers();
            if (users is null)
            {
                return DEExitEnum.ERROR_USERS_IS_NULL;
            }

            User user = TablesReader.ReadUserInGroup(users, fullName);
            if (user is null)
            {
                return DEExitEnum.ERROR_USER_IS_NOT_STUDENT;
            }

            List<Group> groups = TablesReader.ReadGroups();
            if (groups is null)
            {
                return DEExitEnum.ERROR_GROUPS_IS_NULL;
            }

            Group group = TablesReader.ReadGroup(groups, groupNumber);
            if (group is null)
            {
                return DEExitEnum.ERROR_TABLE_NOT_INCLUDE_GROUP;
            }

            List<List<int>> usersGroupsTable = ReadUsersGroupTable();
            if (usersGroupsTable is null)
            {
                return InsertGroupInDB(1, group.Id, user.Id) ? DEExitEnum.SUCCEEDED : DEExitEnum.ERROR_SOMETHING_WENT_WRONG;
            }

            foreach (List<int> info in usersGroupsTable)
            {
                if (info.Contains(user.Id))
                {
                    return DEExitEnum.ERROR_STUDENT_ALREADY_IN_GROUP;
                }
            }

            if (user.Type == 2)
            {
                foreach (List<int> info in usersGroupsTable)
                {
                    if (info[2] == group.Id)
                    {
                        User tmp = TablesReader.ReadUserById(info[1]);
                        if (tmp.Type == 2)
                        {
                            return DEExitEnum.ERROR_CURATOR_ALREADY_ADDED;
                        }
                    }
                }
            }

            int lastIndex = usersGroupsTable.Count - 1;
            int newId = usersGroupsTable[lastIndex][0] + 1;

            return InsertGroupInDB(newId, group.Id, user.Id) ? DEExitEnum.SUCCEEDED : DEExitEnum.ERROR_SOMETHING_WENT_WRONG;
        }

        public static DEExitEnum ShowGroupList(DataGrid dataGrid, string groupNumber)
        {
            List<Group> groups = TablesReader.ReadGroups();
            if (groups is null)
            {
                return DEExitEnum.ERROR_GROUPS_IS_NULL;
            }

            Group group = TablesReader.ReadGroup(groups, groupNumber);
            if (group is null)
            {
                return DEExitEnum.ERROR_TABLE_NOT_INCLUDE_GROUP;
            }


            List<List<int>> usersGroups = TablesReader.ReadUsersGroups();
            if (usersGroups is null)
            {
                return DEExitEnum.ERROR_USERS_GROUPS_IS_NULL;
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
                if (usersGroups[i][2] == group.Id &&
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
            return DEExitEnum.SUCCEEDED;
        }

        public static DEExitEnum CreateGroup(string groupNumber, string educationForm, string educationDegree, string specialty)
        {
            List<Group> groups = TablesReader.ReadGroups();
            string chair = string.Empty;
            switch (specialty)
            {
                case "Комп. інженерія":
                    chair = "КСМ";
                    break;
                case "Комп. науки":
                    chair = "Комп'ютерні науки";
                    break;
                case "Інженерія програмного забезпечення":
                    chair = "Програмне забезпечення комп’ютерних систем";
                    break;
                default:
                    break;
            }

            if (groups is null)
            {
                return CreateGroupInDatabase(1, groupNumber, specialty, chair, educationForm, educationDegree) ?
                    DEExitEnum.SUCCEEDED : DEExitEnum.ERROR_SOMETHING_WENT_WRONG;
            }

            Group group = TablesReader.ReadGroup(groups, groupNumber);
            if (!(group is null))
            {
                return DEExitEnum.ERROR_THIS_GROUPS_ALREADY_ADDED;
            }

            int newId = groups.Last().Id + 1;
            return CreateGroupInDatabase(newId, groupNumber, specialty, chair, educationForm, educationDegree) ?
                    DEExitEnum.SUCCEEDED : DEExitEnum.ERROR_SOMETHING_WENT_WRONG;
        }

        public static DEExitEnum AddSubjectToGroup(string subjectName, string groupNumber,
            string semesters, string hours, string examType)
        {
            List<Group> groups = TablesReader.ReadGroups();
            if (groups is null)
            {
                return DEExitEnum.ERROR_GROUPS_IS_NULL;
            }

            List<Subject> subjects = TablesReader.ReadSubjects();
            if (subjects is null)
            {
                return DEExitEnum.ERROR_SUBJECTS_IS_NULL;
            }

            Group group = TablesReader.ReadGroup(groups, groupNumber);
            if (group is null)
            {
                return DEExitEnum.ERROR_TABLE_NOT_INCLUDE_GROUP;
            }

            Subject subject = TablesReader.ReadSubject(subjects, subjectName);
            if (subject is null)
            {
                return DEExitEnum.ERROR_TABLE_NOT_INCLUDE_SUBJECT;
            }

            int _examType = 0;

            switch (examType)
            {
                case "Залік":
                    _examType = 0;
                    break;
                case "Екзамен":
                    _examType = 1;
                    break;
            }
            int _semesters = Convert.ToInt32(semesters);
            int _hours = Convert.ToInt32(hours);
            List<List<int>> groupsSubject = ReadSubjectGroupTable();
            if (groupsSubject is null)
            {
                return InsertSubjectInDB(1, group.Id, subject.Id, _semesters, _examType, _hours)
                    ? DEExitEnum.SUCCEEDED
                    : DEExitEnum.ERROR_SOMETHING_WENT_WRONG;
            }

            foreach (List<int> info in groupsSubject)
            {
                if (info[1] == group.Id && info[2] == subject.Id)
                {
                    return DEExitEnum.ERROR_SUBJECT_GROUP_ALREADY_ADDED;
                }
            }

            int lastIndex = groupsSubject.Count - 1;
            int newId = groupsSubject[lastIndex][0] + 1;

            return InsertSubjectInDB(newId, group.Id, subject.Id, _semesters, _examType, _hours)
                ? DEExitEnum.SUCCEEDED
                : DEExitEnum.ERROR_SOMETHING_WENT_WRONG;
        }

        public static DEExitEnum CreateUser(string fullName, string username, string password,
            string email, string phoneNumber, string userType)
        {
            List<User> users = TablesReader.ReadUsers();
            if (users is null)
            {
                return InsertNewUser(1, fullName, username, password, email, phoneNumber, userType)
                    ? DEExitEnum.SUCCEEDED
                    : DEExitEnum.ERROR_SOMETHING_WENT_WRONG;
            }

            foreach (User user in users)
            {
                if (user.FullName.Equals(fullName) ||
                    user.UserName.Equals(username))
                {
                    return DEExitEnum.ERROR_USER_ALREADY_CREATED;
                }
            }

            int lastIndex = users.Count - 1;
            int newId = users[lastIndex].Id + 1;

            return InsertNewUser(newId, fullName, username, password, email, phoneNumber, userType)
                    ? DEExitEnum.SUCCEEDED
                    : DEExitEnum.ERROR_SOMETHING_WENT_WRONG;
        }

        private static List<List<int>> ReadUsersGroupTable()
        {
            string query = "SELECT * FROM mydb.usersgroups";
            DataBaseConnector connector = new DataBaseConnector();
            if (connector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connector.Connection);
                MySqlDataReader dataReader = command.ExecuteReader();

                List<List<int>> table = new List<List<int>>();

                while (dataReader.Read())
                {
                    table.Add(new List<int>
                    {
                        Convert.ToInt32(dataReader["idUsersGroups"]),
                        Convert.ToInt32(dataReader["Users_idUsersTable"]),
                        Convert.ToInt32(dataReader["Groups_idGroups"])
                    });
                }
                dataReader.Close();
                connector.CloseConnection();

                return (table.Count == 0) ? null : table;
            }
            return null;
        }

        private static List<List<int>> ReadSubjectGroupTable()
        {
            string query = "SELECT * FROM mydb.subjectlistforgroup";
            DataBaseConnector connector = new DataBaseConnector();
            if (connector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connector.Connection);
                MySqlDataReader dataReader = command.ExecuteReader();

                List<List<int>> table = new List<List<int>>();

                while (dataReader.Read())
                {
                    table.Add(new List<int>
                    {
                        Convert.ToInt32(dataReader["SubjectListForGroupcol"]),
                        Convert.ToInt32(dataReader["Groups_idGroups"]),
                        Convert.ToInt32(dataReader["Subjects_idSubjects"])
                    });
                }
                dataReader.Close();
                connector.CloseConnection();

                return (table.Count == 0) ? null : table;
            }
            return null;
        }

        private static bool InsertGroupInDB(int id, int groupId, int studentId)
        {
            DataBaseConnector connector = new DataBaseConnector();
            string query = "insert into mydb.usersgroups(`idUsersGroups`, `Users_idUsersTable`, `Groups_idGroups`) " +
                $"values({id}, {studentId}, {groupId});";
            if (connector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connector.Connection);
                command.ExecuteNonQuery();
                connector.CloseConnection();
                return true;
            }
            return false;
        }

        private static bool CreateGroupInDatabase(int id, string groupNumber, string specialty,
            string chair, string educationForm, string educationDegree)
        {
            DataBaseConnector connector = new DataBaseConnector();
            string query = "insert into mydb.groups(idGroups, GroupNumber, Specialty, Chair, EducationForm, EducationDegree) " +
                $"values({id}, '{groupNumber}', '{specialty}', '{chair}', '{educationForm}', '{educationDegree}');";
            if (connector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connector.Connection);
                command.ExecuteNonQuery();
                connector.CloseConnection();
                return true;
            }
            return false;
        }

        private static bool InsertSubjectInDB(int id, int groupId, int subjectId,
            int semesters, int examType, int hours)
        {
            DataBaseConnector connector = new DataBaseConnector();
            string query = "insert into mydb.subjectlistforgroup(`SubjectListForGroupcol`, `Groups_idGroups`, `Subjects_idSubjects`, `Semester`, `ExamType`, `Hours`) " +
                $"values({id}, {groupId}, {subjectId}, {semesters}, {examType}, {hours});";
            if (connector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connector.Connection);
                command.ExecuteNonQuery();
                connector.CloseConnection();
                return true;
            }
            return false;
        }

        private static bool InsertNewUser(int id, string fullName, string username, string password,
            string email, string phoneNumber, string userType)
        {
            int _userType = 1;
            switch (userType)
            {
                case "Староста":
                    _userType = 1;
                    break;
                case "Куратор":
                    _userType = 2;
                    break;
                case "Студент":
                    _userType = 3;
                    break;
                case "Працівник деканату":
                    _userType = 4;
                    break;
            }

            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            byte[] tmp = Encoding.ASCII.GetBytes(password);
            byte[] encryptedPass = Protector.Protect(tmp);
            string query = "insert into mydb.users(idUsersTable, `Full Name`, Username, Password, `E-mail`, PhoneNumber, `UserType_idUserType`) " +
                $"values({id},'{fullName}','{username}',@byteData,'{email}','{phoneNumber}',{_userType});";
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand mySqlCommand = new MySqlCommand(query, dataBaseConnector.Connection);
                mySqlCommand.Parameters.Add(new MySqlParameter("@byteData", encryptedPass));
                mySqlCommand.ExecuteNonQuery();
                dataBaseConnector.CloseConnection();
                return true;
            }
            return false;
        }

        public static string GetStudentInfo(string fullName)
        {
            List<User> users = TablesReader.ReadUsers();
            if (users is null)
            {
                return "Users table is empty.";
            }

            User user = TablesReader.ReadUserInGroup(users, fullName);
            if (user is null)
            {
                return "User is not a student.";
            }

            return StudentEditor.GetStudentInfo(user);
        }
    }
}
