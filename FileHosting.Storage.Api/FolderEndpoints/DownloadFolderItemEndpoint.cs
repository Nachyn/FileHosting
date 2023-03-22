using System.Net.Mime;
using FileHosting.Shared.Api.FluentValidation;
using FileHosting.Storage.AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class DownloadFolderItemEndpoint : IEndpoint<IResult, DownloadFolderItemRequest>
{
    private readonly IFolderService _folderService;

    public DownloadFolderItemEndpoint(IFolderService folderService)
    {
        _folderService = folderService;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/folders/{folderId}/folderItems/{folderItemId}",
                ([FromRoute] int folderId, [FromRoute] int folderItemId, [FromServices] DownloadFolderItemRequestValidator validator) =>
                {
                    var request = new DownloadFolderItemRequest
                    {
                        FolderId = folderId,
                        FolderItemId = folderItemId
                    };
                    validator.ValidateAndThrowIfInvalid(request);

                    return HandleAsync(request);
                })
            .Produces(StatusCodes.Status200OK)
            .WithTags(EndpointTags.FolderEndpoints);
    }

    public async Task<IResult> HandleAsync(DownloadFolderItemRequest request)
    {
        var file = await _folderService.GetFolderItemFile(request.FolderId, request.FolderItemId);
        return Results.File(file.Bytes, MediaTypeNames.Application.Octet, file.FileName);
    }
}