using LogicitApp.Data.Models;

namespace LogicitApp.Data.DataLogic
{
    public class ProductLogic : BaseLogic
    {
        public bool IsUsed(long id)
        {
            var entity = Get<Product>(id);
            
            if (entity == null)
                return false;

            return entity.OrderProducts.Any();
        }

    }
}
