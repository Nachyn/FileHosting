using FileHosting.Shared.Api.FluentValidation;
using FileHosting.Storage.AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class AddFolderItemEndpoint : IEndpoint<IResult, AddFolderItemRequest>
{
    private readonly IFolderService _folderService;

    public AddFolderItemEndpoint(IFolderService folderService)
    {
        _folderService = folderService;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/folders/{folderId}/folderItems",
                ([FromRoute] int folderId, [FromForm] IFormFile file, [FromServices] AddFolderItemRequestValidator validator) =>
                {
                    var request = new AddFolderItemRequest
                    {
                        FolderId = folderId,
                        File = file
                    };
                    validator.ValidateAndThrowIfInvalid(request);

                    return HandleAsync(request);
                })
            .Produces(StatusCodes.Status200OK)
            .WithTags(EndpointTags.FolderEndpoints);
    }

    public async Task<IResult> HandleAsync(AddFolderItemRequest request)
    {
        await using var fileStream = request.File.OpenReadStream();
        await _folderService.AddFolderItem(request.FolderId, request.File.FileName, fileStream);
        return Results.Ok();
    }
}