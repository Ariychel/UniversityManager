using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using UniversityManager.ExitEnums;
using UniversityManager.Structures;
using UniversityManager.Utils;

namespace UniversityManager.Controllers
{
    public static class MonitorController
    {
        public static MonitorExitEnum AddPass(DateTime dateTime, string fullName, string subjectName, User monitor)
        {
            return MonitorEditor.AddPass(dateTime, fullName, subjectName, monitor);
        }

        public static MonitorExitEnum ReadLogOfPass(User monitor, DataGrid dataGrid, Label groupNumber, Label specialty,
            Label chair, Label educationDegree, Label educationForm)
        {
            return MonitorEditor.ReadLogOfPass(monitor, dataGrid, groupNumber, specialty,
                chair, educationDegree, educationForm);
        }

        public static MonitorExitEnum AddAchievement(string fullName, string achieveType, User monitor)
        {
            return MonitorEditor.AddAchievment(fullName, achieveType, monitor);
        }

        public static string GetStudentInfo(string fullName, User monitor)
        {
            return MonitorEditor.GetStudentInfo(fullName, monitor);
        }

        public static void ShowGroupList(DataGrid dataGrid, User monitor)
        {
            MonitorEditor.ShowGroupList(dataGrid, monitor);
        }
    }
}
