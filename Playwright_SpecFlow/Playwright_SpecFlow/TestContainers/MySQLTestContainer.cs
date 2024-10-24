using System;
using MySql.Data.MySqlClient;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using FluentAssertions;
using NUnit.Framework;


namespace Playwright_SpecFlow.TestContainers
{

    internal class MySQLTestContainer
    {
        private Config _config = new Config();
        private MySqlConnection connection;
        private MySqlCommand command;


        [OneTimeSetUp]
        public async Task Setup()
        {
            await _config.ContainerSetup();
        }
        public async Task StartMySqlContainer()
        {

            // Crear la tabla y almacenar usuarios
            using (var connection = new MySqlConnection(_config._constring))
            {
                connection.Open();
                var command = connection.CreateCommand(); 
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Username VARCHAR(255),
                    Password VARCHAR(255)
                );
                INSERT INTO Users (Username, Password) VALUES
                ('standard_user', 'secret_sauce'),
                ('locked_out_user', 'secret_sauce'),
                ('problem_user', 'secret_sauce'),
                ('performance_glitch_user', 'secret_sauce'),
                ('error_user', 'secret_sauce'),
                ('visual_user', 'secret_sauce');
            ";
                command.ExecuteNonQuery();
            }
        }
    }
}
    
