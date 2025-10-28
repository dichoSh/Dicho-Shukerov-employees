namespace Employees.Services.Dtos
{
    public record ProjectDaysDto
    {
        public int ProjectId { get; set; }

        public int Days { get; set; }
    }
}
