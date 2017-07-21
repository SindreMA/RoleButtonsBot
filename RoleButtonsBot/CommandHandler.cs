using System;
using Discord;
using System.Collections.Generic;
using System.Text;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using RoleButtonsBot.DTO;

namespace TestEasyBot
{
    class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        public static List<ButtonLinks> buttonlinks = new List<ButtonLinks>();

        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            _service.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += _client_MessageReceived;
            _client.ReactionAdded += _client_ReactionAdded;
            _client.ReactionRemoved += _client_ReactionRemoved;
            try
            {

                string json = File.ReadAllText("ButtonLinks.json");
                buttonlinks = JsonConvert.DeserializeObject<List<ButtonLinks>>(json);
            }
            catch (Exception)
            {
            }
        }

        private async Task _client_ReactionRemoved(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {

            var messageid = arg1;
            var channel = arg2;
            var socketuser = arg3;
            var msg = channel.GetMessageAsync(socketuser.MessageId).Result;
            IGuild GuildG = null;
            foreach (var Guild in _client.Guilds)
            {
                foreach (var Channel in Guild.TextChannels)
                {
                    if (arg3.Channel.Id == Channel.Id)
                    {
                        GuildG = Channel.Guild;
                        break;
                    }
                }
            }

            if (buttonlinks.Exists(x => x.Emoji == socketuser.Emote.Name && x.GuildID == GuildG.Id && x.MessageID == socketuser.MessageId))
            {
                var item = buttonlinks.Find(x => x.Emoji == socketuser.Emote.Name && x.GuildID == GuildG.Id && x.MessageID == socketuser.MessageId);
                var guild = _client.GetGuild(item.GuildID);
                var role = guild.GetRole(item.RoleID);
                var user = guild.GetUser(socketuser.UserId);
                await user.RemoveRoleAsync(role);
            }
            else if (buttonlinks.Exists(x => x.Emoji.Contains(socketuser.Emote.Name) && x.GuildID == GuildG.Id && x.MessageID == socketuser.MessageId))
            {
                var item = buttonlinks.Find(x => x.Emoji.Contains(socketuser.Emote.Name) && x.GuildID == GuildG.Id && x.MessageID == socketuser.MessageId);
                var guild = _client.GetGuild(item.GuildID);
                var role = guild.GetRole(item.RoleID);
                var user = guild.GetUser(socketuser.UserId);
                await user.RemoveRoleAsync(role);
            }
        }

        private async Task _client_ReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {

            var messageid = arg1;
            var channel = arg2;
            var socketuser = arg3;
            var msg = channel.GetMessageAsync(socketuser.MessageId).Result;
            IGuild GuildG = null;
            foreach (var Guild in _client.Guilds)
            {
                foreach (var Channel in Guild.TextChannels)
                {
                    if (arg3.Channel.Id == Channel.Id)
                    {
                        GuildG = Channel.Guild;
                        break;
                    }
                }
            }
            if (buttonlinks.Exists(x => x.Emoji == socketuser.Emote.Name && x.GuildID == GuildG.Id && x.MessageID == socketuser.MessageId))
            {
                var item = buttonlinks.Find(x => x.Emoji == socketuser.Emote.Name && x.GuildID == GuildG.Id && x.MessageID == socketuser.MessageId);
                var guild = _client.GetGuild(item.GuildID);
                var role = guild.GetRole(item.RoleID);
                var user = guild.GetUser(socketuser.UserId);
                await user.AddRoleAsync(role);
            }
            else if (buttonlinks.Exists(x => x.Emoji.Contains(socketuser.Emote.Name) && x.GuildID == GuildG.Id && x.MessageID == socketuser.MessageId))
            {
                var item = buttonlinks.Find(x => x.Emoji.Contains(socketuser.Emote.Name) && x.GuildID == GuildG.Id && x.MessageID == socketuser.MessageId);
                var guild = _client.GetGuild(item.GuildID);
                var role = guild.GetRole(item.RoleID);
                var user = guild.GetUser(socketuser.UserId);
                await user.AddRoleAsync(role);
            }
        }

        private async Task _client_MessageReceived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);
            int argPost = 0;
            if (msg.HasCharPrefix('.', ref argPost))
            {
                var result = _service.ExecuteAsync(context, argPost);
                if (!result.Result.IsSuccess && result.Result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.Result.ErrorReason);
                }
                Program.Log("Invoked " + msg + " in " + context.Channel + " with " + result.Result, ConsoleColor.Magenta);
            }
            else
            {
                Program.Log(context.Channel + "-" + context.User.Username + " : " + msg, ConsoleColor.White);
            }

        }
    }
}
