using DocumentMicroserviceApplication.DTO;
using DocumentMicroserviceApplication.Interface;
using DocumentMicroserviceInfrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace DocumentMicroAppi.Controllers
{
    [Route("documents")]
    [ApiController]
    public class DocumentHandlerController : ControllerBase
    {
        private readonly IDocumentService services;
        public DocumentHandlerController(IDocumentService services) 
        { 
        this.services = services;    
        
        }


        [HttpPost]
        [Route("Upload/Docs")]
        public async Task<IActionResult> UploadDocs([FromForm] UploadDocumentDto dto)
        {

            await services.AddDocument(dto);
          
            return Ok("File Uploaded successfully");
        }

       [HttpDelete]
        [Route("DeleteDocs/{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                await services.DeleteDocId(id);
                return Ok(new { message = "Document deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            try
            {
                var doc = await services.GetDocumentById(id);
                return Ok(doc);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
           
          
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var docs = await services.GetAllDocuments();
            return Ok(docs);
        }


        [HttpPut]
        [Route("UpdateDocs/{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromForm] UpdateDocumentDto dto)
        {
            try
            {
                await services.UpdateDocument(id, dto);
                return Ok(new { message = "Document updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
          
        }

    }
}
