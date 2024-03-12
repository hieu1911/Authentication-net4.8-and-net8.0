using LoginAndChatRealTime.Helper;
using LoginAndChatRealTime.Hubs;
using LoginAndChatRealTime.Interfaces;
using LoginAndChatRealTime.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Synercoding.FormsAuthentication;
using System;
using static System.Collections.Specialized.BitVector32;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7213", "https://localhost:44394").AllowAnyHeader()
                                                  .AllowAnyMethod()
                                                  .AllowCredentials();
                      });
});

var faOptions = new FormsAuthenticationOptions()
{
    DecryptionKey = "F6D5A5C8DDEC57481610829F58D6C95BDAC5FA21082F3FA9CB5A36DCEAACBEDB",
    ValidationKey = "F2D27DF0348E9A3EAD6AC66330C31F821394D4CD1A5E139EEE85EA9D9F2A963E55EC87572F699FB834292CC9E37AD56B6B26AA379106CBA5E9AA544C688F3E92",
    EncryptionMethod = EncryptionMethod.AES,
    ValidationMethod = ValidationMethod.SHA1,
};


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.LoginPath = "/Login";
        options.Cookie.Name = "CookieAuthen";
        options.TicketDataFormat = new FormsAuthenticationDataFormat<AuthenticationTicket>(
                        faOptions,
                        FormsAuthHelper.ConvertCookieToTicket,
                        FormsAuthHelper.ConvertTicketToCookie
                    );
    });


builder.Services.AddScoped<IUserSerivce, UserService>(); 
builder.Services.AddScoped<IMessageSerivce, MessageService>();
builder.Services.AddScoped<IRoomService, RoomService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(MyAllowSpecificOrigins);
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/chatHub");

app.Run();
