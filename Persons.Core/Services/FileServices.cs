using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persons.Data.Entities;
using Persons.Data.Interfaces;
using Persons.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Core.Services
{
    public class FileServices : IFileService
    {
        private readonly PersonsDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public FileServices(PersonsDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        //function to add and edit images 
        public async Task UploadFileAsync(int personId, IFormFile file)
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");

                if (file != null)
                {
                    var fileNameLength = file.FileName.Length > 10 ? file.FileName.Substring(file.FileName.Length-10) : file.FileName;
                    string FileName = personId + GuidString + fileNameLength;
                    string filePath = @"\Files\";
                    var path = Path.Combine(_appEnvironment.WebRootPath + filePath + FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _context.Files.AddAsync(new Files()
                    {
                        Name = file.FileName,
                        Url = @"/Files/" + FileName,
                        PersonId = personId,
                    });
                  await _context.SaveChangesAsync();
                }
        }

        //edit files
        public async  Task EditFileAsync(int personId, IFormFile file)
        {
            if (file != null)
            {
                var oldfile = await _context.Files.FirstOrDefaultAsync(x => x.PersonId == personId);

                await DeleteFileAsync(personId, oldfile.FileId);

                await UploadFileAsync(personId, file);

              
            }
        }

        //delete files
        public async Task DeleteFileAsync(int personId, int fileId)
        {
                var file = await _context.Files.FirstOrDefaultAsync(x => x.PersonId == personId && x.FileId == fileId);
                var path = Path.Combine(_appEnvironment.WebRootPath + file.Url);
                if (File.Exists(path))
                {
                    _context.Files.Remove(file);
                    File.Delete(path);
                }
                //var deletableImages = await _context.Files.FirstOrDefaultAsync(e => e.BookId == bookId && e.FileId == file.FileId);

                _context.Files.Remove(file);

             await _context.SaveChangesAsync();
        }
    }
}
