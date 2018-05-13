using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tryApi.DAL;
using tryApi.Models;
using tryApi.Utilities;
using tryApi.ViewModel;

namespace tryApi.Controllers
{
    public class ProductController : ApiController
    {
        public IEnumerable<Product> GetAllProducts()
        {
            return ProductDataLayer.GetProducts();
        }

        [Route("api/producth/{name}")]
        public IHttpActionResult GetProduct(string name)
        {
            var product = ProductDataLayer.GetProducts().FirstOrDefault((p) => p.Name == name);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IHttpActionResult AddProduct([FromBody]ProductFilterViewModel obj)
        {
            try
            {
                ProductDataLayer.AddProduct(obj);
                return Ok();
            }
            catch (Exception ex)
            {
                return new ErrorMessage(ex, HttpStatusCode.InternalServerError, Request);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                ProductDataLayer.DeleteProduct(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return new ErrorMessage(ex, HttpStatusCode.InternalServerError, Request);
            }
        }
    }
}
