using FileHosting.Storage.AppCore.Interfaces;
using FileHosting.Storage.AppCore.Services.Folders;
using MinimalApi.Endpoint;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class GetAllFoldersEndpoint : IEndpoint<IResult>
{
    private readonly IFolderService _folderService;

    public GetAllFoldersEndpoint(IFolderService folderService)
    {
        _folderService = folderService;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/folders", HandleAsync)
            .Produces<IReadOnlyCollection<FolderVm>>()
            .WithTags("FolderEndpoints");
    }

    public async Task<IResult> HandleAsync()
    {
        return Results.Ok(await _folderService.GetAllFolders());
    }
}