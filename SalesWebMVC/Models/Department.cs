using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesWebMVC.Models
{
    public class Department
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(string name)
        {
            Name = name;
        }

        public void AddSales(Seller seller) => Sellers.Add(seller);
        public double TotalSales(DateTime initialDate, DateTime finalDate) => Sellers.Sum(seller => seller.TotalSales(initialDate, finalDate));
    }
}
