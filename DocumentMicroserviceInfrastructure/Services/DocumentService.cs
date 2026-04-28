using AutoMapper;
using DocumentMicroserviceApplication.DTO;
using DocumentMicroserviceApplication.Interface;
using DocumentMicroserviceDomain.Entity;
using DocumentMicroserviceInfrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DocumentMicroserviceInfrastructure.Services
{
    public class DocumentService : IDocumentService
    {
         ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;


        public DocumentService(ApplicationDbContext db,IMapper mapper,IWebHostEnvironment env)
        {

            this.db = db;
            this.mapper = mapper;
            this.env = env;

        }
        public  async Task AddDocument(UploadDocumentDto doc)
        {
            //fetching the main root path
            var mainPath = env.WebRootPath;

            //combining the local folder path and the unique file name
            var folderPath = Path.Combine( mainPath + "Content", "Docs" );

            // ensure folder exists (safe)
            Directory.CreateDirectory(folderPath);

            foreach (var file in doc.File)
            {
                //for unique name + extracting extensions and adding with unique filename
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var fileOgName = Guid.NewGuid();
                var doctype = Path.GetExtension(file.FileName);


                //combining the main path and the local path
                string fullPath = Path.Combine(folderPath, fileName);


                //uploading the docs
                await UploadFile(file, fullPath);

                //inserting the records  from dto to models(dB)
                var docs = new Documents()
                {
                    DocName = fileOgName.ToString(),
                    DocType = doctype,
                    PolicyId = doc.PolicyId,
                    DocPath = folderPath,
                    CreatedBy = "Mahesh",
                    CreatedAt = DateTime.Now,
                };
                db.Add(docs);

            }

   
            await db.SaveChangesAsync();

        }



        public async Task UploadFile(IFormFile File, string mainPath)
        {
            using (FileStream stream = new FileStream(mainPath, FileMode.Create))
            {
                await File.CopyToAsync(stream);
            }
        }

        //fetching docs by Id
        public async Task<Documents> GetDocumentById(int docId)
        {
            var doc = db.Documents.Find(docId);
            if (doc == null)
            {
                throw new KeyNotFoundException($"document id{docId} not found ");

            };

            if (doc.DeletedAt != null)
                throw new InvalidOperationException($"Document with ID {docId} has been deleted.");
            return doc;

        }


        //fetching all documents
        public async Task<List<Documents>> GetAllDocuments()
        {
            var docs= await db.Documents.Where(d => d.DeletedAt == null) .ToListAsync();
            return docs;
        }

        //Deleting docs by id
        public async Task DeleteDocId(int docId)
        {
          var doc=await db.Documents.FindAsync(docId);

            if (doc == null)
            {
                throw new KeyNotFoundException($"Document with ID {docId} not found.");
            }

            doc.DeletedBy = "mahesh";
            doc.DeletedAt = DateTime.Now;   

            await db .SaveChangesAsync();
        }



        //Updating the docs

        public async Task UpdateDocument(int docId, UpdateDocumentDto dto)
        {
            var doc = await db.Documents.FindAsync(docId);

            if (doc == null)
                throw new KeyNotFoundException($"Document with ID {docId} not found.");

            if (doc.DeletedAt != null)
                throw new InvalidOperationException($"Document with ID {docId} has been deleted.");

     

            // only replace file if a new one is provided
            if (dto.File != null)
            {
                var mainPath = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var folderPath = Path.Combine(mainPath, "Content", "Docs");
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
                var fullPath = Path.Combine(folderPath, fileName);

                // delete old file if exists
                if (!string.IsNullOrEmpty(doc.DocPath) && File.Exists(doc.DocPath))
                    File.Delete(doc.DocPath);

                await UploadFile(dto.File, fullPath);
                doc.DocPath = fullPath;
                doc.PolicyId = dto.PolicyId;
                doc.DocType = Path.GetExtension(dto.File.FileName);
            }

            await db.SaveChangesAsync();
        }

    }
}
