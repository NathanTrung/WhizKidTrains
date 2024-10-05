using System.Text.RegularExpressions;

/// <summary>
/// Store validation rules as methods
/// </summary>
public static class ValidationService
{
    public static bool IsValidEmail(string email)
    {
        string pat = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex r = new Regex(pat, RegexOptions.IgnoreCase);
        Match m = r.Match(email);
        return m.Success;
    }
    public static bool IsValidPassword(string password) => password.Length > 8;

}
