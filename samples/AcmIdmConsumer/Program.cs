using Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAcmIdmAuthentication(
    "",
    "",
    "",
    "");

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapGet("/", () => { })
    .RequireAuthorization(
        "dv_gr_geschetstgebouw_beheer",
        "dv_gr_geschetstgebouw_uitzonderingen");
app.UseRouting();

app.UseAuthorization()
    .UseAcmIdmAuthorization();

app.MapRazorPages();

app.Run();
