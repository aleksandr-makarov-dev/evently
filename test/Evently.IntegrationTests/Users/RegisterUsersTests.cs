using System.Net;
using System.Net.Http.Json;
using Evently.API.Controllers.Users;
using Evently.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.IntegrationTests.Users;

public class RegisterUserTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    public static readonly TheoryData<string, string, string, string> InvalidRequests = new()
    {
        { "", Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName() },
        { Faker.Internet.Email(), "", Faker.Name.FirstName(), Faker.Name.LastName() },
        { Faker.Internet.Email(), "12345", Faker.Name.FirstName(), Faker.Name.LastName() },
        { Faker.Internet.Email(), Faker.Internet.Password(), "", Faker.Name.LastName() },
        { Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), "" }
    };

    [Theory]
    [MemberData(nameof(InvalidRequests))]
    public async Task Should_ReturnFailure_WhenRequestIsInvalid(string email, string password, string firstName,
        string lastName)
    {
        // Arrange
        var request = new RegisterUserRequest(
            firstName, lastName, email, password);

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/users/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_ReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var request = new RegisterUserRequest(
            Faker.Name.FirstName(),
            Faker.Name.LastName(),
            "create@example.com",
            Faker.Internet.Password()
        );

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/users/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
