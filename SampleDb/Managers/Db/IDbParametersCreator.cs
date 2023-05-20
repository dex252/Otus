using System.Data;

namespace SampleDb.Managers.Db
{
    public interface IDbParametersCreator
    {
        IDbDataParameter GetParameter<TValue>(string key, TValue value);
    }
}
