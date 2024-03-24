namespace WebResume.BL.General;

public interface IWebCookie{
    void AddSecure(string name, string value, int days = 0);
    void Add(string name, string value, int days = 0);
    void Delete(string name);
    string? Get(string name);
}