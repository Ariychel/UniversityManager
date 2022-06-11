using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManager.Structures
{
    public class Achieve
    {
        public int Id { get; }
        public string Name { get; }
        public decimal AdditionalMark { get; }

        public Achieve(int id, string name, decimal additionalMark)
        {
            Id = id;
            Name = name;
            AdditionalMark = additionalMark;
        }
    }
}
