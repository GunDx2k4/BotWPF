using BotWPF.Bot.Attributes;
using Discord;
using Discord.Interactions;

namespace BotWPF.Bot.Modules.Interactions
{
    [BotPermission(GuildPermission.Administrator)]
    [UserPermission(GuildPermission.Administrator)]
    public class AdminModule : InteractionModuleBase<SocketInteractionContext>
    {

        [SlashCommand("ping", "Test Bot")]
        public async Task HandlePingCommand()
        {
            await RespondAsync($"Pong!");
        }
    }
}
