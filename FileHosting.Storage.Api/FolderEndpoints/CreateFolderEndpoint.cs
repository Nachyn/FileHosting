using System.Net.Mime;
using FileHosting.Shared.Api.FluentValidation;
using FileHosting.Storage.Api.Consts;
using FileHosting.Storage.AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoint;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class CreateFolderEndpoint : IEndpoint<IResult, CreateFolderRequest, IFolderService>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/folders", (Validated<CreateFolderRequest> request, [FromServices] IFolderService folderService) =>
            {
                request.ThrowIfFailure();
                return HandleAsync(request.Value, folderService);
            })
            .Accepts<CreateFolderRequest>(MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status200OK)
            .WithTags(EndpointTags.FolderEndpoints)
            .RequireAuthorization(Policy.Authorized);
    }

    public async Task<IResult> HandleAsync(CreateFolderRequest request, IFolderService folderService)
    {
        await folderService.CreateFolder(request.Name);
        return Results.Ok();
    }
}