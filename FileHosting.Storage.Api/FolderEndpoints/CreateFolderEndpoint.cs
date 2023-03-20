using System.Net.Mime;
using FileHosting.Shared.Api.FluentValidation;
using FileHosting.Storage.AppCore.Interfaces;
using MinimalApi.Endpoint;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class CreateFolderEndpoint : IEndpoint<IResult, CreateFolderRequest>
{
    private readonly IFolderService _folderService;

    public CreateFolderEndpoint(IFolderService folderService)
    {
        _folderService = folderService;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/folders", (Validated<CreateFolderRequest> request) =>
            {
                request.ThrowIfFailure();
                return HandleAsync(request.Value);
            })
            .Accepts<CreateFolderRequest>(MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status200OK)
            .WithTags(EndpointTags.FolderEndpoints);
    }

    public async Task<IResult> HandleAsync(CreateFolderRequest request)
    {
        await _folderService.CreateFolder(request.Name);
        return Results.Ok();
    }
}