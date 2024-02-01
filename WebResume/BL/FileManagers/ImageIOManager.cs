using System.Security.Cryptography;

namespace WebResume.BL.FileManagers;

public class ImageIOManager : IImageIOManager{
    
    public async Task UploadImage(IFormFile fileData){
        string imgName = fileData!.FileName;
        string fileName = GetFullImageName(imgName);
        
        using (var fStream = System.IO.File.Create(fileName))
            await fileData!.CopyToAsync(fStream);
    }


    private string GetImagePath(string source){
        MD5 md5Hasher = MD5.Create();
        byte[] inputImg = System.Text.Encoding.ASCII.GetBytes(source!);
        byte[] hash = md5Hasher.ComputeHash(inputImg);
        string imgHash = Convert.ToHexString(hash);
        return "./wwwroot/images/" + imgHash[..2] + "/"
                         + imgHash[..4];
    }

    private string GetFullImageName(string imgName){
        string imgPath = GetImagePath(imgName!);
        
        if (!Directory.Exists(imgPath))
            Directory.CreateDirectory(imgPath);

        return imgPath + "/" + imgName;
    }
}