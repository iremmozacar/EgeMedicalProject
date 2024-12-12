using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EgeApp.Frontend.Mvc.Models.Category
{
    public class CategoryUpdateViewModel
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Kategori adı gereklidir.")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("imageUrl")]
        [Display(Name = "Kategori Resmi")]
        public string ImageUrl { get; set; }
        [JsonPropertyName("isHome")]
        public bool IsHome { get; set; }
        [JsonIgnore]
        public IFormFile Image { get; set; }
    }
}
