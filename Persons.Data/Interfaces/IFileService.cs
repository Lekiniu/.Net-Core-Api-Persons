using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Persons.Data.Models;
using Microsoft.AspNetCore.Http;

namespace Persons.Data.Interfaces
{
    public interface IFileService
    {
        Task UploadFileAsync(int personId, IFormFile file);

        Task EditFileAsync(int personId, IFormFile file);

        Task DeleteFileAsync(int personId, int fileId);
    }
}
