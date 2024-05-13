namespace ServerAPI.Domain
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
    }
}
