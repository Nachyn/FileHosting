using FileHosting.Storage.Api.Consts;
using FileHosting.Storage.AppCore.Interfaces;
using FileHosting.Storage.AppCore.Services.Folders;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class GetAllFoldersEndpoint : IEndpoint<IResult, IFolderService>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/folders", ([FromServices] IFolderService folderService) => HandleAsync(folderService))
            .Produces<IReadOnlyCollection<FolderVm>>()
            .WithTags(EndpointTags.FolderEndpoints)
            .RequireAuthorization(Policy.Authorized);
    }

    public async Task<IResult> HandleAsync(IFolderService folderService)
    {
        return Results.Ok(await folderService.GetAllFolders());
    }
}