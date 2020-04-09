using IBatisNet.DataMapper;

namespace ASPNETapp2.Repositories
{
    public static class DBConnection
    {
        public static ISqlMapper EntityMapper
        {
            get
            {
                ISqlMapper mapper = Mapper.Instance();
                return mapper;
            }
        }
    }
}