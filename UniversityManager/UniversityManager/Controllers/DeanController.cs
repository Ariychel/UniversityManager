using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using UniversityManager.ExitEnums;
using UniversityManager.Utils;

namespace UniversityManager.Controllers
{
    public class DeanController
    {
        public DeanController() { }

        public DEExitEnum AddStudentToGroup(string groupNumber, string fullName)
        {
            return DeanEmployeeEditor.AddUserToGroup(groupNumber, fullName);
        }

        public DEExitEnum AddSubjectForGroup(string subjectName, string groupNumber,
            string semesters, string hours, string examType)
        {
            return DeanEmployeeEditor.AddSubjectToGroup(subjectName, groupNumber, semesters,
                hours, examType);
        }

        public DEExitEnum CreateUser(string fullName, string username, string password,
            string email, string phoneNumber, string userType)
        {
            return DeanEmployeeEditor.CreateUser(fullName, username, password, email,
                phoneNumber, userType);
        }

        public DEExitEnum CreateGroup(string groupNumber, string educationForm, string educationDegree, string specialty)
        {
            return DeanEmployeeEditor.CreateGroup(groupNumber, educationForm, educationDegree, specialty);
        }

        public string GetStudentInfo(string fullName)
        {
            return DeanEmployeeEditor.GetStudentInfo(fullName);
        }

        public static DEExitEnum ShowGroupList(DataGrid dataGrid, string groupNumber)
        {
            return DeanEmployeeEditor.ShowGroupList(dataGrid, groupNumber);
        }
    }
}
