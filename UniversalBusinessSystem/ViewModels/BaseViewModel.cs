using System;
using System.Windows.Input;
using UniversalBusinessSystem.Services;

namespace UniversalBusinessSystem.ViewModels
{
    public abstract class BaseViewModel
    {
        protected readonly INavigationService _navigationService;
        
        public ICommand GoBackCommand { get; }
        public ICommand GoHomeCommand { get; }
        
        protected BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
            GoBackCommand = new RelayCommand(GoBack, CanGoBack);
            GoHomeCommand = new RelayCommand(GoHome);
        }
        
        private void GoBack()
        {
            _navigationService.GoBack();
        }
        
        private bool CanGoBack()
        {
            return _navigationService.CanGoBack;
        }
        
        private void GoHome()
        {
            _navigationService.NavigateTo(typeof(DashboardViewModel));
        }
    }
    
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        
        public event EventHandler CanExecuteChanged;
        
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        
        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;
        
        public void Execute(object parameter) => _execute();
        
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
