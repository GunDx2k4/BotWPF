using BotWPF.Bot.Core;
using Discord;
using Discord.Interactions;

namespace BotWPF.Bot.Attributes
{
    public class BotPermissionAttribute : PreconditionAttribute
    {
        public GuildPermission? GuildPermission { get; }
        public ChannelPermission? ChannelPermission { get; }

        public BotPermissionAttribute(GuildPermission guildPermission)
        {
            GuildPermission = guildPermission;
        }

        public BotPermissionAttribute(ChannelPermission channelPermission)
        {
            ChannelPermission = channelPermission;
        }

        public override async Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context, ICommandInfo commandInfo, IServiceProvider services)
        {
            IGuildUser guildUser = null;
            if (context.Guild != null)
            {
                guildUser = await context.Guild.GetCurrentUserAsync().ConfigureAwait(continueOnCapturedContext: false);
            }

            if (GuildPermission.HasValue)
            {
                if (guildUser == null)
                {
                    return PreconditionResult.FromError("Lệnh phải được dùng trong kênh của server.");
                }

                if (!guildUser.GuildPermissions.Has(GuildPermission.Value))
                {
                    return PreconditionResult.FromError(ErrorMessage ?? $"Bot không có quyền {MarkdownDiscord.BoldText(MarkdownDiscord.HighlightText(GuildPermission.Value.ToString()))}.");
                }
            }

            if (ChannelPermission.HasValue && !((!(context.Channel is IGuildChannel channel)) ? ChannelPermissions.All(context.Channel) : guildUser.GetPermissions(channel)).Has(ChannelPermission.Value))
            {
                return PreconditionResult.FromError(ErrorMessage ?? $"Bot không có quyền truy cập kênh {MarkdownDiscord.BoldText(MarkdownDiscord.HighlightText(ChannelPermission.Value.ToString()))}.");
            }

            return PreconditionResult.FromSuccess();
        }
    }
}
