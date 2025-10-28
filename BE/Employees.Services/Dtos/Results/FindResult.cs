namespace Employees.Services.Dtos.Results
{
    public class FindResult : BaseResult
    {
        public List<TeamProjectDaysDto>? TeamProjectDays { get; set; }
    }
}
