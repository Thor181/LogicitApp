using LogicitApp.Data.DataLogic.Interfaces;

namespace LogicitApp.Data.DataLogic
{
    public class DriverLogic : BaseLogic, IAppliedEntityLogic
    {
        public bool IsUsed(long id)
        {
            var entity = DbContext.Orders.FirstOrDefault(x => x.DriverId == id);
            return entity != null;
        }
    }
}
