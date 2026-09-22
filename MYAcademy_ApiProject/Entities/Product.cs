namespace MYAcademy_ApiProject.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public decimal ProductPrice { get; set; }
        public bool IsCritic { get; set; }
    }
}
