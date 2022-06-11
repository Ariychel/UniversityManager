using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManager.Structures
{
    public class Subject
    {
        public int Id { get; }
        public string Name { get; }

        public Subject(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
