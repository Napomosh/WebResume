namespace WebResume.BL.General;

public class WebCookie(IHttpContextAccessor httpContextAccessor) : IWebCookie{
    public void AddSecure(string name, string value, int hours = 0){
        CookieOptions options = new CookieOptions{
            Path = "/",
            HttpOnly = true,
            Secure = true,
        };
        if(hours > 0)
            options.Expires = DateTimeOffset.Now.AddHours(hours);
        httpContextAccessor?.HttpContext?.Response.Cookies.Append(
            name, value, options);
    }

    public void Add(string name, string value, int hours = 0){
        CookieOptions options = new CookieOptions{
            Path = "/",
        };
        if(hours > 0)
            options.Expires = DateTimeOffset.Now.AddHours(hours);
        httpContextAccessor?.HttpContext?.Response.Cookies.Append(
            name, value, options);
    }

    public void Delete(string name){
        httpContextAccessor?.HttpContext?.Response.Cookies.Delete(name);
    }

    public string? Get(string name){
        var cookie = httpContextAccessor?.HttpContext?.Request.Cookies.
                                            FirstOrDefault(x => x.Key == name);
        if (cookie != null && cookie.Value.Value != null)
            return cookie.Value.Value;
        return null;
    }
}