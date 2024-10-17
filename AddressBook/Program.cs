using Microsoft.EntityFrameworkCore;
using AddressBook.DataAccess;
using AddressBook.Application;
using AddressBook.DataAccess.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IContactsService, ContactsService>();
        builder.Services.AddScoped<IContactsRepository, ContactsRepository>();

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        builder.Services.AddDbContext<DataContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                options => options.MigrationsAssembly("AddressBook.DataAccess"));
        });

        builder.Services.AddScoped<IDataContext, DataContext>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("AllowAllOrigins");
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}