using BotWPF.Base;
using BotWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace BotWPF.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel => _mainView.CurrentViewModel;

        public ControlBarViewModel ControlBarViewModel { get; }

        public DialogViewModel DialogViewModel { get; }

        private readonly NavigationMainView _mainView;

        public ICommand SizeChangedCommand { get; set; }

        public MainWindowViewModel()
        {
            SizeChangedCommand = new RelayCommand<Window>((p) =>
            {
                return p is Window;
            }, (p) =>
            {
                double newWidth = p.ActualWidth;
                double newHeight = p.ActualHeight;

                double left = (SystemParameters.PrimaryScreenWidth - newWidth) / 2;
                double top = (SystemParameters.PrimaryScreenHeight - newHeight) / 2;

                p.Left = left;
                p.Top = top;
            });
            ControlBarViewModel = new ControlBarViewModel();
            DialogViewModel = DialogViewModel.Instance;

            _mainView = NavigationMainView.Instance;
            _mainView.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
