using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using VOD.Common.DTOs;
using VOD.Database.Contexts;
using VOD.Database.Entities;
using VOD.Database.Interfaces;
using VOD.Database.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

AutoMapperConfig();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
    opt.AllowAnyOrigin()
       .AllowAnyHeader()
       .AllowAnyMethod()
    );
});


builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding the connection string to the Sql-db defined in the appsettings.json file
builder.Services.AddDbContext<VODContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("VODConnection")));

// registering the dbservice generic CRUD-operations
builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

void AutoMapperConfig()
{
    var autoMapperConfig = new AutoMapper.MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Director, DirectorDTO>().ReverseMap();   // mappar från director till directorDTO, ReverseMap mappar från directorDTO till director
        cfg.CreateMap<Director, FullDirectorDTO>().ReverseMap();
        cfg.CreateMap<CreateDirectorDTO, Director>().ForMember(dir => dir.Id, src => src.Ignore()).ForMember(dir => dir.Films, src => src.Ignore());

        cfg.CreateMap<Genre, GenreDTO>().ReverseMap();
        cfg.CreateMap<Genre, FullGenreDTO>().ReverseMap();
        cfg.CreateMap<CreateGenreDTO, Genre>().ForMember(g => g.Id, src => src.Ignore()).ForMember(g => g.Films, src => src.Ignore());

        cfg.CreateMap<Film, FilmDTO>().ReverseMap();
        cfg.CreateMap<EditFilmDTO, Film>();
        cfg.CreateMap<CreateFilmDTO, Film>();


        cfg.CreateMap<FilmGenre, FilmGenreDTO>().ReverseMap();

        cfg.CreateMap<SimilarFilm, SimilarFilmDTO>().ReverseMap();

    });
    var mapper = autoMapperConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);
}
