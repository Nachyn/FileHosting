using FileHosting.Shared.Api.FluentValidation;
using FileHosting.Storage.Api.Consts;
using FileHosting.Storage.AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class AddFolderItemEndpoint : IEndpoint<IResult, AddFolderItemRequest, IFolderService>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/folders/{folderId}/folderItems",
                ([FromRoute] int folderId,
                    [FromForm] IFormFile file,
                    [FromServices] IFolderService folderService,
                    [FromServices] AddFolderItemRequestValidator validator) =>
                {
                    var request = new AddFolderItemRequest
                    {
                        FolderId = folderId,
                        File = file
                    };
                    validator.ValidateAndThrowIfInvalid(request);

                    return HandleAsync(request, folderService);
                })
            .Produces(StatusCodes.Status200OK)
            .WithTags(EndpointTags.FolderEndpoints)
            .RequireAuthorization(Policy.Authorized);
    }

    public async Task<IResult> HandleAsync(AddFolderItemRequest request, IFolderService folderService)
    {
        await using var fileStream = request.File.OpenReadStream();
        await folderService.AddFolderItem(request.FolderId, request.File.FileName, fileStream);
        return Results.Ok();
    }
}