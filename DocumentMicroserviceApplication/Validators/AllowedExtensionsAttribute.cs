using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentMicroserviceApplication.Validators
{
    public  class AllowedExtensionsAttribute: ValidationAttribute
    {


        private readonly string[] extension;

        public AllowedExtensionsAttribute(string[] extension)
        {
           this. extension = extension;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value is IFormFile file)
            {
                var ext = Path.GetExtension(file.FileName).ToLower();
                if (!extension.Contains(ext))
                    return new ValidationResult(ErrorMessage ?? "This file type is not allowed.");
            }
            return ValidationResult.Success;
        }

    }
}
