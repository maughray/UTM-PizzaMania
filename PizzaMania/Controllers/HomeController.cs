using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using PizzaMania.Models;

namespace PizzaMania.Controllers
{
    public class HomeController : Controller
    {
        private Context context = new Context();

        public ActionResult Index(int id, string message = null)
        {
            ViewBag.SuccessMessage = message;
            ViewBag.Pizzas = GetAvailablePizzas();
            ViewBag.CardItems = GetCardItems(id);
            ViewBag.tableId = id;

            var order = context.Orders.Where(o => o.tableId == id && o.status == PizzaStatus.Done).ToList();
            foreach(Order o in order)
            {
                context.Orders.Remove(o);
                context.SaveChanges();
            }

            Response.AddHeader("Refresh", "5");
            return View();
        }

        public ActionResult AddToCard(int tableId, int pizzaId)
        {
            var pizza = context.Pizzas.Where(p => p.id == pizzaId).First();
            var requiredIngredients = context.PizzaIngredients.Where(i => i.pizzaid == pizzaId);
            var ingredients = context.Ingredients;

            foreach(PizzaIngredient required in requiredIngredients.ToList())
            {
                var ingredient = ingredients.Where(i => i.id == required.ingredientId && i.weight >= required.ingredientWeight).ToList();
                if (ingredient.Count() == 0)
                {
                    return RedirectToAction("Index", new { id = tableId, message = "Nu mai este in stock!" });
                }
                else
                {
                    ingredient.First().weight -= required.ingredientWeight;
                }
            }

            foreach(Ingredient i in ingredients)
            {
                context.Ingredients.Attach(i);
                context.Entry(i).Property(x => x.weight).IsModified = true;
            }

            var orderId = GetActiveOrder(tableId).id;
            var orderPizza = new OrderPizza { orderId = orderId, pizzaId = pizzaId, pizzaName = pizza.name };
            context.OrderPizzas.Add(orderPizza);

            context.SaveChanges();

            return Redirect("/Home/Index/" + tableId);
        }

        public ActionResult Order(int tableId)
        {
            context.Orders.Where(o => o.tableId == tableId && o.status == PizzaStatus.None).First().status = PizzaStatus.Pending;
            context.SaveChanges();
            return Redirect("/Home/OrderStatus?tableId=" + tableId);
        }

        public ActionResult OrderStatus(int tableId)
        {
            var order = context.Orders.Where(o => o.tableId == tableId && o.status != PizzaStatus.None).First();
            Response.AddHeader("Refresh", "5");
            return Content("Order status: " + order.status.ToString());
        }

        //
        // Actions
        //

        private Order GetActiveOrder(int tableId)
        {
            var orders = context.Orders.Where(o => o.tableId == tableId && o.status != PizzaStatus.Done).ToList();
            if (orders.Count() == 0)
            {
                var order = new Order { tableId = tableId, status = PizzaStatus.None };
                context.Orders.Add(order);
                context.SaveChanges();
                return order;
            }
            else
            {
                return orders.First();
            }
        }

        private List<OrderPizza> GetCardItems(int tableId)
        {
            var order = GetActiveOrder(tableId);
            var pizzas = context.OrderPizzas.Where(o => o.orderId == order.id).ToList();
            return pizzas;
        }

        private List<Pizza> GetAvailablePizzas()
        {
            var pizzas = context.Pizzas.ToList();
            var ingredients = context.Ingredients.ToList();
            var pizzaIngredients = context.PizzaIngredients.ToList();

            foreach (Pizza pizza in pizzas.ToList())
            {
                var requiredIngredients = pizzaIngredients.Where(i => i.pizzaid == pizza.id);
                foreach (PizzaIngredient requiredIngredient in requiredIngredients)
                {
                    var ingredient = ingredients.Where(i => i.id == requiredIngredient.ingredientId && i.weight >= requiredIngredient.ingredientWeight);
                    if (ingredient.Count() == 0)
                    {
                        pizzas.Remove(pizza);
                    }
                    else
                    {
                        if (pizza.ingredients == null)
                        {
                            pizza.ingredients = ingredient.First().name;
                        }
                        else
                        {
                            pizza.ingredients += ", " + ingredient.First().name;
                        }
                    }
                }
            }

            return pizzas;
        }
    }
}