using Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Net.Mime;

namespace Presentation.Controllers
{
    public static class FileController
    {
        public static IEndpointRouteBuilder MapFileController(this IEndpointRouteBuilder routers)
        {
            routers.MapPost("/uploadFile", UploadFile);
            routers.MapPost("/uploadmanyfiles", UploadManyFiles);
            routers.MapGet("/downloadFile", DownloadFile);
            return routers;
        }

        public static async Task<IResult> UploadFile([FromServices] IFileService formFileService, [FromForm] IFormFile formfile)
        {
            var file = await formFileService.SaveFileToDisk(formfile);
            return Results.Ok(file);
        }

        public static async Task<IResult> UploadManyFiles([FromServices] IFileService formFileService, [FromForm] IFormFileCollection formfile)
        {
            var file = await formFileService.SaveMultipleFileToDisk(formfile);
            return Results.Ok(file);
        }

        public static async Task<IResult> DownloadFile([FromServices] IFileService formFileService, [FromQuery] string fileName)
        {
            byte[] file = await formFileService.GetFile(fileName);

            if (file != null)
            {
                var fileContentResult = new FileContentResult(file, "application/octet-stream")
                {
                    FileDownloadName = fileName
                };

                return Results.Ok(fileContentResult);
            }

            return Results.NoContent();
        }
    }
}
