using System;
using System.Collections.Generic;
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
}