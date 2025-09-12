using Evently.Application.Categories.CreateCategory;
using Evently.Application.Categories.GetCategories;
using Evently.Application.Categories.GetCategory;
using Evently.Domain.Abstractions;
using Evently.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.IntegrationTests.Categories;

public class GetCategoryTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Should_ReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var command = new CreateCategoryCommand(Faker.Music.Genre());
        Result<Guid> categoryId = await Sender.Send(command);

        var query = new GetCategoryQuery(categoryId.Value);

        // Act
        Result<CategoryResponse> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Name.Should().Be(command.Name);
    }
}
