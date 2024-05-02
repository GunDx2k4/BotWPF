using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace BotWPF.Bot.Core.Services
{
    public class MessageService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _service;
        public MessageService(DiscordSocketClient client, CommandService commands, IServiceProvider service)
        {
            _client = client;
            _commands = commands;
            _service = service;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: _service);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;


            if (message.Channel is SocketGuildChannel guildChannel)
            {
                Console.WriteLine($"[Message/{message.Author.Username}] {message.Content} ====> [Channel/{guildChannel.Name}, Guild/{guildChannel.Guild.Name}]");
            }
            else if (message.Channel is SocketDMChannel dmChannel)
            {
                Console.WriteLine($"[Message/{message.Author.Username}] {message.Content} ====> [User/{dmChannel.Recipient.Username}]");
            }

            int argPos = 0;

            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }
    }
}
