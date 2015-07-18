using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BOL
{
    public class UserValidation
    {
        [Required]
        [EmailAddress]
        [UniqueEmail]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    [MetadataType(typeof(UserValidation))]
    public partial class T_User
    {
        public string ConfirmPassword { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var myUser = validationContext.ObjectInstance as T_User;

            var db = new LinkHubEntities();

            return db.T_User.Count(x => (x.UserEmail != myUser.UserEmail && x.UserEmail == value.ToString())) > 0
                ? new ValidationResult("Email already exist")
                : ValidationResult.Success;
        }
    }


}