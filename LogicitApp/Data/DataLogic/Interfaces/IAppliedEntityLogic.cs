using LogicitApp.Data.Models.Interfaces;
using LogicitApp.Shared.Results;

namespace LogicitApp.Data.DataLogic.Interfaces
{
    public interface IAppliedEntityLogic : IDisposable
    {
        public bool IsUsed(long id);
        public BaseResult Remove<T>(long id) where T : class, IDbEntity;
    }
}
