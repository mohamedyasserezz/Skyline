using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Skyline.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyline.Models
{
	public class Employee
	{

		public int Id { get; set; }

		[Required(ErrorMessage = "Enter a valid Full name!")]
		[MinLength(8, ErrorMessage = "Full Name can't be less than 8 Characters")]
		[MaxLength(50, ErrorMessage = "Full Name can't be more than 50 Characters")]
		[Display(Name = "Full Name")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "Enter a valid National Id")]
		[MinLength(8, ErrorMessage = "Full Name can't be less than 14 Characters")]
		[MaxLength(50, ErrorMessage = "Full Name can't be more than 14 Characters")]
		[Display(Name = "National Id")]
		public string NationalId { get; set; }


		[Required(ErrorMessage = "Enter a valid Position!")]
		[MinLength(8, ErrorMessage = "Full Name can't be less than 2 Characters")]
		[MaxLength(50, ErrorMessage = "Full Name can't be more than 20 Characters")]
		[DisplayName("Occupation")]
		public string Position { get; set; }

		[Range(6000, 60000, ErrorMessage = "Salary must be between 6000 EGP and 60000 EGP")]
		public decimal Salary { get; set; }

		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }
		[DisplayName("Hiring Date And Time")]
		public DateTime HiringDateTime { get; set; }
		[DataType(DataType.Time)]
		[DisplayName("Attendence Time")]
		public DateTime AttendenceTime { get; set; }
		[DisplayName("Phone No.")]
		[RegularExpression("^01\\d{9}$", ErrorMessage = "Invalid Phone No.")]
		public string PhoneNumber { get; set; }
		[DisplayName("Email Address")]
		[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Invalid Email Address")]
		public string EmailAddress { get; set; }
		[DisplayName("Confirm Email Address")]
		[NotMapped]
		[Compare("EmailAddress", ErrorMessage = "Email and Confirm Email not match.")]
		public string ConfirmEmailAddress { get; set; }
		[DisplayName("Secret Code")]
		[MinLength(4, ErrorMessage = "Secret code can't be less than 4")]
		[DataType(DataType.Password)]
		public string SecretCode { get; set; }
		[DisplayName("Confirm Secret Code")]
		[NotMapped]
		[Compare("SecretCode", ErrorMessage = "Secret Code and Confirm Secret Code not match.")]
		[DataType(DataType.Password)]
		public string ConfirmSecretCode { get; set; }

		[Range(0, 100, ErrorMessage = "Appraisal must be between 0 to 100")]
		public byte Appraisal { get; set; }

		[DisplayName("Is Active")]
		public bool IsActive { get; set; }

		[ValidateNever]
		public string? Notes { get; set; }

		[DisplayName("Department")]
		[Range(1, double.MaxValue, ErrorMessage = "Choose a valid Department")]
		public int DepartmentId { get; set; }
		[ValidateNever]
		public Department Department { get; set; }

		[ValidateNever]
		public string ImagePath { get; set; }

		[DisplayName("Image")]
		[ValidateNever]
		[NotMapped]
		public IFormFile ImageFile { get; set; }
	}
}
