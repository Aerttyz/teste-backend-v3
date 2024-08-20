
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.DataTransferObjects;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata;



namespace TheatricalPlayersRefactoringKataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        [HttpPost]
        [Route("CreateInvoice")]
        public IActionResult CreateInvoice([FromBody] InvoiceDTO invoiceDTO)
        {
            if(invoiceDTO == null)
            {
                return BadRequest("Invoice cannot be null");
            }
            
            Invoice invoice = new Invoice(invoiceDTO.Customer, new List<Performance>());
            
            foreach(var performanceDTO in invoiceDTO.Performances)
            {
                Performance performance = new Performance(performanceDTO.PlayId, performanceDTO.Audience);
                invoice.Performances.Add(performance);

               
            }     
             return Ok(invoice);

        } 

    }
}