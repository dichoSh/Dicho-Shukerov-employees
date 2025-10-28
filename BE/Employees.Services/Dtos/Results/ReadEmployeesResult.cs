namespace Employees.Services.Dtos.Results
{
    public class ReadEmployeesResult : BaseResult
    {
        public IDictionary<int, List<EmployeeDto>>? ProjectEmployees { get; set; }
    }
}
