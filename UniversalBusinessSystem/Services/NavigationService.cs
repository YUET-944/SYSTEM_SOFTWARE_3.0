using System;
using System.Windows.Controls;
using UniversalBusinessSystem.ViewModels;

namespace UniversalBusinessSystem.Services
{
    public interface INavigationService
    {
        void NavigateTo(Type viewModelType);
        void GoBack();
        bool CanGoBack { get; }
    }

    public class NavigationService : INavigationService
    {
        private readonly Frame _mainFrame;
        
        public NavigationService(Frame mainFrame)
        {
            _mainFrame = mainFrame;
        }
        
        public bool CanGoBack => _mainFrame.CanGoBack;
        
        public void NavigateTo(Type viewModelType)
        {
            var viewModel = App.GetService(viewModelType);
            _mainFrame.Navigate(viewModel);
        }
        
        public void GoBack()
        {
            if (_mainFrame.CanGoBack)
            {
                _mainFrame.GoBack();
            }
        }
    }
}
