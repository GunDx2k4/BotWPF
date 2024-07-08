using BotWPF.Base;
using BotWPF.Bot.Core;
using Discord.WebSocket;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BotWPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string UrlAvatar => BotDiscord.gI().ClientBot.CurrentUser.GetAvatarUrl();
        public List<SocketGuild> ListGuilds => BotDiscord.gI().ClientBot.Guilds.ToList();

        public MainViewModel()
        {
        }
    }
}
