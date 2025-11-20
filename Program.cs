using Microsoft.EntityFrameworkCore;
using ReservaCinema.Models;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ContextoBancoDeDados>(options => options.UseSqlServer(connectionString));
var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/sessoes", async (ContextoBancoDeDados contexto) => {
    return await contexto.Sessoes.Select(sessao => new SessaoDTO(sessao)).ToArrayAsync();
});

app.MapPost("/sessoes", async (ContextoBancoDeDados contexto, Sessao sessao) =>
{
    contexto.Sessoes.Add(sessao);
    await contexto.SaveChangesAsync();

    for (var i = 1; i <= sessao.QuantidadeTotalAssentos; i++)
    {
        Assento assento = new()
        {
            IdSessao = sessao.Id,
            NumeroAssento = i
        };
        contexto.Assentos.Add(assento);
        await contexto.SaveChangesAsync();
    }

    return Results.Created($"/sessoes/{sessao.Id}", new SessaoDTO(sessao));
});

app.MapGet("/sessoes/{id}", async (ContextoBancoDeDados contexto, int id) =>
{
    var sessao = await contexto.Sessoes.FindAsync(id);
    if (sessao is null) return Results.NotFound();

    return Results.Ok(new SessaoDTO(sessao));
});

app.MapPut("/sessoes/{id}", async (ContextoBancoDeDados contexto, int id, Sessao inputSessao) =>
{
    var sessao = await contexto.Sessoes.FindAsync(id);
    if (sessao is null) return Results.NotFound();

    sessao.Sala = inputSessao.Sala;
    sessao.SinopseFilme = inputSessao.SinopseFilme;
    sessao.Filme = inputSessao.Filme;
    sessao.DiretoresFilme = inputSessao.DiretoresFilme;
    sessao.DataHora = inputSessao.DataHora;
    if (sessao.QuantidadeTotalAssentos < inputSessao.QuantidadeTotalAssentos)
    {
        for (var i = sessao.QuantidadeTotalAssentos + 1; i <= inputSessao.QuantidadeTotalAssentos; i++)
        {
            contexto.Assentos.Add(new ()
            {
                IdSessao = sessao.Id,
                NumeroAssento = i,
            });
            await contexto.SaveChangesAsync();
        }
    } else if (sessao.QuantidadeTotalAssentos > inputSessao.QuantidadeTotalAssentos)
    {
        var assentosDeletar = contexto.Assentos.Where(assento => assento.IdSessao == sessao.Id)
                                .OrderBy(a => a.NumeroAssento)
                                .Skip(inputSessao.QuantidadeTotalAssentos)
                                .Take(sessao.QuantidadeTotalAssentos - inputSessao.QuantidadeTotalAssentos);

        foreach (var assento in assentosDeletar)
        {
            contexto.Assentos.Remove(assento);
        }
    }
    sessao.QuantidadeTotalAssentos = inputSessao.QuantidadeTotalAssentos;

    await contexto.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/sessoes/{id}", async (ContextoBancoDeDados contexto, int id) =>
{
    if (await contexto.Sessoes.FindAsync(id) is Sessao sessao)
    {
        contexto.Sessoes.Remove(sessao);
        await contexto.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.MapGet("assentos/{idSessao}", async (ContextoBancoDeDados contexto, int idSessao) =>
{
    return Results.Ok(
        await contexto.Assentos
            .Where(assento => assento.IdSessao == idSessao)
            .Select(assento => new AssentoDTO(assento))
            .ToListAsync()
    );
});

app.MapPatch("reservar-assento/{id}", async (ContextoBancoDeDados contexto, int id) =>
{
    var assento = await contexto.Assentos.FindAsync(id);
    if (assento is null) return Results.NotFound();

    if (assento.Reservado)
        return Results.BadRequest(new BadHttpRequestException("Assento já está reservado"));

    assento.Reservado = true;

    await contexto.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
