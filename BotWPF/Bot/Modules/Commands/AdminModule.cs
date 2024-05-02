using Discord;
using Discord.Commands;

namespace BotWPF.Bot.Modules.Commands
{
    public class AdminModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task HandlePingCommand()
        {
            await Context.Message.ReplyAsync("Pong!", allowedMentions: AllowedMentions.None);
        }
    }
}
