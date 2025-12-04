using System.Windows.Controls;
using System.Windows;
using UniversalBusinessSystem.ViewModels;

namespace UniversalBusinessSystem.Views;

public partial class ModuleManagementView : UserControl
{
    public ModuleManagementView()
    {
        InitializeComponent();
        // Resolve the ViewModel via DI so required services are injected
        try
        {
            DataContext = App.GetService<ModuleManagementViewModel>();
        }
        catch
        {
            // Fallback: do nothing, leave DataContext null to allow design-time rendering
        }
    }
}
