using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using UniversityManager.Structures;
using UniversityManager.Utils;

namespace UniversityManager.Controllers
{
    public static class StudentController
    {
        public static string GetStudentInfo(User user)
        {
            return StudentEditor.GetStudentInfo(user);
        }

        public static void ShowSubjectList(DataGrid dataGrid, User user)
        {
            StudentEditor.ShowSubjectList(dataGrid, user);
        }
    }
}
