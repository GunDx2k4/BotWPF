using BotWPF.Base;
using BotWPF.Bot.Core;
using BotWPF.ViewModels;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace BotWPF.Bot.Services
{
    class BotService
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;

        public BotService(DiscordSocketClient client, InteractionService commands)
        {
            _client = client;
            _commands = commands;
        }

        public async Task ReadyClientAsync()
        {
            await _commands.RegisterCommandsGloballyAsync();
            await _client.SetActivityAsync(new Game("BOT Quản lý , hỗ trợ những việc linh tinh ..... Code By C# Owner : GunDx(dungdzk4#8028)"));
            BotDiscord.gI().IsStarted = true;

            DialogViewModel.Instance.OpenDialog($"Connected to BOT {BotDiscord.gI().ClientBot.CurrentUser.Username}");
            NavigationMainView.Instance.createMainView();
        }

        public Task ConnectedClientAsync()
        {
            Console.WriteLine($"[Connected/{_client.CurrentUser.Username}] ....");
            return Task.CompletedTask;
        }


        public Task DisconnectedClientAsync(Exception e)
        {
            BotDiscord.gI().IsStarted = false;
            Console.WriteLine($"[Disconnected/{_client.CurrentUser?.Username}] {e} ....");
            return Task.CompletedTask;
        }
    }
}
