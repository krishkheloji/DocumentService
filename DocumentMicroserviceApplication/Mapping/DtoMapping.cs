using AutoMapper;
using DocumentMicroserviceApplication.DTO;
using DocumentMicroserviceDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentMicroserviceApplication.Mapping
{
    public class DtoMapping:Profile
    {

        public DtoMapping()
        {

            CreateMap<Documents, UploadDocumentDto>().ReverseMap();

        }

    }
}
