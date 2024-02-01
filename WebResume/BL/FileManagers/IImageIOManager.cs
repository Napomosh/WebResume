namespace WebResume.BL.FileManagers;

public interface IImageIOManager{
    Task UploadImage(IFormFile fileData);
}