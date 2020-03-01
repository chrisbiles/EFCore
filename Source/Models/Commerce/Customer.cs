using System;
using Models.Interfaces.Commerce;

namespace Models.Commerce
{
	public class Customer : ICustomer
	{
		public DateTime Created { get; set; }
		public DateTime LastModifiedDateTime { get; set; }
		public Guid Id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NameConcatenation => $"{FirstName} {LastName}";

		public string EmailAddress { get; set; }
		public bool IsActive { get; set; }
	}
}