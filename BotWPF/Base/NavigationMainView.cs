using BotWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWPF.Base
{
    public class NavigationMainView
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        private static NavigationMainView _instance;

        public static NavigationMainView Instance => _instance ??= new NavigationMainView();

        public NavigationMainView()
        {
            createLoginView();
        }

        public void createLoginView()
        {
            CurrentViewModel = new LoginViewModel();
        }

        public void createMainView()
        {
            CurrentViewModel = new MainViewModel();
        }
    }
}
