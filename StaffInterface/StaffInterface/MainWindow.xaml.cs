using StaffInterface.ApplicationLayer;
using StaffInterface.ApplicationLayer.Contracts;
using StaffInterface.Core.Abstractions;
using StaffInterface.Core.Models;
using StaffInterface.Infrastructure;
using StaffInterface.Infrastructure.Contracts;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StaffInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Organization> OrganizationGridCollection { get; set; }
        public ObservableCollection<Employee> EmployeeGridCollection { get; set; }
        public ObservableCollection<string> OrganizationBoxCollection { get; set; }
        protected IAppService _appService;
        protected IBaseApiAccess _apiAccess;
        IHttpIO _IhttpIO;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            OrganizationGridCollection = new();
            EmployeeGridCollection = new();
            OrganizationBoxCollection = new();
            OrganizationsComboBox.ItemsSource = OrganizationBoxCollection;

            AppSettingsProvider settings = new AppSettingsProvider();
            _IhttpIO = new HttpIO();
            _apiAccess = new BaseApiAccess(settings.appSettings.APIHost, _IhttpIO);
            _appService = new AppService();
            _appService.UpdateOrganizationsCollection(OrganizationGridCollection, _apiAccess, OrganizationBoxCollection);
            _appService.UpdateEmloyeeCollection(EmployeeGridCollection, _apiAccess);
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            string? selectedOrganization = OrganizationsComboBox.SelectedItem as string;
            if((selectedOrganization is null) || (selectedOrganization=="Все"))
            {
                MessageBox.Show("Выберите организацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Guid selectedOrganizationId = OrganizationGridCollection.Where(e => e.Name == selectedOrganization).ToList()[0].Id;

            (Employee? employee, string error) = Employee.Create(
                Guid.NewGuid(),
                NameEmployeeText.Text,
                LastnameEmployeeText.Text,
                PatronomicEmployeeText.Text,
                DateOfBirthPicker.SelectedDate,
                PassportSeriesText.Text,
                PassportNumberText.Text,
                selectedOrganizationId
                );
            if(!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            (Guid newUserId, error) = _appService.AddEmployeeToBase(employee, _apiAccess);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            error = _appService.UpdateEmloyeeCollection(EmployeeGridCollection,_apiAccess, selectedOrganizationId);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void AddOrganizationBtn_Click(object sender, RoutedEventArgs e)
        {
            (Organization? organization, string error) = Organization.Create(
                Guid.NewGuid(),
                NameOrganizationText.Text,
                InnOrganizationText.Text,
                LegalAddressOrganizationText.Text,
                ActualAddressOrganizationText.Text
            );
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            (Guid newOrganizationId,error) = _appService.AddOrganizationToBase(organization, _apiAccess);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            error = _appService.UpdateOrganizationsCollection(OrganizationGridCollection,_apiAccess,OrganizationBoxCollection);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void OrganizationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string error = string.Empty;
            string? selectedOrganization = OrganizationsComboBox.SelectedItem as string;
            if ((selectedOrganization is null) || (selectedOrganization == "Все"))
            {
                error = _appService.UpdateEmloyeeCollection(EmployeeGridCollection, _apiAccess);
            }
            else
            {
                Guid selectedOrganizationId = OrganizationGridCollection.Where(e => e.Name == selectedOrganization).ToList()[0].Id;
                error = _appService.UpdateEmloyeeCollection(EmployeeGridCollection, _apiAccess, selectedOrganizationId);
            }
            if (!string.IsNullOrEmpty(error))
            {
                //MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void SaveEmployeeCsvBtn_Click(object sender, RoutedEventArgs e)
        {
            string error = string.Empty;
            string? selectedOrganization = OrganizationsComboBox.SelectedItem as string;
            if ((selectedOrganization is null) || (selectedOrganization == "Все"))
            {
                error = _appService.SaveEmployeeToFile(_apiAccess);
            }
            else
            {
                Guid selectedOrganizationId = OrganizationGridCollection.Where(e => e.Name == selectedOrganization).ToList()[0].Id;
                error = _appService.SaveEmployeeToFile(_apiAccess, selectedOrganizationId);
            }
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"Файл сохранен.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveOrganizationCsvBtn_Click(object sender, RoutedEventArgs e)
        {
            string error = _appService.SaveOrganizationsToFile(_apiAccess);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"Файл сохранен.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddEmployeeCsvBtn_Click(object sender, RoutedEventArgs e)
        {
            string? selectedOrganization = OrganizationsComboBox.SelectedItem as string;
            string error = _appService.AddEmployeeToBaseFromFile(_apiAccess);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((selectedOrganization is null) || (selectedOrganization == "Все"))
            {
                error = _appService.UpdateEmloyeeCollection(EmployeeGridCollection, _apiAccess);
            }
            else
            {
                Guid selectedOrganizationId = OrganizationGridCollection.Where(e => e.Name == selectedOrganization).ToList()[0].Id;
                error = _appService.UpdateEmloyeeCollection(EmployeeGridCollection, _apiAccess, selectedOrganizationId);
            }
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"Сотрудники из файла загружены.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddOrganizationCsvBtn_Click(object sender, RoutedEventArgs e)
        {
            string error = _appService.AddOrganizationToBaseFromFile(_apiAccess);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            error = _appService.UpdateOrganizationsCollection(OrganizationGridCollection, _apiAccess, OrganizationBoxCollection);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"Организации из файла загружены.", "Выполнено!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _IhttpIO.Dispose();
        }
    }
}