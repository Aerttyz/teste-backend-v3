using System.Collections.Generic;


namespace TheatricalPlayersRefactoringKata.DataTransferObjects
{
    public class InvoiceDTO
    {
        public string Customer { get; set; }
        public List<PerformanceDTO> Performances { get; set; }
    }
}