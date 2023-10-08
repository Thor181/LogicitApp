using LogicitApp.Data.Models;
using LogicitApp.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicitApp.Data.DataLogic
{
    public class OrderLogic : BaseLogic
    {
        private bool _disposed = false;

        protected OrderProductLogic OrderProductLogic { get; set; }

        public OrderLogic()
        {
            OrderProductLogic = new OrderProductLogic();
        }

        public override BaseResult Remove<T>(long id)
        {
            using var tr = OrderProductLogic.DbContext.Database.BeginTransaction();
            
            var orderProducts = OrderProductLogic.GetAll<OrderProduct>(x => x.OrderId == id);
            var removeResult = OrderProductLogic.RemoveRange<OrderProduct>(orderProducts.Select(x => x.Id).ToArray());

            if (!removeResult.Success)
            {
                tr.Rollback();
                return removeResult;
            }

            var removeOrderResult = new BaseResult();

            try
            {
                var order = Get<Order>(id);
                DbContext.Remove(order);

                tr.Commit();

                DbContext.SaveChanges();

                removeOrderResult.Message = "Успешно удалено";
            }
            catch (Exception e)
            {
                removeOrderResult.Success = false;
                removeOrderResult.Message = $"При удалении сущности возникла ошибка\r\n{e.Message}\r\n{e.InnerException}";
            }
            
            if (!removeOrderResult.Success)
            {
                tr.Rollback();
                return removeOrderResult;
            }

            return removeOrderResult;
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    OrderProductLogic.Dispose();
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
