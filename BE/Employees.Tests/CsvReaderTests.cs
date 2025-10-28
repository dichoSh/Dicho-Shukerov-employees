using Employees.Services;

namespace Employees.Tests
{
    public class CsvReaderTests
    {
        private CsvEmployeesFileReader reader;

        [SetUp]
        public void Setup()
        {
            reader = new CsvEmployeesFileReader();
        }

        [Test]
        public async Task ReadEmployeesGroupedByProject_ai_teams_IsCorrect()
        {
            var stream = File.OpenRead(".\\CSVs\\ai_teams.csv");

            var result = await reader.ReadEmployeesGroupedByProject(stream, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Message, Is.Null);
                Assert.That(result.ProjectEmployees, Is.Not.Null);
                Assert.That(result.ProjectEmployees!.Count, Is.EqualTo(3));
                Assert.That(result.ProjectEmployees![12].Count, Is.EqualTo(2));
                Assert.That(result.ProjectEmployees![10].Count, Is.EqualTo(3));
                Assert.That(result.ProjectEmployees![11].Count, Is.EqualTo(2));
            });
        }

        [Test]
        public async Task ReadEmployeesGroupedByProject_billing_teams_IsCorrect()
        {
            var stream = File.OpenRead(".\\CSVs\\billing_teams.csv");

            var result = await reader.ReadEmployeesGroupedByProject(stream, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Message, Is.Null);
                Assert.That(result.ProjectEmployees, Is.Not.Null);
                Assert.That(result.ProjectEmployees!.Count, Is.EqualTo(5));
                Assert.That(result.ProjectEmployees![10].Count, Is.EqualTo(2));
                Assert.That(result.ProjectEmployees![11].Count, Is.EqualTo(2));
                Assert.That(result.ProjectEmployees![12].Count, Is.EqualTo(2));
                Assert.That(result.ProjectEmployees![13].Count, Is.EqualTo(2));
                Assert.That(result.ProjectEmployees![14].Count, Is.EqualTo(1));
            });

        }

    }
}
