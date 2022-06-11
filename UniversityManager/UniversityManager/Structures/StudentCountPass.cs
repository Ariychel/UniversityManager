using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManager.Structures
{
    struct StudentCountPass
    {
        public string FullName { get; set; }
        public int PassesCount { get; set; }

        public StudentCountPass(string fullName, int passesCount)
        {
            FullName = fullName;
            PassesCount = passesCount;
        }
    }
}
