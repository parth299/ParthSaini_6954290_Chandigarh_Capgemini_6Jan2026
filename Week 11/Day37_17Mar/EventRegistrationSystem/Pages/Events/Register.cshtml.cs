using System.Collections.Generic;
using EventRegistrationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class RegisterModel : PageModel
{
	// Temporary data storage (no database)
	public static List<EventRegistration> registrations = new List<EventRegistration>();

	[BindProperty]
	public EventRegistration Registration { get; set; }

	public void OnGet()
	{
	}

	public IActionResult OnPost()
	{
		Registration.Id = registrations.Count + 1;
		registrations.Add(Registration);

		return RedirectToPage("Index");
	}
}