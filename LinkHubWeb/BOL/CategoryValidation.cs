using System.ComponentModel.DataAnnotations;

namespace BOL
{
    public class CategoryValidation
    {
        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string CategoryDesc { get; set; } 
    }

    [MetadataType(typeof(CategoryValidation))]
    public partial class T_Category
    {
        
    }
}