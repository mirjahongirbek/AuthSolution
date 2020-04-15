using AuthModel.Models.Entitys;

namespace AuthModel.Interfaces
{
    public interface IDeleteDataService<TDeleteData>
        where TDeleteData : DeleteData
    {
        DeleteData AddData(string tableName, int UserId, object data, string schemeName = "");
    }
}
