using System.Security.Cryptography;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
    
namespace WebResume.Utils;

public static class ImageIOManager{
    private const int IMAGE_STD_WIDTH = 800;
    private const int IMAGE_STD_HEIGHT = 600;
    private const string IMAGE_JPEG_EXTENSION = ".jpeg";
    public static async Task UploadImage(IFormFile fileData, string fileName, bool needResize = true){
        var resultStream = fileData.OpenReadStream();
        if(needResize)
            await ResizeImage(resultStream, fileName, IMAGE_STD_WIDTH, IMAGE_STD_HEIGHT);
        else{
            using Image image = await Image.LoadAsync(resultStream);
            await image.SaveAsJpegAsync(fileName, new JpegEncoder());
        }
    }
    
    public static string GetImagePath(string source){
        MD5 md5Hasher = MD5.Create();
        byte[] inputImg = System.Text.Encoding.ASCII.GetBytes(source!);
        byte[] hash = md5Hasher.ComputeHash(inputImg);
        string imgHash = Convert.ToHexString(hash);
        return "./wwwroot/images/" + imgHash[..2] + "/"
                         + imgHash[..4];
    }

    public static string CreateImageNameInDirectory(string imgName){
        string imgPath = GetImagePath(imgName!);
        
        if (!Directory.Exists(imgPath))
            Directory.CreateDirectory(imgPath);
        
        return imgPath + "/" + Path.GetFileNameWithoutExtension(imgName) + IMAGE_JPEG_EXTENSION;
    }

    public static async Task ResizeImage(Stream fileStream, string fileName, int newWidth, int newHeight){
        using Image image = await Image.LoadAsync(fileStream);
        int aspectWidth = newWidth;
        int aspectHeight = newHeight;

        if (image.Width / (image.Height / newHeight) > newWidth)
            aspectHeight = (int)(image.Height / (image.Width / (float)newWidth));
        else
            aspectWidth = (int)(image.Width / (image.Height / (float)newHeight));
        
        image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));

        await image.SaveAsJpegAsync(fileName, new JpegEncoder());
    }
}