using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentMicroserviceApplication.Validators;

namespace DocumentMicroserviceApplication.DTO
{
    public class UploadDocumentDto
    {


        //public string? DocName { get; set; }
        public int PolicyId { get; set; }

        //public string? DocType { get; set; }


        [Required(ErrorMessage = "File is required.")]
        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = "File size must not exceed 2MB.")]
        [AllowedExtensions(new[] { ".pdf", ".docx", ".png", ".jpg" }, ErrorMessage = "File type not allowed.")]
        public List<IFormFile> File { get; set; }
    }
}
