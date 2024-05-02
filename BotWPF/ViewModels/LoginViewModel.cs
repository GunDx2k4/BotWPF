using BotWPF.Base;
using System.Windows.Input;
using BotWPF.Bot.Core;
using Discord;

namespace BotWPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string TokenBot
        {
            get
            {
                return BotDiscord.gI().Token;
            }
            set
            {
                if (BotDiscord.gI().Token != value)
                {
                    BotDiscord.gI().Token = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<LoginViewModel>(CanLogin, OnLogin);
        }

        private bool CanLogin(object parameter)
        {
            if (parameter is LoginViewModel model)
            {
                return !string.IsNullOrEmpty(model.TokenBot);
            }
            return false;
        }

        private void OnLogin(object parameter)
        {
            if (parameter is LoginViewModel model)
            {
                try
                {
                    TokenUtils.ValidateToken(TokenType.Bot, model.TokenBot);
                } 
                catch(ArgumentException)
                {
                    DialogViewModel.Instance.OpenDialog($"Error token [{model.TokenBot}] is invalid.");
                    return;
                }

                BotDiscord.gI().ConnectBotAsync(TokenBot).GetAwaiter();
                DialogViewModel.Instance.OpenDialog($"Watting for login ...", false);
            }
        }
    }
}
