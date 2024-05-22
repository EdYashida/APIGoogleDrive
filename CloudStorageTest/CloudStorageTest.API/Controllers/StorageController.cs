using CloudStorageTest.Application.UseCases.Users.UploadProfilePhoto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorageTest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StorageController : ControllerBase
{
    [HttpPost]
    public IActionResult UploadImage([FromServices] IUploadProfilePhotoUseCase useCase, 
        IFormFile file) { //forma de devolver os cod status


        useCase.Execute(file);

        return Created();
    }


}
