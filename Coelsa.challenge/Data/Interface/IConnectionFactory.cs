using System.Data;

namespace Coelsa.Challenge.Api.Data.Cnx
{
    public interface IConnectionFactory
    {
        IDbConnection GetDbConnection { get; }
    }
}