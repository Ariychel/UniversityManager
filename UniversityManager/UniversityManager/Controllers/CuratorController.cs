using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using UniversityManager.ExitEnums;
using UniversityManager.Structures;
using UniversityManager.Utils;

namespace UniversityManager.Controllers
{
    class CuratorController
    {
        public static CuratorExitEnum AddMark(string fullName, string subjectName, string mark, string markType, DateTime date, User curator)
        {
            return CuratorEditor.AddMark(fullName, subjectName, mark, markType, date, curator);
        }

        public static string GetStudentInfo(string fullName, User curator)
        {
            return CuratorEditor.GetStudentInfo(fullName, curator);
        }

        public static void ShowGroupList(DataGrid dataGrid, User curator)
        {
            CuratorEditor.ShowGroupList(dataGrid, curator);
        }
    }
}
