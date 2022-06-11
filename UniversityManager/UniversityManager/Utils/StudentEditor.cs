using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using UniversityManager.ExitEnums;
using UniversityManager.Structures;

namespace UniversityManager.Utils
{
    public static class StudentEditor
    {
        public static string GetStudentInfo(User user)
        {
            List<Group> groups = TablesReader.ReadGroups();
            if (groups is null)
            {
                return StudentExitEnum.ERROR_GROUPS_IS_NULL.GetDescription();
            }

            List<List<int>> usersGroups = TablesReader.ReadUsersGroups();
            if (usersGroups is null)
            {
                return StudentExitEnum.ERROR_USERS_GROUPS_IS_NULL.GetDescription();
            }

            Dictionary<int, int> userGroupsMap = new Dictionary<int, int>();

            for (int i = 0; i < usersGroups.Count; i++)
            {
                userGroupsMap.Add(usersGroups[i][1], usersGroups[i][2]);
            }

            string groupNumber = string.Empty;
            string specialty = string.Empty;
            string educationForm = string.Empty;
            string educationDegree = string.Empty;
            for (int i = 0; i < usersGroups.Count; i++)
            {
                if (usersGroups[i][2] == userGroupsMap[user.Id])
                {
                    foreach (Group group in groups)
                    {
                        if (group.Id == usersGroups[i][2])
                        {
                            groupNumber = group.Number;
                            specialty = group.Specialty;
                            educationForm = group.EducationForm;
                            educationDegree = group.EducationDegree;
                            break;
                        }
                    }
                }
            }

            int userGroupId = 0;
            for (int i = 0; i < usersGroups.Count; i++)
            {
                if (usersGroups[i][1] == user.Id)
                {
                    userGroupId = usersGroups[i][0];
                    break;
                }
            }

            int countOfPasses = 0;
            List<LogOfPass> logOfPasses = TablesReader.ReadLogOfPasses();
            if (!(logOfPasses is null))
            {
                foreach (LogOfPass logOfPass in logOfPasses)
                {
                    if (logOfPass.UsersGroupsId == userGroupId)
                    {
                        countOfPasses++;
                    }
                }
            }

            List<MarkList> markLists = TablesReader.ReadMarkList();
            double averageMark = 0;
            int marksCount = 0;
            foreach (MarkList markList in markLists)
            {
                if (markList.UsersGroupsId == userGroupId)
                {
                    averageMark += markList.Mark;
                    marksCount++;
                }
            }
            averageMark /= (double)marksCount;

            string logOfStudent = $"ПІБ: {user.FullName} \n" +
                $"Пошта: {user.EMail}\n" +
                $"Номер телефону: {user.PhoneNumber}\n" +
                $"Група: {groupNumber}\n" +
                $"Спеціальність: {specialty}\n" +
                $"Форма навчання: {educationForm}\n" +
                $"Ступінь: {educationDegree}\n" +
                $"Серденій бал: {Convert.ToString(averageMark)}\n" +
                $"Кількість пропусків: {Convert.ToString(countOfPasses)}\n";
            return logOfStudent;
        }

        public static void ShowSubjectList(DataGrid dataGrid, User user)
        {
            List<Group> groups = TablesReader.ReadGroups();
            if (groups is null)
            {
                return;
            }

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

            int userGroupId = 0;
            for (int i = 0; i < usersGroups.Count; i++)
            {
                if (usersGroups[i][1] == user.Id)
                {
                    userGroupId = usersGroups[i][2];
                    break;
                }
            }

            List<SubjectForTable> subjectForTables = TablesReader.ReadSubjectForStudent(userGroupId);
            dataGrid.ItemsSource = subjectForTables;
        }
    }
}
