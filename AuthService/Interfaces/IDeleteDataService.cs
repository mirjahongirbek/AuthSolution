using AuthService.Models;

namespace AuthService.Interfaces.Service
{
    public interface IDeleteDataService<TDeleteData>
        where TDeleteData: DeleteData
    {
       DeleteData AddData(string tableName, int UserId, object data, string schemeName = "");
    }
}
