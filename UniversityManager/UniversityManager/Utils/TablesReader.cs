using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UniversityManager.Structures;

namespace UniversityManager.Utils
{
    public static class TablesReader
    {
        public static List<User> ReadUsers()
        {
            string query = "SELECT * FROM mydb.users";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                List<User> users = new List<User>();
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                List<int> usersId = new List<int>();
                List<string> fullNames = new List<string>();
                List<string> userNames = new List<string>();
                List<string> eMails = new List<string>();
                List<string> phoneNumbers = new List<string>();
                List<int> usersType = new List<int>();

                while (dataReader.Read())
                {
                    usersId.Add(Convert.ToInt32(dataReader["idUsersTable"]));
                    fullNames.Add(Convert.ToString(dataReader["Full Name"]));
                    userNames.Add(Convert.ToString(dataReader["Username"]));
                    eMails.Add(Convert.ToString(dataReader["E-mail"]));
                    phoneNumbers.Add(Convert.ToString(dataReader["PhoneNumber"]));
                    usersType.Add(Convert.ToInt32(dataReader["UserType_idUserType"]));
                }
                dataReader.Close();

                for (int i = 0; i < usersId.Count; i++)
                {
                    users.Add(new User(usersId[i], fullNames[i], userNames[i], "",
                       eMails[i], phoneNumbers[i], usersType[i]));
                }
                dataBaseConnector.CloseConnection();
                return users.Count == 0 ? null : users;
            }
            return null;

        }

        public static User ReadUserById(int id)
        {
            string query = $"SELECT * FROM mydb.users where idUsersTable = {id}";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);
                MySqlDataReader dataReader = command.ExecuteReader();
                User user = null;
                while (dataReader.Read())
                {
                    user = new User(
                        Convert.ToInt32(dataReader["idUsersTable"]),
                        Convert.ToString(dataReader["Full Name"]),
                        Convert.ToString(dataReader["Username"]),
                        "",
                        Convert.ToString(dataReader["E-mail"]),
                        Convert.ToString(dataReader["PhoneNumber"]),
                        Convert.ToInt32(dataReader["UserType_idUserType"]));
                }
                dataReader.Close();

                dataBaseConnector.CloseConnection();
                return user;
            }
            return null;
        }

        public static List<MarkList> ReadMarkList()
        {
            string query = "SELECT * FROM mydb.marklist";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                List<int> ids = new List<int>();
                List<string> subjectsListIds = new List<string>();
                List<int> usersGroupsIds = new List<int>();
                List<int> marks = new List<int>();
                List<DateTime> dates = new List<DateTime>();
                List<int> markTypes = new List<int>(); ;

                while (dataReader.Read())
                {
                    ids.Add(Convert.ToInt32(dataReader["idMarkList"]));
                    subjectsListIds.Add(Convert.ToString(dataReader["SubjectListForGroup_SubjectListForGroupcol"]));
                    usersGroupsIds.Add(Convert.ToInt32(dataReader["UsersGroups_idUsersGroups"]));
                    marks.Add(Convert.ToInt32(dataReader["Mark"]));
                    dates.Add(dataReader.GetDateTime("Date"));
                    markTypes.Add(Convert.ToInt32(dataReader["MarkType_idMarkType"]));
                }
                dataReader.Close();

                List<MarkList> markLists = new List<MarkList>();
                for (int i = 0; i < ids.Count; i++)
                {
                    markLists.Add(new MarkList(ids[i], subjectsListIds[i], usersGroupsIds[i],
                        marks[i], dates[i], markTypes[i]));
                }
                dataBaseConnector.CloseConnection();
                return markLists.Count == 0 ? null : markLists;
            }
            return null;
        }

        public static List<Group> ReadGroups()
        {
            string query = "SELECT * FROM mydb.groups";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                List<Group> groups = new List<Group>();
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                List<int> groupIds = new List<int>();
                List<string> groupNumbers = new List<string>();
                List<string> specialties = new List<string>();
                List<string> chairs = new List<string>();
                List<string> educationForms = new List<string>();
                List<string> educationDegrees = new List<string>();

                while (dataReader.Read())
                {
                    groupIds.Add(Convert.ToInt32(dataReader["idGroups"]));
                    groupNumbers.Add(Convert.ToString(dataReader["GroupNumber"]));
                    specialties.Add(Convert.ToString(dataReader["Specialty"]));
                    chairs.Add(Convert.ToString(dataReader["Chair"]));
                    educationForms.Add(Convert.ToString(dataReader["EducationForm"]));
                    educationDegrees.Add(Convert.ToString(dataReader["EducationDegree"]));
                }
                dataReader.Close();

                for (int i = 0; i < groupIds.Count; i++)
                {
                    groups.Add(new Group(groupIds[i], groupNumbers[i], specialties[i],
                       chairs[i], educationForms[i], educationDegrees[i]));
                }
                dataBaseConnector.CloseConnection();
                return groups.Count == 0 ? null : groups;
            }
            return null;
        }

        public static List<Subject> ReadSubjects()
        {
            string query = "SELECT * FROM mydb.subjects";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                List<Subject> subjects = new List<Subject>();
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                List<int> subjectIds = new List<int>();
                List<string> subjectNames = new List<string>();

                while (dataReader.Read())
                {
                    subjectIds.Add(Convert.ToInt32(dataReader["idSubjects"]));
                    subjectNames.Add(Convert.ToString(dataReader["Name"]));
                }
                dataReader.Close();

                for (int i = 0; i < subjectIds.Count; i++)
                {
                    subjects.Add(new Subject(subjectIds[i], subjectNames[i]));
                }
                dataBaseConnector.CloseConnection();
                return subjects.Count == 0 ? null : subjects;
            }
            return null;
        }

        public static Subject ReadSubject(List<Subject> subjects, string subjectName)
        {
            foreach (Subject subject in subjects)
            {
                if (subject.Name.Equals(subjectName))
                {
                    return subject;
                }
            }
            return null;
        }

        public static Group ReadGroup(List<Group> groups, string groupNumber)
        {
            foreach (Group group in groups)
            {
                if (group.Number.Equals(groupNumber))
                {
                    return group;
                }
            }
            return null;
        }

        public static Group ReadGroupById(List<Group> groups, int id)
        {
            foreach (Group group in groups)
            {
                if (group.Id == id)
                {
                    return group;
                }
            }
            return null;
        }

        public static User ReadUserInGroup(List<User> users, string fullName)
        {
            foreach (User user in users)
            {
                if ((user.FullName.Equals(fullName) && user.Type == 3) ||
                    (user.FullName.Equals(fullName) && user.Type == 1) ||
                    (user.FullName.Equals(fullName) && user.Type == 2))
                {
                    return user;
                }
            }
            return null;
        }

        public static List<List<int>> ReadUsersGroups()
        {
            string query = "SELECT * FROM mydb.usersgroups";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                List<int> userGroupsId = new List<int>();
                List<int> userId = new List<int>();
                List<int> groupId = new List<int>();

                while (dataReader.Read())
                {
                    userGroupsId.Add(Convert.ToInt32(dataReader["idUsersGroups"]));
                    userId.Add(Convert.ToInt32(dataReader["Users_idUsersTable"]));
                    groupId.Add(Convert.ToInt32(dataReader["Groups_idGroups"]));
                }
                dataReader.Close();

                List<List<int>> usersGroups = new List<List<int>>();
                for (int i = 0; i < userGroupsId.Count; i++)
                {
                    usersGroups.Add(new List<int>{
                        userGroupsId[i],
                        userId[i],
                        groupId[i]
                    });
                }
                dataBaseConnector.CloseConnection();
                return usersGroups.Count == 0 ? null : usersGroups;
            }
            return null;
        }

        public static List<SubjectForGroup> ReadSubjectsForGroup()
        {
            string query = "SELECT * FROM mydb.subjectlistforgroup";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                List<string> subjectsListIds = new List<string>();
                List<int> groupsId = new List<int>();
                List<int> subjectsId = new List<int>();
                List<int> semesters = new List<int>();
                List<int> examType = new List<int>();
                List<int> hours = new List<int>();

                while (dataReader.Read())
                {
                    subjectsListIds.Add(Convert.ToString(dataReader["SubjectListForGroupcol"]));
                    groupsId.Add(Convert.ToInt32(dataReader["Groups_idGroups"]));
                    subjectsId.Add(Convert.ToInt32(dataReader["Subjects_idSubjects"]));
                    semesters.Add(Convert.ToInt32(dataReader["Semester"]));
                    examType.Add(Convert.ToInt32(dataReader["ExamType"]));
                    hours.Add(Convert.ToInt32(dataReader["Hours"]));
                }
                dataReader.Close();

                List<SubjectForGroup> subjectForGroups = new List<SubjectForGroup>();
                for (int i = 0; i < subjectsListIds.Count; i++)
                {
                    subjectForGroups.Add(new SubjectForGroup(subjectsListIds[i], groupsId[i], subjectsId[i],
                        semesters[i], examType[i], hours[i]));
                }
                dataBaseConnector.CloseConnection();
                return subjectForGroups.Count == 0 ? null : subjectForGroups;
            }
            return null;
        }

        public static List<LogOfPass> ReadLogOfPasses()
        {
            string query = "SELECT * FROM mydb.logofpasses";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                List<int> ids = new List<int>();
                List<DateTime> dates = new List<DateTime>();
                List<string> subjectIds = new List<string>();
                List<int> usersGroupsIds = new List<int>();

                while (dataReader.Read())
                {
                    ids.Add(Convert.ToInt32(dataReader["idLogOfVisits"]));
                    dates.Add(dataReader.GetDateTime("Date"));
                    subjectIds.Add(Convert.ToString(dataReader["SubjectListForGroup_SubjectListForGroupcol"]));
                    usersGroupsIds.Add(Convert.ToInt32(dataReader["UsersGroups_idUsersGroups"]));
                }
                dataReader.Close();

                List<LogOfPass> logOfPasses = new List<LogOfPass>();
                for (int i = 0; i < ids.Count; i++)
                {
                    logOfPasses.Add(new LogOfPass(ids[i], dates[i], subjectIds[i], usersGroupsIds[i]));
                }
                dataBaseConnector.CloseConnection();
                return logOfPasses.Count == 0 ? null : logOfPasses;
            }
            return null;
        }

        public static List<Achieve> ReadAchievesTypes()
        {
            string query = "SELECT * FROM mydb.spsocachievetype";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                List<int> ids = new List<int>();
                List<string> names = new List<string>();
                List<decimal> marks = new List<decimal>();

                while (dataReader.Read())
                {
                    ids.Add(Convert.ToInt32(dataReader["idSpSocAchieveType"]));
                    names.Add(Convert.ToString(dataReader["AchieveName"]));
                    marks.Add(Convert.ToDecimal(dataReader["AdditionalMark"]));
                }
                dataReader.Close();

                List<Achieve> achieves = new List<Achieve>();
                for (int i = 0; i < ids.Count; i++)
                {
                    achieves.Add(new Achieve(ids[i], names[i], marks[i]));
                }
                dataBaseConnector.CloseConnection();
                return achieves.Count == 0 ? null : achieves;
            }
            return null;
        }

        public static List<List<int>> ReadAchieves()
        {
            string query = "SELECT * FROM mydb.sportssocialachievements";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                List<int> ids = new List<int>();
                List<int> userGroupsId = new List<int>();
                List<int> achieveTypes = new List<int>();

                while (dataReader.Read())
                {
                    ids.Add(Convert.ToInt32(dataReader["idSportsSocialAchievements"]));
                    userGroupsId.Add(Convert.ToInt32(dataReader["UsersGroups_idUsersGroups"]));
                    achieveTypes.Add(Convert.ToInt32(dataReader["SpSocAchieveType_idSpSocAchieveType"]));
                }
                dataReader.Close();

                List<List<int>> achieves = new List<List<int>>();
                for (int i = 0; i < userGroupsId.Count; i++)
                {
                    achieves.Add(new List<int>{
                        ids[i],
                        userGroupsId[i],
                        achieveTypes[i]
                    });
                }
                dataBaseConnector.CloseConnection();
                return achieves.Count == 0 ? null : achieves;
            }
            return null;
        }

        public static List<SubjectForTable> ReadSubjectForStudent(int groupId)
        {
            string query = $"SELECT * FROM mydb.subjectlistforgroup WHERE `Groups_idGroups` = {groupId}";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();

            List<string> subjectsListIds = new List<string>();
            List<int> groupsId = new List<int>();
            List<int> subjectsId = new List<int>();
            List<int> semesters = new List<int>();
            List<int> examType = new List<int>();
            List<int> hours = new List<int>();
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    subjectsListIds.Add(Convert.ToString(dataReader["SubjectListForGroupcol"]));
                    groupsId.Add(Convert.ToInt32(dataReader["Groups_idGroups"]));
                    subjectsId.Add(Convert.ToInt32(dataReader["Subjects_idSubjects"]));
                    semesters.Add(Convert.ToInt32(dataReader["Semester"]));
                    examType.Add(Convert.ToInt32(dataReader["ExamType"]));
                    hours.Add(Convert.ToInt32(dataReader["Hours"]));
                }
                dataReader.Close();

                List<SubjectForGroup> subjectForGroups = new List<SubjectForGroup>();
                for (int i = 0; i < subjectsListIds.Count; i++)
                {
                    subjectForGroups.Add(new SubjectForGroup(subjectsListIds[i], groupsId[i], subjectsId[i],
                        semesters[i], examType[i], hours[i]));
                }
                dataBaseConnector.CloseConnection();
            }

            List<string> subjectsNames = new List<string>();

            for (int i = 0; i < subjectsId.Count; i++)
            {
                subjectsNames.Add(ReadSubjectById(subjectsId[i]).Name);
            }

            List<string> examTypes = new List<string>();
            for (int i = 0; i < examType.Count; i++)
            {
                switch (examType[i])
                {
                    case 0:
                        examTypes.Add("Залік");
                        break;
                    case 1:
                        examTypes.Add("Екзамен");
                        break;
                }
            }

            List<SubjectForTable> subjectForTables = new List<SubjectForTable>();

            for (int i = 0; i < subjectsListIds.Count; i++)
            {
                subjectForTables.Add(new SubjectForTable(subjectsListIds[i], subjectsNames[i],
                    semesters[i], examTypes[i], hours[i]));
            }
            return subjectForTables;
        }

        public static Subject ReadSubjectById(int subjectId)
        {
            string query = $"SELECT * FROM mydb.subjects WHERE idSubjects = {subjectId}";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                Subject subject = null;

                while (dataReader.Read())
                {
                    subject = new Subject(Convert.ToInt32(dataReader["idSubjects"]),
                        Convert.ToString(dataReader["Name"]));
                }
                dataReader.Close();

                return subject;
            }
            return null;
        }
    }
}
