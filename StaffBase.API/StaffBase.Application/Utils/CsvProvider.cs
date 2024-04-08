using StaffBase.Core.Models;
using System.Collections.Generic;
using System.Text;

namespace StaffBase.Application.Utils
{
    public class CsvProvider : ICsvProvider
    {
        public string OrganizationToCsv(List<Organization> organizationsList)
        {
            StringBuilder csvBuilder = new();
            csvBuilder.AppendLine("Id,Name,Inn,LegalAddress,ActualAddress");
            foreach (var item in organizationsList)
            {
                csvBuilder.AppendLine($"\"{item.Id}\",\"{item.Name}\",\"{item.Inn}\",\"{item.LegalAddress}\",\"{item.ActualAddress}\"");
            }
            return csvBuilder.ToString();
        }

        public string EmployeeToCsv(List<Employee> employeeList)
        {
            StringBuilder csvBuilder = new();
            csvBuilder.AppendLine("Id,Lastname,Firstname,Patronomic,Birthdate,PassportSeries,PassportNumber,OrganizationId");
            foreach (var item in employeeList)
            {
                csvBuilder.AppendLine($"\"{item.Id}\",\"{item.Lastname}\",\"{item.Firstname}\",\"{item.Patronomic}\",\"{item.Birthdate}\",\"{item.PassportSeries}\",\"{item.PassportNumber}\",\"{item.OrganizationId}\"");
            }
            return csvBuilder.ToString();
        }

        (List<Employee> employee, string error) ICsvProvider.CsvToEmployee(string csv)
        {
            string error = string.Empty;
            List<Employee> employee = new();
            string[] csvEmployeeList = csv.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for(int i=1; i < csvEmployeeList.Length; i++)
            {
                string[] csvEmployeeFields = csvEmployeeList[i].Split("\",\"");
                csvEmployeeFields = csvEmployeeFields.Select(x => x.Replace("\"", "")).ToArray();
                if (csvEmployeeFields.Length == 8)
                {
                    Guid id;
                    Guid organizationId;
                    Employee emp;
                    DateTime birthday;
                    if (!Guid.TryParse(csvEmployeeFields[0], out id))
                    {
                        error = $"Bad id format in stroke {i}.";
                        return (employee, error);
                    }
                    if (!Guid.TryParse(csvEmployeeFields[7], out organizationId))
                    {
                        error = $"Bad organization id in stroke {i}.";
                        return (employee, error);
                    }
                    if (!DateTime.TryParse(csvEmployeeFields[4], out birthday))
                    {
                        error = $"Bad birthday format in stroke {i}.";
                        return (employee, error);
                    }

                    (emp, error) = Employee.Create(
                        id,
                        csvEmployeeFields[1],
                        csvEmployeeFields[2],
                        csvEmployeeFields[3],
                        birthday,
                        csvEmployeeFields[5],
                        csvEmployeeFields[6],
                        organizationId
                        );
                    if (!string.IsNullOrEmpty(error))
                    {
                        return (employee, error);
                    }
                    
                    employee.Add(emp);
                }
                else
                {
                    error = $"Bad stroke {i} format.";
                    return (employee, error);
                }
            }
            return (employee, error);
        }

        (List<Organization>, string error) ICsvProvider.CsvToOrganization(string csv)
        {
            string error = string.Empty;
            List<Organization> organizations = new();
            string[] csvOrganizationsList = csv.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < csvOrganizationsList.Length; i++)
            {
                string[] csvOrganizationsFields = csvOrganizationsList[i].Split("\",\"");
                csvOrganizationsFields = csvOrganizationsFields.Select(x => x.Replace("\"", "")).ToArray();
                if (csvOrganizationsFields.Length == 5) 
                {
                    Guid id;
                    Organization org;
                    if (!Guid.TryParse(csvOrganizationsFields[0], out id))
                    {
                        error = $"Bad id format in stroke {i}.";
                        return (organizations, error);
                    }
                    (org, error) = Organization.Create(
                        id,
                        csvOrganizationsFields[1],
                        csvOrganizationsFields[2],
                        csvOrganizationsFields[3],
                        csvOrganizationsFields[4]
                        );
                    if (!string.IsNullOrEmpty(error))
                    {
                        return (organizations, error);
                    }
                    organizations.Add(org);
                }
                else
                {
                    error = $"Bad stroke {i} format.";
                    return (organizations, error);
                }
            }
            return (organizations, error);
        }
    }
}
