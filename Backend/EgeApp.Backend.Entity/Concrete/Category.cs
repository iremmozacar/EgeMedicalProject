using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgeApp.Backend.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public bool IsHome { get; set; }

        public string ImageUrl { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}