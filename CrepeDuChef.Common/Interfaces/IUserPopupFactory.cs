namespace CrepeDuChef.Common.Interfaces
{
    public interface IUserPopupFactory<T,P>
    {
        T CreateAddUserForm();
        T CreateUpdateUserForm(P user);
    }
}
