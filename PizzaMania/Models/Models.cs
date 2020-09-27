using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaMania.Models
{
    public class Ingredient
    {
        public int id { get; set; }
        public string name { get; set; }
        public int weight { get; set; }
    }

    public class Pizza
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public String ingredients { get; set; }
    }

    public class PizzaIngredient
    {
        public int id { get; set; }
        public int pizzaid { get; set; }
        public int ingredientId { get; set; }
        public int ingredientWeight { get; set; }
    }

    public class Order
    {
        public int id { get; set; }
        public int tableId { get; set; }
        public PizzaStatus status { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string pizzas { get; set; }
    }

    public class OrderPizza
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int pizzaId { get; set; }
        public string pizzaName { get; set; }
    }

    public enum PizzaStatus
    {
        Pending = 0,
        Preparing = 1,
        Done = 2,
        None = 3
    }
}