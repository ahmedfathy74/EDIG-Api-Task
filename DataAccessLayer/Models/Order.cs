namespace EDIG_Api_Task.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string PersonName { get; set; } = null!;
        public decimal Price { get; set; }
        public int StockID { get; set; }
        public Stock Stock { get; set; } = null!;

    }
}
