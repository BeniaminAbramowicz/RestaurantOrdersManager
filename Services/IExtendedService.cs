namespace ASPNETapp2.Services
{
    interface IExtendedService<T,K> : IService<T,K>
    {
        T FindByName(string name);
    }
}
