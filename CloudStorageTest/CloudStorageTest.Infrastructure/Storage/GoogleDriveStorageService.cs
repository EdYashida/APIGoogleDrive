using CloudStorageTest.Domain.Entities;
using CloudStorageTest.Domain.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;

namespace CloudStorageTest.Infrastructure.Storage;
public class GoogleDriveStorageService : IStorageService //regra de negócio não enxerga tal classe
{

    private readonly GoogleAuthorizationCodeFlow _authorization;

    public GoogleDriveStorageService(GoogleAuthorizationCodeFlow authorization)
    {
        _authorization = authorization;
    }



    public string Upload(IFormFile file, User user)
    {
        var credential = new UserCredential(null, user.Email, new Google.Apis.Auth.OAuth2.Responses.TokenResponse
        {
            AccessToken = user.AccessToken,
            RefreshToken = user.RefreshToken
        });

        var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer
        {
            ApplicationName = "GoogleDriveTest",
            HttpClientInitializer = credential
        });

        var driveFile = new Google.Apis.Drive.v3.Data.File
        {
            Name = file.Name,
            MimeType = file.ContentType //tipo do arquivo
        };

        var command = service.Files.Create(driveFile, file.OpenReadStream(), file.ContentType;
        command.Fields = "id";//garante que será obtido o id

        var response = command.Upload();

        if (response.Status is not Google.Apis.Upload.UploadStatus.Completed)
            throw response.Exception;

        return command.ResponseBody.Id;
    }
}
