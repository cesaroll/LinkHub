using System.ComponentModel.DataAnnotations;
using BLL.Areas;
using BLL.Entities;

namespace LinkHubUI.Areas.User.ViewModels
{
    public class UrlsForm
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Url]
        [UniqueUrl]
        public string Url { get; set; }

        [Required]
        public string Desc { get; set; }
       
    }

    public class UniqueUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (new UserAreaBs()).UrlBs.UrlExist(value.ToString())?
                        new ValidationResult("Url already exist") : ValidationResult.Success;
        }
    }

}