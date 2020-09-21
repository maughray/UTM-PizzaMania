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

        public ActionResult Index()
        {
            var pizzas = context.Pizzas.ToList();
            var ingredients = context.Ingredients.ToList();
            var pizzaIngredients = context.PizzaIngredients.ToList();

            foreach(Pizza pizza in pizzas.ToList())
            {
                var requiredIngredients = pizzaIngredients.Where(i => i.pizzaid == pizza.id);
                foreach(PizzaIngredient requiredIngredient in requiredIngredients)
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
            ViewBag.Pizzas = pizzas;
            return View();
        }

        public ActionResult Order(int id)
        {
            var pizza = context.Pizzas.Where(p => p.id == id).First();
            var requiredIngredients = context.PizzaIngredients.Where(i => i.pizzaid == id);
            var ingredients = context.Ingredients;

            foreach(PizzaIngredient required in requiredIngredients.ToList())
            {
                var ingredient = ingredients.Where(i => i.id == required.ingredientId && i.weight >= required.ingredientWeight).ToList();
                if (ingredient.Count() == 0)
                {
                    return Content("Din pacate, aceasta pizza nu mai este in stock!");
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

            var orderId = context.Orders.Count() + 1;
            
            // Create order

            return Redirect("/Home/OrderStatus/" + orderId);
        }

        public ActionResult OrderStatus(int id)
        {
            var orders = context.Orders.Count();
            var order = context.Orders.Where(o => o.id == id).First();
            Response.AddHeader("Refresh", "5");
            return Content("Order status: " + order.status.ToString());
        }
    }
}