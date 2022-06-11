using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UniversityManager.Controllers;
using UniversityManager.ExitEnums;
using UniversityManager.Structures;

namespace UniversityManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _user;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            AuthController controller = new AuthController();
            string userName = Username.Text;
            string password = Password.Password;

            _user = controller.AuthInSystem(userName, password);
            if (_user is null)
            {
                MessageBox.Show("Невірно введені дані.");
            }
            else
            {
                AuthMenu.Visibility = Visibility.Hidden;
                switch (_user.Type)
                {
                    case 1:
                        UserNameM.Content += " " + _user.FullName;
                        MonitorMenu.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        UserNameC.Content += " " + _user.FullName;
                        CuratorMenu.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        UserNameS.Content += " " + _user.FullName;
                        StudentMenu.Visibility = Visibility.Visible;
                        break;
                    case 4:
                        UserNameD.Content += " " + _user.FullName;
                        DeanEmployeeMenu.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

            switch (_user.Type)
            {
                case 1:
                    MonitorMenu.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    CuratorMenu.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    StudentMenu.Visibility = Visibility.Hidden;
                    break;
                case 4:
                    DeanEmployeeMenu.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
            AuthMenu.Visibility = Visibility.Visible;            
        }

        private void AddStudentInGroup_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullName.Text;
            string groupNumber = GroupNumber.Text;
            DEExitEnum exitEnum = new DeanController().AddStudentToGroup(groupNumber, fullName);
            ExitMessage.Content = exitEnum.GetDescription();
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            string subjectName = SubjectName.Text;
            string groupNumber = GroupNumber1.Text;
            string semesters = Semestrs.Text;
            string examType = Convert.ToString(ExamType.SelectedItem);
            string hours = Hours.Text;
            DEExitEnum exitEnum = new DeanController().AddSubjectForGroup(subjectName, groupNumber,
                semesters, hours, examType);
            ExitMessageSubject.Content = exitEnum.GetDescription();
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameR.Text;
            string userName = UsernameR.Text;
            string password = PasswordR.Text;
            string eMail = EmailR.Text;
            string phoneNumber = PhoneNumberR.Text;
            string userType = Convert.ToString(UserTypeR.SelectedItem);
            DEExitEnum exitEnum = new DeanController().CreateUser(fullName, userName,
                password, eMail, phoneNumber, userType);
            RegExitMessage.Content = exitEnum.GetDescription();
        }

        private void CreateGroup_Click(object sender, RoutedEventArgs e)
        {
            string groupNumber = GroupNumberCreateGroup.Text;
            string educationForm = EducationFormCreateGroup.Text;
            string educationDegree = EducationDegreeCreateGroup.Text;
            string specialty = Specialty.Text;
            DEExitEnum exitEnum = new DeanController().CreateGroup(groupNumber,
                educationForm, educationDegree, specialty);
            CreateGroupExitMessage.Content = exitEnum.GetDescription();
        }

        private void NumbersInTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddPass_Click(object sender, RoutedEventArgs e)
        {
            if (!(PassDate.SelectedDate is null))
            {
                DateTime dateTime = (DateTime)PassDate.SelectedDate;
                string fullName = FullNameM.Text;
                string subjectName = SubjectNameM.Text;
                MonitorExitEnum monitorExitEnum = MonitorController.AddPass(dateTime, fullName, subjectName, _user);
                ExitMessageM.Content = monitorExitEnum.GetDescription();
            }
            else
            {
                ExitMessageM.Content = MonitorExitEnum.ERROR_DATE_NOT_CHOOSE.GetDescription();
            }
        }

        private void ReadLogOfPassses_Click(object sender, RoutedEventArgs e)
        {
            MonitorExitEnum monitorExitEnum = MonitorController.ReadLogOfPass(_user, SimpleTable, GroupNumberPas, SpecialtyPas,
                Chair, EducationDegree, EducationForm);
            ExitMessageMPas.Content = monitorExitEnum.GetDescription();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MonitorExitEnum monitorExitEnum = MonitorController.AddAchievement(FullNameAchieve.Text, TypeOfAchieve.Text, _user);
            ExitAchieve.Content = monitorExitEnum.GetDescription();
        }

        private void AddMark_Click(object sender, RoutedEventArgs e)
        {
            string fullName = AddMarkFullName.Text;
            string subjectName = AddMarkSubjectName.Text;
            string mark = AddMarkCount.Text;
            DateTime date = (DateTime)AddMarkDate.SelectedDate;
            string markType = (string)AddMarkType.SelectedItem;
            CuratorExitEnum curatorExitEnum = CuratorController.AddMark(fullName, subjectName, mark, markType, date, _user);
            AddMarkExitMessage.Content = curatorExitEnum.GetDescription();
        }

        private void StudentMenu_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            StudentInfo.Content = StudentController.GetStudentInfo(_user);
            StudentController.ShowSubjectList(SubjectList, _user);
        }

        private void GetStudentInfoDean_Click(object sender, RoutedEventArgs e)
        {
            StudentInfoDean.Content = new DeanController().GetStudentInfo(InfoStudentBoxDean.Text);
        }

        private void GetStudentInfoMonitor_Click(object sender, RoutedEventArgs e)
        {
            StudentInfoMonitor.Content = MonitorController.GetStudentInfo(InfoStudentBoxMonitor.Text, _user);
        }

        private void GetStudentInfoCurator_Click(object sender, RoutedEventArgs e)
        {
            StudentInfoCurator.Content = CuratorController.GetStudentInfo(InfoStudentBoxCurator.Text, _user);
        }

        private void MonitorMenu_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MonitorController.ShowGroupList(GroupListMonitor, _user);
        }

        private void CuratorMenu_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CuratorController.ShowGroupList(GroupListCurator, _user);
        }

        private void ShowGroupList_Click(object sender, RoutedEventArgs e)
        {
            DEExitEnum exitEnum = DeanController.ShowGroupList(GroupListDean, GroupNumberDean.Text);
            GroupListExitEnum.Content = exitEnum.GetDescription();
        }
    }
}
