namespace PROG3A_POE.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public DateTime ProductDate { get; set; }
        public int Quantity { get; set; }
        

        public Product(int productID, string productName, string productType, DateTime productDate, int quantity)
        {
            ProductID = productID;
            ProductName = productName;
            ProductType = productType;
            ProductDate = productDate;
            Quantity = quantity;
           
        }

        public Product(string productName, string productType, DateTime productDate, int quantity)
        {
            ProductName = productName;
            ProductType = productType;
            ProductDate = productDate;
            Quantity = quantity;
        }

        public Product()
        {
                
        }
    }
}
