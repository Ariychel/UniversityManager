using UniversityManager.Structures;
using UniversityManager.Utils;

namespace UniversityManager.Controllers
{
    class AuthController
    {
        public AuthController() { }

        public User AuthInSystem(string userName, string password)
        {
            return Authenticator.CheckAuthData(userName, password);
        }

    }
}
