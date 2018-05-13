using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tryApi.Models;
using tryApi.Utilities;
using tryApi.ViewModel;

namespace tryApi.DAL
{
    public class ProductDataLayer
    {
        static string JSONFilePath = System.Web.HttpContext.Current.Request.MapPath("~\\ProductsData.json");

        public static Product[] GetProducts()
        {
            Product[] products = Helper.DeSerializeNonStandardList<Product>(JSONFilePath);
            return products;
        }

        public static void DeleteProduct(int ProductID)
        {
            JSONHandler.DeleteJsonElement(JSONFilePath, "id", ProductID.ToString());
        }

        public static void AddProduct(ProductFilterViewModel Product)
        {
            dynamic jsonObject = new JObject();
            jsonObject.name = Product.name;

            JSONHandler.AddJsonElement(JSONFilePath, jsonObject);
        }
    }
}