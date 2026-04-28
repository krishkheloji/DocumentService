using DocumentMicroserviceApplication.DTO;
using DocumentMicroserviceDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DocumentMicroserviceApplication.Interface
{
    public interface IDocumentService
    {
        public Task AddDocument(UploadDocumentDto doc);

        Task DeleteDocId(int docId);
        Task<Documents> GetDocumentById(int docId);
        Task<List<Documents>> GetAllDocuments();

        Task UpdateDocument(int docId, UpdateDocumentDto dto);
    }
}
