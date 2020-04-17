namespace ASPNETapp2.Repositories
{
    interface IExtendedRepository<T> : IRepository<T>
    {
        T FindByName(string name);
    }
}
