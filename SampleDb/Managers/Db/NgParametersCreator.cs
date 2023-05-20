using Npgsql;
using System.Data;

namespace SampleDb.Managers.Db
{
    public class NgParametersCreator : IDbParametersCreator
    {
        public IDbDataParameter GetParameter<TValue>(string key, TValue value)
        {
            var param = new NpgsqlParameter();
            param.ParameterName = key;
            param.Value = value;

            return param;
        }
    }
}
