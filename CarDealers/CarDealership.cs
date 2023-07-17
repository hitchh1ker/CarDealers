using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealers.Repository
{
    public class CarDealership
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CityId { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
    }
}
