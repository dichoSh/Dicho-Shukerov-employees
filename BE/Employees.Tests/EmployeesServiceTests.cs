using Employees.Services;
using Employees.Services.Interfaces;

namespace Employees.Tests
{
    public class EmployeesServiceTests
    {
        private CsvEmployeesFileReader reader;
        private IEmployeeService employeeService;

        [SetUp]
        public void Setup()
        {
            reader = new CsvEmployeesFileReader();
            employeeService = new EmployeeService(reader);
        }

        [Test]
        public async Task FindLongestPeriodTeam_ai_teams_IsCorrect()
        {
            var stream = File.OpenRead(".\\CSVs\\ai_teams.csv");

            var result = await employeeService.FindLongestPeriodTeam(stream, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Message, Is.Null);
                Assert.That(result.TeamProjectDays, Is.Not.Null);
                Assert.That(result.TeamProjectDays!.Count, Is.EqualTo(2));
                Assert.That(result.TeamProjectDays![0].Employee1Id, Is.EqualTo(143));
                Assert.That(result.TeamProjectDays![0].Employee2Id, Is.EqualTo(218));
                Assert.That(result.TeamProjectDays![0].ProjectId, Is.EqualTo(10));
                Assert.That(result.TeamProjectDays![0].DaysTogether, Is.EqualTo(145));
                Assert.That(result.TeamProjectDays![1].Employee1Id, Is.EqualTo(143));
                Assert.That(result.TeamProjectDays![1].Employee2Id, Is.EqualTo(218));
                Assert.That(result.TeamProjectDays![1].ProjectId, Is.EqualTo(11));
                Assert.That(result.TeamProjectDays![1].DaysTogether, Is.EqualTo(11));
            });
        }

        [Test]
        public async Task FindLongestPeriodTeam_billing_teams_IsCorrect()
        {
            var stream = File.OpenRead(".\\CSVs\\billing_teams.csv");

            var result = await employeeService.FindLongestPeriodTeam(stream, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Message, Is.Null);
                Assert.That(result.TeamProjectDays, Is.Not.Null);
                Assert.That(result.TeamProjectDays!.Count, Is.EqualTo(1));
                Assert.That(result.TeamProjectDays![0].Employee1Id, Is.EqualTo(1));
                Assert.That(result.TeamProjectDays![0].Employee2Id, Is.EqualTo(5));
                Assert.That(result.TeamProjectDays![0].ProjectId, Is.EqualTo(13));
                Assert.That(result.TeamProjectDays![0].DaysTogether, Is.EqualTo(2461));
            });

        }

    }
}
