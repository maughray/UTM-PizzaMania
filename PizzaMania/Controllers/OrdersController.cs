using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaMania.Models;

namespace PizzaMania.Controllers
{
    public class OrdersController : Controller
    {
        private Context context = new Context();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = context.Orders.ToList();

            foreach(Order order in orders)
            {
                var pizzas = context.OrderPizzas.Where(o => o.orderId == order.id).ToList();
                foreach(OrderPizza op in pizzas)
                {
                    order.pizzas += op.pizzaName;
                    if (op != pizzas.Last())
                    {
                        order.pizzas += ", ";
                    }
                }
            }

            ViewBag.pendingOrders = orders.Where(o => o.status == PizzaStatus.Pending);
            ViewBag.preparingOrders = orders.Where(o => o.status == PizzaStatus.Preparing);

            Response.AddHeader("Refresh", "5");
            return View();
        }

        public ActionResult SetPreparing(int id)
        {
            SetOrderStatus(id, PizzaStatus.Preparing);
            return Redirect("/Orders/Index");
        }

        public ActionResult SetDone(int id)
        {
            SetOrderStatus(id, PizzaStatus.Done);
            return Redirect("/Orders/Index");
        }

        private void SetOrderStatus(int id, PizzaStatus status)
        {
            var order = context.Orders.Where(o => o.id == id).First();
            order.status = status;
            context.Entry(order).Property(x => x.status).IsModified = true;
            context.SaveChanges();
        }
    }
}