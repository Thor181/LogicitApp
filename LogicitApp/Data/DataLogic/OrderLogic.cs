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

        public BaseResult AddOrder(Order order, IEnumerable<Product> products)
        {
            var result = new BaseResult() { Message = "Заказ успешно добавлен" };

            try
            {
                DbContext.Add(order);
                DbContext.SaveChanges();

                foreach (var item in products)
                {
                    var orderProduct = new OrderProduct()
                    {
                        OrderId = order.Id,
                        ProductId = item.Id
                    };

                    OrderProductLogic.MarkAsAdded(orderProduct);
                }

                var orderProductResult = OrderProductLogic.SaveChanges();
                if (!orderProductResult.Success)
                {
                    result.Success = false;
                    result.Message = orderProductResult.Message;
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"При добавлении заказа возникла ошибка | {e.Message} | {e.InnerException}";
            }

            return result;
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
