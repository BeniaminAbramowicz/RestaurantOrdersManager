using IBatisNet.DataMapper;

namespace ASPNETapp2.Repository
{
    class DBConnection
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