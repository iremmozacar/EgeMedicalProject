using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace EgeApp.Frontend.Mvc.Models.Category
{
    public class CategoryCreateViewModel
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Kategori adı gereklidir.")]
        public string Name { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("imageUrl")]
        [Display(Name = "Kategori Resmi")]
        public string ImageUrl { get; set; }
        [JsonPropertyName("isHome")]
        public bool IsHome { get; set; }


        [JsonIgnore]
        public IFormFile Image { get; set; }
    }
}
