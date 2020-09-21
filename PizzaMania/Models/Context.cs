using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PizzaMania.Models
{
    public class Context: DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaIngredient> PizzaIngredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPizza> OrderPizzas { get; set; }
    }

    public class PizzaDbInitializer: DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            context.Ingredients.Add(new Ingredient { id = 1, name = "Cascaval", weight = 800 });
            context.Ingredients.Add(new Ingredient { id = 2, name = "Ciuperci", weight = 200 });
            context.Ingredients.Add(new Ingredient { id = 3, name = "Carne de pui", weight = 300 });
            context.Ingredients.Add(new Ingredient { id = 4, name = "Gogosari", weight = 300 });
            context.Ingredients.Add(new Ingredient { id = 5, name = "Branza", weight = 500 });
            context.Ingredients.Add(new Ingredient { id = 6, name = "Mozzarella", weight = 500 });

            context.Pizzas.Add(new Pizza { id = 1, name = "Rancho", price = 75 });
            context.Pizzas.Add(new Pizza { id = 2, name = "Pepperoni", price = 85 });
            context.Pizzas.Add(new Pizza { id = 3, name = "Capricioasa", price = 90 });

            context.PizzaIngredients.Add(new PizzaIngredient { id = 1, pizzaid = 1, ingredientId = 1, ingredientWeight = 100 });
            context.PizzaIngredients.Add(new PizzaIngredient { id = 2, pizzaid = 1, ingredientId = 2, ingredientWeight = 100 });
            context.PizzaIngredients.Add(new PizzaIngredient { id = 3, pizzaid = 1, ingredientId = 3, ingredientWeight = 100 });
            context.PizzaIngredients.Add(new PizzaIngredient { id = 4, pizzaid = 1, ingredientId = 4, ingredientWeight = 100 });

            context.PizzaIngredients.Add(new PizzaIngredient { id = 5, pizzaid = 2, ingredientId = 1, ingredientWeight = 100 });
            context.PizzaIngredients.Add(new PizzaIngredient { id = 6, pizzaid = 2, ingredientId = 5, ingredientWeight = 100 });
            context.PizzaIngredients.Add(new PizzaIngredient { id = 7, pizzaid = 2, ingredientId = 6, ingredientWeight = 100 });

            context.PizzaIngredients.Add(new PizzaIngredient { id = 8, pizzaid = 3, ingredientId = 1, ingredientWeight = 100 });
            context.PizzaIngredients.Add(new PizzaIngredient { id = 9, pizzaid = 3, ingredientId = 5, ingredientWeight = 100 });
            context.PizzaIngredients.Add(new PizzaIngredient { id = 10, pizzaid = 3, ingredientId = 3, ingredientWeight = 100 });
            context.PizzaIngredients.Add(new PizzaIngredient { id = 11, pizzaid = 3, ingredientId = 6, ingredientWeight = 100 });

            base.Seed(context);
        }
    }
}