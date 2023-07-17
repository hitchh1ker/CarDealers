namespace CarDealers.Repository.Car.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Miliage { get; set; }
        public int ColorId { get; set; }
        public decimal Cost { get; set; }
        public int CardealershipId { get; set; }
    }
}
