namespace Employees.Services.Dtos
{
    public record TeamProjectDaysDto
    {
        public int Employee1Id { get; set; }

        public int Employee2Id { get; set; }

        public int ProjectId { get; set; }

        public int DaysTogether { get; set; }
    }
}
