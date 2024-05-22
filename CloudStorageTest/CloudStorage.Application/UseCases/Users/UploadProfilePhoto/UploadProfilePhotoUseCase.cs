using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;
public class UploadProfilePhotoUseCase : IUploadProfilePhotoUseCase
{
    private readonly IStorageService _storageService; 

    public UploadProfilePhotoUseCase(IStorageService storageService)
    {
        _storageService=storageService;
    }
    public void Execute(IFormFile file) //simplesmente executa a regra de negócio
    {
        var fileStream = file.OpenReadStream();

        var isImage = fileStream.Is<JointPhotographicExpertsGroup>(); //garantir que o arquivo passado é uma imagem jpeg

        if(isImage == false) //se não for
            throw new Exception("The file is not an image!");

       var user = GetFromDatabase();

        _storageService.Upload(file, user);

    }

    private User GetFromDatabase()
    {
        return new User
        {
            Id = 1,
            Name = "fulano",
            Email = "tal@gmail.com",
            RefreshToken = "...",
            AccessToken = "..."

        }
    }
}
