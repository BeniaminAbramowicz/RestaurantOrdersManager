namespace ASPNETapp2.Repositories
{
    interface IExtendedRepository<T,K> : IRepository<T,K>
    {
        T FindByName(string name);
    }
}
