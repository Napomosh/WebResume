using WebResume.DAL;
using Microsoft.EntityFrameworkCore;
using WebResume.BL;
using WebResume.BL.Auth;
using WebResume.BL.General;
using WebResume.Utils;
using ISession = WebResume.BL.Auth.ISession;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MsSqlAppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IWebCookie, WebCookie>();
builder.Services.AddScoped<IDbSession, DbSession>();
builder.Services.AddScoped<IDbUser, DbUser>();
builder.Services.AddScoped<IDbUserToken, DbUserToken>();
builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddScoped<IEncrypt, Encrypt>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<ISession, Session>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()){
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();