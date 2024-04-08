using StaffBase.Core.Utils;
using System.Runtime.InteropServices;

namespace StaffBase.Core.Models
{
    public class Organization
    {
        public const int NAME_MAX_SIZE = 100;
        public const int INN_MAX_SIZE = 10;
        public const int ADDRESS_SIZE = 500;
        private Organization(
            Guid id, 
            string name, 
            string inn, 
            string legalAddress, 
            string actualAddress)
        {
            Id = id;
            Name = name;
            Inn = inn;
            LegalAddress = legalAddress;
            ActualAddress = actualAddress;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Inn { get; }
        public string LegalAddress { get; }
        public string ActualAddress { get; }

        public static (Organization? organization, string error) Create(
            Guid? id,
            string name,
            string inn,
            string legalAddress,
            string actualAddress)
        {
            string error = string.Empty;
            Organization? organization = null;
            if (id is null)
            {
                error = "wrong id.";
                return (organization, error);
            }
            bool check;
            (check, error) = Check.TextFields(name, NAME_MAX_SIZE);
            if (!check)
            {
                return (organization, error);
            }
            (check, error) = Check.TextFields(inn, INN_MAX_SIZE);
            if (!check)
            {
                return (organization, error);
            }
            (check, error) = Check.AddressFields(legalAddress, ADDRESS_SIZE);
            if (!check)
            {
                return (organization, error);
            }
            (check, error) = Check.AddressFields(actualAddress, ADDRESS_SIZE);
            if (!check)
            {
                return (organization, error);
            }

            organization = new((Guid)id, name, inn, legalAddress, actualAddress);
            return (organization, error);
        }
    }
}
