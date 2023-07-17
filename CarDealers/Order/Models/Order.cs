namespace CarDealers.Repository.Order.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
        public int StatusId { get; set; }
    }
}
