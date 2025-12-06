using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using UniversalBusinessSystem.Services;
using UniversalBusinessSystem.Core.Entities;

namespace UniversalBusinessSystem.Views
{
    public partial class CreateAccountWindow : Window
    {
        private readonly ShopConfigurationService _shopConfigService;
        private readonly AccountService _accountService;
        private ShopConfiguration? _selectedShopConfig;

        public CreateAccountWindow()
        {
            InitializeComponent();
            
            // Get services from DI container
            _shopConfigService = App.ServiceProvider.GetService<ShopConfigurationService>() 
                               ?? new ShopConfigurationService();
            _accountService = App.ServiceProvider.GetService<AccountService>() 
                            ?? new AccountService();
            
            InitializeShopTypes();
        }

        private void InitializeShopTypes()
        {
            var shopTypes = _shopConfigService.GetShopTypes();
            cmbShopType.ItemsSource = shopTypes;
            cmbShopType.DisplayMemberPath = "Name";
            cmbShopType.SelectedValuePath = "Name";
        }

        private void ShopType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbShopType.SelectedItem is ShopType selectedShopType)
            {
                _selectedShopConfig = _shopConfigService.GetShopConfiguration(selectedShopType.Name);
                UpdateShopConfigurationUI();
            }
        }

        private void UpdateShopConfigurationUI()
        {
            if (_selectedShopConfig == null) return;

            // Clear existing controls
            wpUnits.Children.Clear();
            wpCategories.Children.Clear();

            // Add units
            foreach (var unit in _selectedShopConfig.Units)
            {
                var checkBox = new CheckBox
                {
                    Content = $"{unit.Name} ({unit.Type})",
                    Margin = new Thickness(0, 0, 10, 5),
                    IsChecked = unit.IsDefault,
                    Tag = unit
                };
                wpUnits.Children.Add(checkBox);
            }

            // Add categories
            foreach (var category in _selectedShopConfig.Categories)
            {
                var checkBox = new CheckBox
                {
                    Content = category,
                    Margin = new Thickness(0, 0, 10, 5),
                    IsChecked = true
                };
                wpCategories.Children.Add(checkBox);
            }
        }

        private async void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtBusinessName.Text))
                {
                    MessageBox.Show("Please enter a business name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cmbShopType.SelectedItem == null)
                {
                    MessageBox.Show("Please select a shop type.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (txtPassword.Password != txtConfirmPassword.Password)
                {
                    MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Get selected units
                var selectedUnits = new List<Unit>();
                foreach (CheckBox checkBox in wpUnits.Children)
                {
                    if (checkBox.IsChecked == true && checkBox.Tag is Unit unit)
                    {
                        selectedUnits.Add(unit);
                    }
                }

                // Get selected categories
                var selectedCategories = new List<string>();
                foreach (CheckBox checkBox in wpCategories.Children)
                {
                    if (checkBox.IsChecked == true)
                    {
                        selectedCategories.Add(checkBox.Content.ToString());
                    }
                }

                // Create account request
                var request = new CreateAccountRequest
                {
                    BusinessName = txtBusinessName.Text,
                    ShopType = cmbShopType.SelectedValue.ToString(),
                    Address = txtAddress.Text,
                    ContactNumber = txtContactNumber.Text,
                    Username = txtUsername.Text,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text,
                    Password = txtPassword.Password,
                    SelectedUnits = selectedUnits,
                    SelectedCategories = selectedCategories
                };

                // Create account
                var result = await _accountService.CreateAccountAsync(request);

                if (result.Success)
                {
                    MessageBox.Show("Account created successfully! You can now login with your credentials.", 
                                  "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Failed to create account: {result.ErrorMessage}", 
                                  "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
