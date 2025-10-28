namespace Employees.Services.Dtos
{
    public record EmployeeDto
    {
        public int Id { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public override string ToString()
        {
            return $"{Id}, {DateFrom}, {DateTo}";
        }
    }
}
