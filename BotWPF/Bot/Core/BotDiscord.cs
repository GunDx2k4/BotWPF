using BotWPF.Bot.Core.Services;
using BotWPF.Bot.Services;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;

namespace BotWPF.Bot.Core
{
    public class BotDiscord
    {
        public DiscordSocketClient ClientBot { get; private set; }

        public bool IsStarted { get; set; }

        private string _token;

        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                if (Token != value)
                {
                    _token = value;
                }
                else if (Token == null)
                {
                    _token = "Null";
                }
            }
        }

        private static BotDiscord bot;

        private IServiceProvider services;

        private SlashCommandService interactionHandler;

        private InteractionService interactionService;

        private MessageService commandHandler;

        private CommandService commandService;

        private BotService botService;

        public BotDiscord()
        {

        }

        public static BotDiscord gI()
        {
            return bot ??= new BotDiscord();
        }

        private async Task<DiscordSocketClient> CreateBot()
        {
            var ClientBot = new DiscordSocketClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
                GatewayIntents = GatewayIntents.All
            });

            interactionService = new InteractionService(ClientBot);
            interactionHandler = new SlashCommandService(ClientBot, interactionService, services);

            commandService = new CommandService();
            commandHandler = new MessageService(ClientBot, commandService, services);

            botService = new BotService(ClientBot, interactionService);

            ClientBot.Connected += botService.ConnectedClientAsync;
            ClientBot.Disconnected += botService.DisconnectedClientAsync;
            ClientBot.Ready += botService.ReadyClientAsync;
             
            await commandHandler.InstallCommandsAsync();
            await interactionHandler.InitializeAsync();

            return ClientBot;
        }


        public async Task ConnectBotAsync(string tokenBot)
        {
            if (IsStarted) return;

            ClientBot ??= await CreateBot();

            await ClientBot.LoginAsync(TokenType.Bot, tokenBot);
            await ClientBot.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

    }
}
