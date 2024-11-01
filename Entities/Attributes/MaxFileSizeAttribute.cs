using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Web.Attributes
{
    public class MaxFileSizeAttribute:ValidationAttribute
    {
        private readonly int maxFileSizeInMB;

        public MaxFileSizeAttribute(int maxFileSizeInMB)
        {
            this.maxFileSizeInMB = maxFileSizeInMB;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as FormFile;
            var maxFileInByte = maxFileSizeInMB * 1024 * 1024;
            if (file != null) {

                if (file.Length  > maxFileInByte) return new ValidationResult($"the max Size is {maxFileSizeInMB} MB "); 
            }
            return ValidationResult.Success;

        }
    }
}
