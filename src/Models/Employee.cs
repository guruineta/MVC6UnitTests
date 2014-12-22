using System.ComponentModel.DataAnnotations;
using System;
namespace Models
{
	public class Employee
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Designation { get; set; }
		[Required]
		[DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format.")]
		public DateTime JoiningDate { get; set; }
		public string Remarks { get; set; }
	}
}