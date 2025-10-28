namespace Employees.Services.Dtos.Results
{
    public abstract class BaseResult
    {
        public required bool IsSuccess { get; set; }

        public string? Message { get; set; }
    }
}
