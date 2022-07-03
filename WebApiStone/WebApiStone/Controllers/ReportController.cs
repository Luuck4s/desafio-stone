using WebApiStone.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApiStone.Services;

namespace WebApiStone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService) =>
        _reportService = reportService;


    [HttpGet("{personId:length(24)}")]
    public async Task<ActionResult<FamilyTree>> GetTree(string personId, int level = 1)
    {
        return await _reportService.GetFamilyTree(personId, level);
    }
}