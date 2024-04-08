using Newtonsoft.Json;
using StaffInterface.ApplicationLayer.Contracts;
using StaffInterface.Core.Abstractions;
using StaffInterface.Core.Models;
using StaffInterface.Infrastructure.Contracts;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
namespace StaffInterface.Infrastructure
{
    public class BaseApiAccess : IBaseApiAccess
    {
        protected readonly IHttpIO _httpIO;
        protected readonly string _apiUrl;
        public BaseApiAccess(string apiUrl, IHttpIO httpIO)
        {
            if (string.IsNullOrEmpty(apiUrl))
            {
                throw new ArgumentNullException(nameof(apiUrl));
            }
            _apiUrl = apiUrl;
            _httpIO = httpIO;
        }

        public (List<Organization>? organizations, string error) GetOrganizations()
        {
            List<OrganizationResponse>? organizationsResponse = new();
            List<Organization>? organizations = new();
            string error = string.Empty;
            string url = $"{_apiUrl}/api/StaffBase/Organizations";
            (HttpResponseMessage response, error) = _httpIO.SendGet(url);
            if(!string.IsNullOrEmpty(error))
            {
                return (organizations, error);
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            organizationsResponse = JsonConvert.DeserializeObject<List<OrganizationResponse>>(responseBody);
            organizations = organizationsResponse.Select(e => Organization.Create(e.Id, e.Name, e.Inn, e.LegalAddress, e.ActualAddress).organization).ToList();
            return (organizations, error);
        }

        public (List<Employee> employee, string error) GetEmployee()
        {
            List<EmployeeResponse>? employeeResponse = new();
            List<Employee>? employee = new();
            string error = string.Empty;
            string url = $"{_apiUrl}/api/StaffBase/Employee";
            (HttpResponseMessage response, error) = _httpIO.SendGet(url);
            if (!string.IsNullOrEmpty(error))
            {
                return (employee, error);
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            employeeResponse = JsonConvert.DeserializeObject<List<EmployeeResponse>>(responseBody);
            employee = employeeResponse.Select(e => Employee.Create(e.Id, e.Lastname, e.Firstname, e.Patronomic, e.Birthdate, e.PassportSeries, e.PassportNumber, e.OrganizationId).employee).ToList();
            return (employee, error);
        }

        public (List<Employee> employee, string error) GetEmployeeByOrganizationId(Guid id)
        {
            List<EmployeeResponse>? employeeResponse = new();
            List<Employee>? employee = new();
            string error = string.Empty;
            string url = $"{_apiUrl}/api/StaffBase/Employee/{id}";
            (HttpResponseMessage response, error) = _httpIO.SendGet(url);
            if (!string.IsNullOrEmpty(error))
            {
                return (employee, error);
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            employeeResponse = JsonConvert.DeserializeObject<List<EmployeeResponse>>(responseBody);
            employee = employeeResponse.Select(e => Employee.Create(e.Id, e.Lastname, e.Firstname, e.Patronomic, e.Birthdate, e.PassportSeries, e.PassportNumber, e.OrganizationId).employee).ToList();
            return (employee, error);
        }

        public (Guid organizationId, string error) AddOrganization(Organization organization)
        {
            OrganizationRequest organizationRequest = new(
                organization.Name,
                organization.Inn,
                organization.LegalAddress,
                organization.ActualAddress
            );
            string error = string.Empty;
            string url = $"{_apiUrl}/api/StaffBase/Organizations";
            Guid organizationId = new();
            string jsonRequest = JsonConvert.SerializeObject(organizationRequest);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            (HttpResponseMessage response, error) = _httpIO.SendPost(url, content);
            if (!string.IsNullOrEmpty(error))
            {
                return (organizationId, error);
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            organizationId = JsonConvert.DeserializeObject<Guid>(responseBody);
            return (organizationId, error);
        }
        public (Guid employeeId, string error) AddEmployee(Employee employee)
        {
            EmployeeRequest employeeRequest = new(
                employee.Lastname, 
                employee.Firstname, 
                employee.Patronomic, 
                employee.Birthdate, 
                employee.PassportSeries, 
                employee.PassportNumber, 
                employee.OrganizationId
            );
            string error = string.Empty;
            string url = $"{_apiUrl}/api/StaffBase/Employee";
            Guid employeeId = new();
            string jsonRequest = JsonConvert.SerializeObject(employeeRequest);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            (HttpResponseMessage response, error) = _httpIO.SendPost(url, content);
            if (!string.IsNullOrEmpty(error))
            {
                return (employeeId, error);
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            employeeId = JsonConvert.DeserializeObject<Guid>(responseBody);
            return (employeeId, error);
        }

        public string AddOrganizationsCsv(string pathToFile)
        {
            string error = string.Empty;
            string url = $"{_apiUrl}/api/StaffBase/OrganizationsCsv";
            byte[] organizationsFileContent = File.ReadAllBytes(pathToFile);
            using (var form = new MultipartFormDataContent())
            {
                using (var content = new ByteArrayContent(organizationsFileContent))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");
                    content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "file",
                        FileName = "file.csv"
                    };

                    form.Add(content);
                    (HttpResponseMessage response, error) = _httpIO.SendPost(url, form);
                    return error;
                }
            }
        }

        public string AddEmployeeCsv(string pathToFile)
        {
            string error = string.Empty;
            string url = $"{_apiUrl}/api/StaffBase/EmployeeCsv";
            byte[] employeeFileContent = File.ReadAllBytes(pathToFile);
            using(var form = new MultipartFormDataContent())
            {
                using (var content = new ByteArrayContent(employeeFileContent))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");
                    content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "file",
                        FileName = "file.csv"
                    };

                    form.Add(content);
                    (HttpResponseMessage response, error) = _httpIO.SendPost(url, form);
                    return error;
                }
            }
        }

        public string GetOrganizationsCsv(string pathToFile)
        {
            string error = string.Empty;
            string url = $"{_apiUrl}/api/StaffBase/Organizations.csv";
            (HttpResponseMessage response, error) = _httpIO.SendGet(url);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            byte[] csvContent = response.Content.ReadAsByteArrayAsync().Result;
            File.WriteAllBytes(pathToFile, csvContent);
            return error;
        }

        public string GetEmployeeCsv(string pathToFile)
        {
            string error = string.Empty;
            string url = $"{_apiUrl}/api/StaffBase/Employee.csv";
            (HttpResponseMessage response, error) = _httpIO.SendGet(url);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            byte[] csvContent = response.Content.ReadAsByteArrayAsync().Result;
            File.WriteAllBytes(pathToFile, csvContent);
            return error;
        }

        public string GetEmployeeByOrganizationIdCsv(string pathToFile, Guid organizationId)
        {
            string error = string.Empty;
            string url = $"{_apiUrl}/api/StaffBase/Employee.csv/{organizationId}";
            (HttpResponseMessage response, error) = _httpIO.SendGet(url);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            byte[] csvContent = response.Content.ReadAsByteArrayAsync().Result;
            File.WriteAllBytes(pathToFile, csvContent);
            return error;
        }
    }
}
