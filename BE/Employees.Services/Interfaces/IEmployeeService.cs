using Employees.Services.Dtos.Results;

namespace Employees.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<FindResult> FindLongestPeriodTeam(Stream? csvStream, CancellationToken ct);
    }
}
