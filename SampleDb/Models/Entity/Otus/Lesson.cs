namespace SampleDb.Models.Entity.Otus
{
    public class Lesson: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid CourseId { get;set; }
        public Course Course { get; set; }
    }
}
