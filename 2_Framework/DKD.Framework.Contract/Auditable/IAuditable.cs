
namespace DKD.Framework.Contract.Auditable
{
    public interface IAuditable
    {
        void WriteLog(int modelId, string userName, string moduleName, string tableName, string eventType, BaseModel newValues);
    }
}
