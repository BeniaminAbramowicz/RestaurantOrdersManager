namespace ASPNETapp2.Services
{
    interface IExtendedService<T> : IService<T>
    {
        T FindByName(string name);
    }
}
