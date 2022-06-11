using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManager.Structures
{
    public class UserForTable
    {
        private static int _id = 1;
        private readonly int _userType;
        public int Id { get; }
        public string FullName { get; }
        public string EMail { get; }
        public string PhoneNumber { get; }
        public string UserType
        {
            get
            {
                string userType = string.Empty;
                switch (_userType)
                {
                    case 1:
                        userType = "Староста";
                        break;
                    case 2:
                        userType = "Куратор";
                        break;
                    case 3:
                        userType = "Студент";
                        break;
                    case 4:
                        userType = "Працівник деканату";
                        break;
                }
                return userType;
            }
        }

        public UserForTable(string fullName, string eMail, string phoneNumber, int userType)
        {
            Id = _id;
            _id++;
            FullName = fullName;
            EMail = eMail;
            PhoneNumber = phoneNumber;
            _userType = userType;
        }

        public static void StartNewIndexer()
        {
            _id = 1;
        }

    }
}
