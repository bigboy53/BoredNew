using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DKD.Core.Config;
using DKD.Framework.Contract;
using DKD.Framework.Contract.Auditable;
using DKD.Framework.Data.Infrastructure;
using Newtonsoft.Json;

namespace DKD.Core.Logger
{
    [Table("AuditLog")]
    public class AuditLog
    {
        public int ModelId { get; set; }
        public string UserName { get; set; }
        public string ModuleName { get; set; }
        public string TableName { get; set; }
        public string EventType { get; set; }
        public string NewValues { get; set; }
    }

    public class LogDbContext : DbContextBase, IAuditable
    {
        public LogDbContext()
            : base(CachedConfigContext.Current.DataBaseConnection.Log)
        {
            Database.SetInitializer<LogDbContext>(null);
        }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public void WriteLog(int modelId, string userName, string moduleName, string tableName, string eventType, BaseModel newValues)
        {
            this.AuditLogs.Add(new AuditLog()
            {
                ModelId = modelId,
                UserName = userName,
                ModuleName = moduleName,
                TableName = tableName,
                EventType = eventType,
                NewValues = JsonConvert.SerializeObject(newValues, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
            });
            this.SaveChanges();
            this.Dispose();
        }
    }
}
