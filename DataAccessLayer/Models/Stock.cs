namespace EDIG_Api_Task.Entity
{
    public class Stock
    {
        public int Id { get; set; }
        public string StockName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
