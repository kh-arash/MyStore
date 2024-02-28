using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyStore.Service.Models;

namespace MyStore.Service.Services.File
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        public FileService(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public async Task<string> Upload(IFormFile fileUpload)
        {
            FileStream fileStream;
            string uploadLink = "";
            try
            {
                if (fileUpload != null && fileUpload.Length > 0)
                {
                    string folderName = "firebaseFiles";
                    string path = Path.Combine(_environment.WebRootPath, $"img/{folderName}");
                    using (fileStream = new FileStream(path, FileMode.Open))
                    {
                        await fileUpload.CopyToAsync(fileStream);
                    }

                    var auth = new Firebase.Auth.FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(_configuration["Firebase:ApiKey"]));
                    var a = await auth.SignInWithEmailAndPasswordAsync(_configuration["Firebase:Username"], _configuration["Firebase:Password"]);

                    // You can use CancellationTokenSource to cancel the upload midway
                    var cancellation = new CancellationTokenSource();

                    var objFirebase = new Firebase.Storage.FirebaseStorage(
                        _configuration["Firebase:Bucket"],
                        new Firebase.Storage.FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            ThrowOnCancel = true
                        })
                        .Child("assets")
                        .Child($"{fileUpload.Name}.{Path.GetExtension(fileUpload.Name)}");

                    uploadLink = await objFirebase.PutAsync(fileStream, cancellation.Token);
                }
                return uploadLink;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
