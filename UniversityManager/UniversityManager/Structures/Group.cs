namespace UniversityManager.Structures
{
    public class Group
    {
        public int Id { get; }
        public string Number { get; }
        public string Specialty { get; }
        public string Chair { get; }
        public string EducationForm { get; }
        public string EducationDegree { get; }

        public Group(int groupId, string groupNumber, string specialty,
            string chair, string educationForm, string educationDegree)
        {
            Id = groupId;
            Number = groupNumber;
            Specialty = specialty;
            Chair = chair;
            EducationForm = educationForm;
            EducationDegree = educationDegree;
        }
    }
}
