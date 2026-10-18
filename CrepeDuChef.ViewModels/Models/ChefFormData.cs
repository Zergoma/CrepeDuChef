namespace CrepeDuChef.ViewModels.Models
{
    /// <summary>
    /// Names entered in the chef form; the caller decides whether it is an addition or an update.
    /// </summary>
    public sealed record ChefFormData(string FirstName, string LastName);
}
