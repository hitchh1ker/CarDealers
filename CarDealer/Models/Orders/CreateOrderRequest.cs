namespace CarDealer.Models.Orders
{
    public class CreateOrderRequest
    {
        public int CarId { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public decimal Cost { get; set; }
    }
}
