using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Data.IRepository;
using Notes.Data.Models;
using Notes.Data.Repository;
using Notes.Services;
using Notes.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NotesDBContext>(options => options.UseSqlServer(builder.Configuration["AZURE_SQL_CONNECTION_STRING"],
        b => b.MigrationsAssembly("Notes.API")));

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<INoteAndCategoryRepository, NoteAndCategoryRepository>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<INoteWithCategoryService, NoteWithCategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
