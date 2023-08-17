using System.ComponentModel.DataAnnotations;

public class AuthorizationVM
{
    [Required(ErrorMessage = "Enter the email")]
    [EmailAddress(ErrorMessage = "Invalid input")]
    public string? email {get; set;}
    [Required(ErrorMessage = "Enter the password")]
    [MinLength(3, ErrorMessage = "Password length must be more than 2 characters")]
    public string? password {get; set;}
    public bool invalid {get; set;} = false;
    public bool exists = true;
}