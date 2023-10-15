using LogicitApp.Data.DataLogic.Interfaces;

namespace LogicitApp.Data.DataLogic
{
    public class TransportLogic : BaseLogic, IAppliedEntityLogic
    {
        public bool IsUsed(long id)
        {
            var entity = DbContext.Orders.FirstOrDefault(x => x.TransportId == id);
            return entity != null;
        }
    }
}
