namespace Everestfcsweb.Models
{
    public class ShiftExpenseData
    {
        public int RecordsTotal { get; set; }
        public IEnumerable<ShiftExpenses>? DataTableData { get; set; }
    }
}
