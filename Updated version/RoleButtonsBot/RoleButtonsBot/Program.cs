﻿using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleButtonsBot
{
    class Program
    {
        static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();
        public async Task StartAsync()

            try
            {
                await _client.LoginAsync(TokenType.Bot, Token);
        
            await _client.StartAsync();
            _handler = new CommandHandler(_client);
            }
            catch (Exception)
            {
                Log("Cant login, check that token is valid!", ConsoleColor.Red);
                Console.ReadLine();
            }
        }
        private Task _client_GuildAvailable(SocketGuild arg)
            Log(arg.Name + " connected!", ConsoleColor.Green);
            return null;
        public static void Log(string message, ConsoleColor color)
            Console.WriteLine(DateTime.Now + " : " + message, color);
}