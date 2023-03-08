namespace modelToDb.Models.Domain
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public long Salary { get; set; }
        public string? Department { get; set; }
        public DateTime DateOfBirth { get;set; }
    }
}
