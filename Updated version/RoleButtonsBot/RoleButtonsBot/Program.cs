using Discord;
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
        static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();        private CommandHandler _handler;        private DiscordSocketClient _client;        public string Token = "";
        public async Task StartAsync()        {

            try            {                string json = File.ReadAllText("Token.json");                Token = JsonConvert.DeserializeObject<string>(json);            }            catch (Exception)            {                Log("Cant import key from json file!", ConsoleColor.Red);            }            Log("Setting up the bot", ConsoleColor.Green);            _client = new DiscordSocketClient();            new CommandHandler(_client);            Log("Logging in...", ConsoleColor.Green);            try
            {
                await _client.LoginAsync(TokenType.Bot, Token);
                                Log("Connecting...", ConsoleColor.Green);
            await _client.StartAsync();            _client.GuildAvailable += _client_GuildAvailable;            await Task.Delay(-1);
            _handler = new CommandHandler(_client);
            }
            catch (Exception)
            {
                Log("Cant login, check that token is valid!", ConsoleColor.Red);
                Console.ReadLine();
            }
        }
        private Task _client_GuildAvailable(SocketGuild arg)        {
            Log(arg.Name + " connected!", ConsoleColor.Green);
            return null;        }
        public static void Log(string message, ConsoleColor color)        {            Console.ForegroundColor = color;
            Console.WriteLine(DateTime.Now + " : " + message, color);            Console.ResetColor();        }    }
}
