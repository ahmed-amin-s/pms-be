using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product
{
    public class Product : BaseEntity<int>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        //public string? ImageURL { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
