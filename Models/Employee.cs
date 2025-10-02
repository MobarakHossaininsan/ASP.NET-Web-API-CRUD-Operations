namespace Employee.Models
{
    public class Employee
    {
        public int Id { get; set; } 
        public required string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
    }
}