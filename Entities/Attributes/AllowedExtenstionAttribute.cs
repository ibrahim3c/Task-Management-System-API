using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Web.Attributes
{
    public class AllowedExtenstionAttribute : ValidationAttribute
    {
        private readonly string allowedExtensions;
        public AllowedExtenstionAttribute(string allowedExtenstions)
        {
            this.allowedExtensions = allowedExtenstions;

        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {

                var extenstion = Path.GetExtension(file.FileName);
                var isAllowed = allowedExtensions.Split(",").Contains(extenstion, StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    return new ValidationResult($"Only {allowedExtensions} are allowed!");
                }

                return ValidationResult.Success;
            }
            return new ValidationResult("the File is Required");
        }

    }
}
