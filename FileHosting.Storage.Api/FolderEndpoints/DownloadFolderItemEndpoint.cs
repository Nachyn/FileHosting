using System.Net.Mime;
using FileHosting.Shared.Api.FluentValidation;
using FileHosting.Storage.Api.Consts;
using FileHosting.Storage.AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class DownloadFolderItemEndpoint : IEndpoint<IResult, DownloadFolderItemRequest, IFolderService>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/folders/{folderId}/folderItems/{folderItemId}",
                ([FromRoute] int folderId,
                    [FromRoute] int folderItemId,
                    [FromServices] DownloadFolderItemRequestValidator validator,
                    [FromServices] IFolderService folderService) =>
                {
                    var request = new DownloadFolderItemRequest
                    {
                        FolderId = folderId,
                        FolderItemId = folderItemId
                    };
                    validator.ValidateAndThrowIfInvalid(request);

                    return HandleAsync(request, folderService);
                })
            .Produces(StatusCodes.Status200OK)
            .WithTags(EndpointTags.FolderEndpoints)
            .RequireAuthorization(Policy.Authorized);
    }

    public async Task<IResult> HandleAsync(DownloadFolderItemRequest request, IFolderService folderService)
    {
        var file = await folderService.GetFolderItemFile(request.FolderId, request.FolderItemId);
        return Results.File(file.Bytes, MediaTypeNames.Application.Octet, file.FileName);
    }
}