using BotWPF.Base;
using Discord;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BotWPF.ViewModels
{
    public class DialogViewModel : BaseViewModel
    {
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isOpenDialog;

        public bool IsOpenDialog
        {
            get
            {
                return _isOpenDialog;
            }
            set
            {
                if (_isOpenDialog != value)
                {
                    _isOpenDialog = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isCanClose; 
        
        public bool IsCanClose
        {
            get
            {
                return _isCanClose;
            }
            set
            {
                if (_isCanClose != value)
                {
                    _isCanClose = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand CloseDialogCommnad { get; set; }

        private static DialogViewModel _instance;

        public static DialogViewModel Instance => _instance ??= new DialogViewModel();

        public DialogViewModel()
        {
            CloseDialogCommnad = new RelayCommand<DialogViewModel>(CanClose, OnClose);
        }

        private bool CanClose(object parameter)
        {
            if (parameter is DialogViewModel model)
            {
                return model.IsCanClose;
            }
            return false;
        }

        private void OnClose(object parameter)
        {
            if (parameter is DialogViewModel model)
            {
                model.IsOpenDialog = false;
                return;
            }
        }

        public void CloseDialog()
        {
            IsOpenDialog = false;
            Message = string.Empty;
            IsCanClose = true;
        }

        public void OpenDialog(string message,bool isCanClose = true)
        {
            IsOpenDialog = true;
            Message = message;
            IsCanClose = isCanClose;
        }


    }
}
