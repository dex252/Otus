namespace SampleDb.Models.Entity.Otus
{
    public class Course: BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Lesson> Lessons { get; set; }

        public List<User> Users { get; set; }
    }
}
