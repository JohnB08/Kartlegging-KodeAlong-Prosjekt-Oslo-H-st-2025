using Kartleggign_LiveChat_Applikasjon.Models;
using Kartleggign_LiveChat_Applikasjon.SignalrHubs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ChannelRepository>();

builder.Services.AddSingleton<UserRepository>();

builder.Services.AddSingleton<UserChannelRelationshipRepository>();

builder.Services.AddLogging();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");

app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();