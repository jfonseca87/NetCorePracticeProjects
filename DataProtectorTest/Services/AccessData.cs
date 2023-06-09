using System;
using Dapper;
using MySql.Data.MySqlClient;

namespace DataProtectorTest.Services;

public class AccessData : IDisposable
{
    private readonly string _conn;
    private MySqlConnection _connection;
    private MySqlTransaction _transaction;

    public AccessData()
    {
        _conn = "Server=localhost;Port=3306;Database=Test;Uid=root;Pwd=Abc.123456;";
    }

    private async Task OpenConnAsync()
    {
        if (_connection == null) 
        {
            _connection = new MySqlConnection(_conn);
        }

        if (_connection.State == System.Data.ConnectionState.Open)
        {
            return;
        }

        await _connection.OpenAsync();
    }

    private async Task CreateTransactionAsync()
    {
        if (_transaction == null)
        {
            _transaction = await _connection.BeginTransactionAsync();
        }
    }

    private async Task CommitAsync()
    {
        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    private async Task CloseConnectionAsync()
    {
        await _connection.CloseAsync();
    }

    public async Task AddSequence()
    {
        await OpenConnAsync();
        await CreateTransactionAsync();
        await _connection.ExecuteAsync($"INSERT INTO tempdata (tempValue) VALUES ('Test Value {DateTime.UtcNow}')");
        await CommitAsync();
        await CloseConnectionAsync();
    }

    public async Task CleanTable()
    {
        await OpenConnAsync();
        await CreateTransactionAsync();
        await _connection.ExecuteAsync($"DELETE FROM tempdata");
        await CommitAsync();
        await CloseConnectionAsync();
    }

    public async void Dispose()
    {
        await CleanTable();
    }
}