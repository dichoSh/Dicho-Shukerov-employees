using Employees.Services.Dtos;
using Employees.Services.Dtos.Results;
using Employees.Services.Helpers;
using Employees.Services.Interfaces;

namespace Employees.Services
{
    public class EmployeeService(IEmployeesFileReader employeesReader) : IEmployeeService
    {
        public async Task<FindResult> FindLongestPeriodTeam(Stream? csvStream, CancellationToken ct)
        {
            var readResult = await employeesReader.ReadEmployeesGroupedByProject(csvStream, ct);

            if (!readResult.IsSuccess)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = readResult.Message,
                };
            }

            Dictionary<(int emp1, int emp2, int projectId), int> pairs = GetTeamProjectDaysTogether(readResult.ProjectEmployees!);
            Dictionary<(int emp1, int emp2), int> teamDeays = GetToatalDaysByTeam(pairs);

            var maxTeamDays = teamDeays.OrderByDescending(pair => pair.Value).First();

            var result = pairs.Where(x => x.Key.emp1 == maxTeamDays.Key.emp1
                                           && x.Key.emp2 == maxTeamDays.Key.emp2)
                              .Select(x => new TeamProjectDaysDto
                              {
                                  Employee1Id = x.Key.emp1,
                                  Employee2Id = x.Key.emp2,
                                  ProjectId = x.Key.projectId,
                                  DaysTogether = x.Value
                              })
                              .ToList();

            return new()
            {
                IsSuccess = true,
                TeamProjectDays = result
            };
        }

        private static Dictionary<(int emp1, int emp2, int projectId), int> GetTeamProjectDaysTogether(IDictionary<int, List<EmployeeDto>> projectEmployees)
        {
            List<EmployeeDto> employees;
            DateTime maxDateFrom, minDateTo;
            int days;
            Dictionary<(int emp1, int emp2, int projectId), int> pairs = [];
            (int emp1, int emp2, int projectId) pair;

            foreach (var projectEmployee in projectEmployees)
            {
                employees = projectEmployee.Value;

                for (int i = 0; i < employees.Count - 1; i++)
                {
                    for (int j = i + 1; j < employees.Count; j++)
                    {
                        if (employees[i].Id == employees[j].Id)
                            continue;

                        maxDateFrom = DateTimeHelper.GetMaxDate(employees[i].DateFrom, employees[j].DateFrom);
                        minDateTo = DateTimeHelper.GetMinDate(employees[i].DateTo, employees[j].DateTo);

                        if (maxDateFrom.CompareTo(minDateTo) <= 0)
                        {
                            days = (minDateTo - maxDateFrom).Days;

                            pair = employees[i].Id < employees[j].Id
                                ? (employees[i].Id, employees[j].Id, projectEmployee.Key)
                                : (employees[j].Id, employees[i].Id, projectEmployee.Key);

                            if (!pairs.TryAdd(pair, days))
                            {
                                pairs[pair] += days;
                            }
                        }
                    }
                }
            }
            return pairs;
        }

        private static Dictionary<(int emp1, int emp2), int> GetToatalDaysByTeam(Dictionary<(int emp1, int emp2, int projectId), int> pairs)
        {
            Dictionary<(int emp1, int emp2), int> result = [];

            foreach (var pair in pairs)
            {
                if (!result.TryAdd((pair.Key.emp1, pair.Key.emp2), pair.Value))
                {
                    result[(pair.Key.emp1, pair.Key.emp2)] += pair.Value;
                }
            }

            return result;
        }
    }
}
