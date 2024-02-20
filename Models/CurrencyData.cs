namespace CBRService.Models
{
    public class CurrencyData
    {
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public string PreviousURL { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public Dictionary<string, Valute> Valute { get; set; } = null!;
    }
}
