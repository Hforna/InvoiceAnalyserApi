using Microsoft.AspNetCore.Mvc;

namespace InvoiceAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadInvoice([FromForm]IFormFile invoice)
    {
        
    }
}