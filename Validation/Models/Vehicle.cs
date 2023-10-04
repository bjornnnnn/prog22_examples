using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Validation.Validations;

namespace Validation.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Remote(action: "VerifyLicencePlate", controller: "Vehicles", ErrorMessage = "Licence plate already registered")]
        public string LicencePlate { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        [StringLength(10, MinimumLength = 2)]
        public string Brand { get; set; } = string.Empty;
        public string OwnerFirstName { get; set; } = string.Empty;

        [KalleAnka]
        public string OwnerLastName { get; set; } = string.Empty;

        [EmailAddress]
        public string OwnerEmail { get; set; } = string.Empty;
    }
}
