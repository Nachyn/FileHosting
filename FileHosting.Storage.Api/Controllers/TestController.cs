using Ardalis.Specification;
using FileHosting.Shared.AppCore.Interfaces;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using Microsoft.AspNetCore.Mvc;

namespace FileHosting.Storage.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IReadRepository<Folder> _fRepository;

    public TestController(IReadRepository<Folder> fRepository)
    {
        _fRepository = fRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        return Ok(await _fRepository.FirstOrDefaultAsync(new ByIdSpec(id)));
    }

    private class ByIdSpec : Specification<Folder>
    {
        public ByIdSpec(int id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}