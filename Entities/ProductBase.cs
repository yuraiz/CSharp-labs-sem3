﻿using System;
using System.Collections.Generic;
using System.Linq;
using _053505_Izmer_lab6.Collections;

namespace _053505_Izmer_lab6.Entities
{
    public class ProductBase
    {
        public event Action<string, string>? ListChanged;
        public event Action<Customer, Product>? NewOrder;

        MyCustomCollection<Customer>? Customers;
        MyCustomCollection<Product> Products;

        public ProductBase(MyCustomCollection<Product> products)
        {
            Products = products;
        }

        public Product? FindProduct(string name)
        {
            return Products.FirstOrDefault(product => product.Name == name);
        }

        private Customer? FindCustomer(string name)
        {
            return Customers?.FirstOrDefault(customer => customer.Name == name);
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
            ListChanged?.Invoke("New product: ", product.Name);
        }

        private void AddCustomer(Customer customer)
        {
            if (Customers == null)
            {
                Customers = new MyCustomCollection<Customer>(customer);
            }
            else
            {
                Customers.Add(customer);
            }
            ListChanged?.Invoke("New customer: ", customer.Name);
            customer.OrderEvent += (product) => NewOrder?.Invoke(customer, product);
        }

        public void PrintOrderOfCustomer(string name) => FindCustomer(name)!.PrintOrder();

        public void PrintSumOfCustomer(string name) => FindCustomer(name)!.PrintSum();

        public void MakeNewOrder(Customer customer, Product product)
        {
            AddCustomer(customer);
            customer.Order(product);
        }

        public void MakeNewOrder(Customer customer, IEnumerable<Product> products)
        {
            AddCustomer(customer);
            customer.Order(products);
        }
    }
}