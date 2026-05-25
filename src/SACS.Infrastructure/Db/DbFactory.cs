using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System;
namespace SACS.Infrastructure.Db;
public class DbFactory
{
    private readonly string _conn;
    public string Schema { get; }

    public DbFactory(IConfiguration cfg)
    {
        _conn = cfg["Database:ConnectionString"]
                 ?? throw new InvalidOperationException("Missing ConnectionString");
        Schema = cfg["Database:Schema"] ?? "procesamiento";
    }

    public IDbConnection Create() => new NpgsqlConnection(_conn);

    /// <summary>Quotea un identificador para PostgreSQL (usa comillas dobles y escapa internamente).</summary>
    public string Quote(string ident)
    {
        if (string.IsNullOrWhiteSpace(ident))
            throw new ArgumentException("Identifier cannot be null/empty", nameof(ident));
        // Escapa comillas dobles dobles:  "  ->  ""
        return $"\"{ident.Replace("\"", "\"\"")}\"";
    }

    /// <summary>Devuelve schema.table correctamente quoteado usando el Schema configurado.</summary>
    public string FullTable(string table)
    {
        if (string.IsNullOrWhiteSpace(table))
            throw new ArgumentException("Table cannot be null/empty", nameof(table));
        return $"{Quote(Schema)}.{Quote(table)}";
    }

    /// <summary>Si ya viene calificado (schema.table), lo respeta; si no, usa Schema.</summary>
    public string Qualify(string maybeQualified)
    {
        if (string.IsNullOrWhiteSpace(maybeQualified))
            throw new ArgumentException("Identifier cannot be null/empty", nameof(maybeQualified));
        var parts = maybeQualified.Split('.', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length switch
        {
            1 => $"{Quote(Schema)}.{Quote(parts[0])}",
            2 => $"{Quote(parts[0])}.{Quote(parts[1])}",
            _ => throw new ArgumentException("Invalid identifier format", nameof(maybeQualified))
        };
    }
}