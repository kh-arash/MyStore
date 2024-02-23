using MyStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Service.Services.File
{
    public interface IFileService
    {
        Task<string> Upload(FileUpload file);
    }
}
