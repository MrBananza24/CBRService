namespace CBRService.Models
{
    public class Valute
    {
        public string Id { get; set; } = null!;
        public string NumCode { get; set; } = null!;
        public string CharCode { get; set; } = null!;
        public int Nominal { get; set; }
        public string Name { get; set; } = null!;
        public decimal Value { get; set; }
        public decimal Previous { get; set; }
    }
}
