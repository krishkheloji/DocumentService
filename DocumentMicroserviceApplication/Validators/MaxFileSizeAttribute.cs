using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentMicroserviceApplication.Validators
{
    public class MaxFileSizeAttribute:ValidationAttribute
    {

        private readonly int _maxBytes;

        public MaxFileSizeAttribute(int maxBytes)
        {
            _maxBytes = maxBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxBytes)
                    return new ValidationResult(ErrorMessage ?? $"File must be less than {_maxBytes / (1024 * 1024)}MB.");
            }
            return ValidationResult.Success;
        }
    }
}
