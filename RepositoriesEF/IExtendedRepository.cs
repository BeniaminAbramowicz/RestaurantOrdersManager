namespace ASPNETapp2.RepositoriesEF
{
    interface IExtendedRepository<T,K> : IRepository<T,K>
    {
        T FindByName(string name);
    }
}
