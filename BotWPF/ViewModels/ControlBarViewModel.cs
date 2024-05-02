using BotWPF.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BotWPF.ViewModels
{
    public class ControlBarViewModel : BaseViewModel
    {
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }

        public ControlBarViewModel()
        {
            MinimizeWindowCommand = new RelayCommand<UserControl>((p) =>
            {
                return p is UserControl;
            }, (p) =>
            {
                var w = Window.GetWindow(p);
                if (w != null)
                {
                    if (w.WindowState != WindowState.Minimized)
                    {
                        w.WindowState = WindowState.Minimized;
                    }    
                    else
                        w.WindowState = WindowState.Maximized;
                }
            });

            CloseWindowCommand = new RelayCommand<UserControl>((p) =>
            {
                return p is UserControl;
            }, (p) =>
            {
                var w = Window.GetWindow(p);
                if (w != null)
                {
                    w.Close();
                }
            });

            MouseMoveWindowCommand = new RelayCommand<UserControl>((p) =>
            {
                return p is UserControl;
            }, (p) =>
            {
                var w = Window.GetWindow(p);
                if (w != null)
                {
                    w.DragMove();
                }
            }
           );
        }
    }
    
}
