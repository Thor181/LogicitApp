using LogicitApp.Data.DataLogic.Interfaces;

namespace LogicitApp.Data.DataLogic
{
    public class ClientLogic : BaseLogic, IAppliedEntityLogic
    {
        public bool IsUsed(long id)
        {
            var entity = DbContext.Orders.FirstOrDefault(x => x.ClientId == id);
            return entity != null;
        }
    }
}
