using Employees.Services.Dtos;
using Employees.Services.Dtos.Results;
using Employees.Services.Interfaces;

namespace Employees.Services
{
    public class CsvEmployeesFileReader : IEmployeesFileReader
    {
        private const string EMP_ID_COLUMN = "EmpID";
        private const string PROJECT_ID_COLUMN = "ProjectID";
        private const string DATE_FROM_COLUMN = "DateFrom";
        private const string DATE_TO_COLUMN = "DateTo";
        private const char SCV_DELIMITER = ',';
        private const int INITIAL_POSITION = -1;

        private readonly Dictionary<string, int> columns = new()
            {
                {EMP_ID_COLUMN, INITIAL_POSITION},
                {PROJECT_ID_COLUMN, INITIAL_POSITION },
                {DATE_FROM_COLUMN, INITIAL_POSITION },
                {DATE_TO_COLUMN, INITIAL_POSITION }
            };

        public async Task<ReadEmployeesResult> ReadEmployeesGroupedByProject(Stream? csvStream, CancellationToken ct)
        {
            if (csvStream == null)
                return new()
                {
                    IsSuccess = false,
                    Message = "Please upload a file"
                };

            using var reader = new StreamReader(csvStream);

            var columnsResult = await SetColumns(reader, ct);

            if (!columnsResult.IsSuccess)
                return columnsResult;

            return await ReadRows(reader, ct);
        }

        private async Task<ReadEmployeesResult> ReadRows(StreamReader reader, CancellationToken ct)
        {
            Dictionary<int, List<EmployeeDto>> projectEmployees = [];
            string? row;
            List<string>? rowValues;
            EmployeeDto? projectEmployee;
            int line = 1;
            int projectId = 0;

            while (!reader.EndOfStream)
            {
                row = await reader.ReadLineAsync(ct);
                line++;

                if (row == null)
                    break;

                rowValues = [.. row.Split(SCV_DELIMITER).Select(x => x.Trim().Trim('"'))];

                if (!int.TryParse(rowValues[columns[PROJECT_ID_COLUMN]], out projectId))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = $"{PROJECT_ID_COLUMN} must be an integer on line {line}"
                    };
                }

                try
                {
                    projectEmployee = CreateEmployeeDto(rowValues);
                    projectEmployees.TryAdd(projectId, []);
                    projectEmployees[projectId].Add(projectEmployee);
                }
                catch (Exception ex)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = $"{ex.Message} on line {line}"
                    };
                }
            }

            return new()
            {
                IsSuccess = true,
                ProjectEmployees = projectEmployees
            };
        }

        private EmployeeDto CreateEmployeeDto(List<string> rowValues)
        {
            if (!int.TryParse(rowValues[columns[EMP_ID_COLUMN]], out int id))
                throw new InvalidDataException($"{EMP_ID_COLUMN} must be an integer");

            if (!DateTime.TryParse(rowValues[columns[DATE_FROM_COLUMN]], out DateTime dateFrom))
                throw new InvalidDataException($"Invalid {DATE_FROM_COLUMN}");

            var toDate = rowValues[columns[DATE_TO_COLUMN]];
            DateTime dateTo;
            if (string.IsNullOrWhiteSpace(toDate)
                || toDate.ToLowerInvariant().Equals("null", StringComparison.InvariantCultureIgnoreCase))
            {
                dateTo = DateTime.Today;
            }
            else if (!DateTime.TryParse(toDate, out dateTo))
                throw new InvalidDataException($"Invalid {DATE_TO_COLUMN}");

            if (dateFrom.CompareTo(dateTo) > 0)
                throw new InvalidDataException("Invalid date range");

            return new()
            {
                Id = id,
                DateFrom = dateFrom,
                DateTo = dateTo,
            };
        }

        private async Task<ReadEmployeesResult> SetColumns(StreamReader reader, CancellationToken ct)
        {
            var firstLine = await reader.ReadLineAsync(ct);

            if (string.IsNullOrWhiteSpace(firstLine))
                return new()
                {
                    IsSuccess = false,
                    Message = "File is empty. First line must be with columns"
                };

            var lineColumns = firstLine.Split(SCV_DELIMITER)
                                       .Select(x => x.Trim().Trim('"'))
                                       .ToList();
            string lineColumn;
            for (int i = 0; i < lineColumns.Count; i++)
            {
                lineColumn = lineColumns[i];

                if (columns.ContainsKey(lineColumn))
                    columns[lineColumn] = i;
            }

            foreach (var column in columns)
            {
                if (column.Value == INITIAL_POSITION)
                    return new()
                    {
                        IsSuccess = false,
                        Message = $"Missing column: {column.Key}"
                    };
            }

            return new() { IsSuccess = true };
        }
    }
}
