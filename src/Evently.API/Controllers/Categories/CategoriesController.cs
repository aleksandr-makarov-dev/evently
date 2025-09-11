using Evently.API.Infrastructure;
using Evently.Application.Categories.ArchiveCategory;
using Evently.Application.Categories.CreateCategory;
using Evently.Application.Categories.GetCategories;
using Evently.Application.Categories.GetCategory;
using Evently.Application.Categories.UpdateCategory;
using Evently.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evently.API.Controllers.Categories;

[ApiController]
[Route("api/categories")]
public class CategoriesController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand(request.Name);

        Result<Guid> result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return Ok(new { id = result.Value });
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var query = new GetCategoriesQuery();

        Result<IReadOnlyList<CategoryResponse>> result = await sender.Send(query);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var query = new GetCategoryQuery(id);

        Result<CategoryResponse> result = await sender.Send(query);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand(id, request.Name);

        Result result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return NoContent();
    }

    [HttpPut("{id:guid}/archive")]
    public async Task<IActionResult> ArchiveCategory([FromRoute] Guid id)
    {
        var command = new ArchiveCategoryCommand(id);

        Result result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return NoContent();
    }
}
