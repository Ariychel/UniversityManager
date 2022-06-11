namespace UniversityManager.Structures
{
    public class User
    {
        public int Id { get; }
        public string FullName { get; }
        public string UserName { get; }
        public string Password { get; }
        public string EMail { get; }
        public string PhoneNumber { get; }
        public int Type { get; }

        public User(int userId, string fullName, string userName,
            string password, string eMail, string phoneNumber, int userType)
        {
            Id = userId;
            FullName = fullName;
            UserName = userName;
            Password = password;
            EMail = eMail;
            PhoneNumber = phoneNumber;
            Type = userType;
        }
    }
}
