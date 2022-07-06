using WebApiStone.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApiStone.Services;
using WebApiStone.Models;

namespace WebApiStone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService) =>
        _reportService = reportService;


    /// <summary>
    /// Busca a arvore genealógica de um usuário através do ID
    /// </summary>
    [HttpGet("{personId:length(24)}")]
    public async Task<ActionResult<FamilyTree>> GetTree(string personId, int level = 1)
    {
        return await _reportService.GetFamilyTree(personId, level);
    }

    /// <summary>
    /// Busca os dados de estatística
    /// </summary>
    [HttpGet("")]
    public async Task<ActionResult<ResultStatistics>> GetStatistics(string? name, string? skincolor, string? education, string? sex)
    {
        return await _reportService.GetStatistics(name, skincolor, education, sex);
    }
}