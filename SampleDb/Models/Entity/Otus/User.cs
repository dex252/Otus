namespace SampleDb.Models.Entity.Otus
{
    public class User : BaseEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public List<Course> Courses { get; set; }
    }
}
