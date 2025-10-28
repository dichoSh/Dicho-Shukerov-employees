using Employees.Services.Dtos.Results;

namespace Employees.Services.Interfaces
{
    public interface IEmployeesFileReader
    {
        Task<ReadEmployeesResult> ReadEmployeesGroupedByProject(Stream? csvStream, CancellationToken ct);
    }
}
