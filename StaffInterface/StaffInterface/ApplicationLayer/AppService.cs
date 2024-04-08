using Microsoft.Win32;
using StaffInterface.Core.Abstractions;
using StaffInterface.Core.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace StaffInterface.ApplicationLayer
{
    public class AppService : IAppService
    {
        public string UpdateOrganizationsCollection(
            ObservableCollection<Organization> OrganizationGridCollection,
            IBaseApiAccess _apiAccess,
            ObservableCollection<string>? OrganizationBoxCollection = null
            )
        {
            string error = string.Empty;
            OrganizationGridCollection.Clear();
            if( OrganizationBoxCollection != null )
            {
                OrganizationBoxCollection.Clear();
                OrganizationBoxCollection.Add("Все");
            }

            (List<Organization>? organizations, error) = _apiAccess.GetOrganizations();
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            foreach (Organization org in organizations)
            {
                OrganizationGridCollection.Add(org);
                if (OrganizationBoxCollection != null)
                {
                    OrganizationBoxCollection.Add(org.Name);
                }
            }
            return error;
        }

        public string UpdateEmloyeeCollection(
                ObservableCollection<Employee> EmployeeGridCollection,
                IBaseApiAccess _apiAccess,
                Guid? organizationId = null
            )
        {
            string error = string.Empty;
            List<Employee>? employee;
            EmployeeGridCollection.Clear();
            if(organizationId is null)
            {
                (employee, error) = _apiAccess.GetEmployee();
            }
            else
            {
                (employee, error) = _apiAccess.GetEmployeeByOrganizationId((Guid)organizationId);
            }

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            foreach (Employee emp in employee)
            {
                EmployeeGridCollection.Add(emp);
            }
            return error;
        }

        public (Guid id, string error) AddEmployeeToBase(Employee employee, IBaseApiAccess _apiAccess)
        {
            return _apiAccess.AddEmployee(employee);
        }

        public string AddEmployeeToBaseFromFile(IBaseApiAccess _apiAccess)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Выберите файл";
            dialog.Filter = "Csv файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";

            bool? result = dialog.ShowDialog();
            if(result is not true)
            {
                return "Select valid path";
            }
            string filename = dialog.FileName;
            return _apiAccess.AddEmployeeCsv(filename);
        }

        public string SaveEmployeeToFile(IBaseApiAccess _apiAccess, Guid? organizationId = null)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Выберите место сохранения файла";
            dialog.Filter = "Csv файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
            dialog.FileName = "Employee.csv";
            bool? result = dialog.ShowDialog();
            if(result is not true)
            {
                return "Invalid path";
            }
            string filename = dialog.FileName;
            if(organizationId is null)
            {
                return _apiAccess.GetEmployeeCsv(filename);
            }
            else
            {
                return _apiAccess.GetEmployeeByOrganizationIdCsv(filename, (Guid)organizationId);
            }
        }

        public (Guid id, string error) AddOrganizationToBase(Organization organization, IBaseApiAccess _apiAccess)
        {
            return _apiAccess.AddOrganization(organization);
        }

        public string AddOrganizationToBaseFromFile(IBaseApiAccess _apiAccess)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Выберите файл";
            dialog.Filter = "Csv файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";

            bool? result = dialog.ShowDialog();
            if (result is not true)
            {
                return "Select valid path";
            }
            string filename = dialog.FileName;
            return _apiAccess.AddOrganizationsCsv(filename);
        }

        public string SaveOrganizationsToFile(IBaseApiAccess _apiAccess)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Выберите место сохранения файла";
            dialog.Filter = "Csv файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
            dialog.FileName = "Organizations.csv";
            bool? result = dialog.ShowDialog();
            if (result is not true)
            {
                return "Invalid path";
            }
            string filename = dialog.FileName;
            return _apiAccess.GetOrganizationsCsv(filename);
        }
    }
}
