using Microsoft.AspNetCore.Mvc;

namespace TeamsLektion1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddScoped<MyService, MyService>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
        app.MapControllers();

        app.Run();
    }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

[ApiController]
[Route("api")]
public class MyController : ControllerBase
{
    private MyService service;

    public MyController(MyService service)
    {
        this.service = service;
    }

    [HttpGet("hello")]
    public IActionResult SayHello([FromBody] Person person)
    {
        try
        {
            service.ValidateInput(person);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }

        return Ok(person);
    }
}

public class MyService
{
    public void ValidateInput(Person person)
    {
        if (person.Name == "Hulk")
        {
            throw new ArgumentException("Name cannot be Hulk!");
        }
    }
}
