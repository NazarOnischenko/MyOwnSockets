using System;
using System.Collections.Generic;
using System.Text;

namespace MyOwnClientSocket
{
    class Product
    {
        public string Name { get; }
        public int Calories { get; }
        public int Weight { get; }
        public Product(string name,int calories,int weight)
        {
            Name = name;
            Calories = calories;
            Weight = weight;
        }
    }
}
