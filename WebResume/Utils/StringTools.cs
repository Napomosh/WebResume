namespace WebResume.Utils;

public static class StringTools{
    public static Guid StringToGuid(string? value){
        if(value == null) 
            return Guid.Empty;
        return Guid.TryParse(value, out var res) ? res : Guid.Empty;
    }
}