using BotWPF.Properties;
using Discord.Interactions;
using Discord;
using BotWPF.Bot.Core;

namespace BotWPF.Bot.Attributes
{
    public class UserPermissionAttribute : PreconditionAttribute
    {
        public GuildPermission? GuildPermission { get; }
        public ChannelPermission? ChannelPermission { get; }

        public UserPermissionAttribute(GuildPermission guildPermission)
        {
            GuildPermission = guildPermission;
        }

        public UserPermissionAttribute(ChannelPermission channelPermission)
        {
            ChannelPermission = channelPermission;
        }

        public override async Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context, ICommandInfo commandInfo, IServiceProvider services)
        {
            IGuildUser guildUser = context.User as IGuildUser;
            if (GuildPermission.HasValue)
            {
                if (guildUser == null)
                {
                    return PreconditionResult.FromError("Lệnh phải được dùng trong kênh của server.");
                }

                if (!guildUser.GuildPermissions.Has(GuildPermission.Value))
                {
                    return PreconditionResult.FromError(ErrorMessage ?? $"Bạn không có quyền {MarkdownDiscord.BoldText(MarkdownDiscord.HighlightText(GuildPermission.Value.ToString()))}.");
                }
            }

            if (ChannelPermission.HasValue && !((!(context.Channel is IGuildChannel channel)) ? ChannelPermissions.All(context.Channel) : guildUser.GetPermissions(channel)).Has(ChannelPermission.Value))
            {
                return PreconditionResult.FromError(ErrorMessage ?? $"Bạn không có quyền truy cập kênh {MarkdownDiscord.BoldText(MarkdownDiscord.HighlightText(ChannelPermission.Value.ToString()))}.");
            }

            return PreconditionResult.FromSuccess();
        }
    }
}
