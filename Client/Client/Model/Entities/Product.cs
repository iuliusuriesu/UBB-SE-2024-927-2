namespace Client.Model.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }

        public Product(int productID, string productName, string productDescription, decimal price, string productType, int quantity)
        {
            ProductID = productID;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            ProductType = productType;
            Quantity = quantity;
        }
    }
}
