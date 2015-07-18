using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BOL
{
    public class UrlValidation
    {
        [Required]
        public string UrlTitle { get; set; }

        [Required]
        [Url]
        [UniqueUrl]
        public string Url { get; set; }
        
        [Required]
        public string UrlDesc { get; set; }
        
        [Required]
        public int CategoryId { get; set; }

        //[Required]
        //public int UserId { get; set; }

        //[Required]
        //public string IsApproved { get; set; }

    }

    [MetadataType(typeof(UrlValidation))]
    public partial class T_Url
    {
        
    }
    
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var myUrl = validationContext.ObjectInstance as T_Url;
            
            var db = new LinkHubEntities();

            return db.T_Url.Count(x => (x.UrlId != myUrl.UrlId && x.Url == value.ToString() )) > 0
                ? new ValidationResult("Url already exist")
                : ValidationResult.Success;
        }
    }

}