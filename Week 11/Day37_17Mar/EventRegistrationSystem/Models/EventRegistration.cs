using System.ComponentModel.DataAnnotations;

namespace EventRegistrationSystem.Models
{
    public class EventRegistration
    {
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string ParticipantName { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required]
		public string EventName { get; set; }
	}
}
