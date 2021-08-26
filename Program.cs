using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

// builder.Services.Configure<MicrosoftIdentityOptions>(OpenIdConnectDefaults.AuthenticationScheme, x =>
// {
//     //x.TokenValidationParameters.ValidIssuer = "https://login.microsoftonline.com/jpda.onmicrosoft.com/v2.0";
//     x.TokenValidationParameters.ValidIssuers = new[] { "https://login.microsoftonline.com/98a34a88-7940-40e8-af71-913452037f31/v2.0", "https://login.microsoftonline.com/55193a41-5b56-4f8a-913a-20087af59ae9/v2.0" };
//     // x.TokenValidationParameters = new TokenValidationParameters
//     // {
//     //     ValidateIssuer = true,
//     //     ValidIssuers = new[] { "https://login.microsoftonline.com/jpda.onmicrosoft.com/v2.0", "https://login.microsoftonline.com/55193a41-5b56-4f8a-913a-20087af59ae9/v2.0" }
//     // };
//     x.ResponseType = "code";
// });

builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, x =>
{
    x.TokenValidationParameters.ValidIssuer = "https://login.microsoftonline.com/jpda.onmicrosoft.com/v2.0";
    //x.TokenValidationParameters.ValidIssuers = new[] { "https://login.microsoftonline.com/98a34a88-7940-40e8-af71-913452037f31/v2.0", "https://login.microsoftonline.com/55193a41-5b56-4f8a-913a-20087af59ae9/v2.0" };
    x.ResponseType = "code";
});

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
