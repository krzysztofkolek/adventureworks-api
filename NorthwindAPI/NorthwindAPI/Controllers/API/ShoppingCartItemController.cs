using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NorthwindAPI.DBModels;

namespace NorthwindAPI.Controllers.API
{
    public class ShoppingCartItemController : ApiController
    {
        private AdventureWorks2014Entities1 db = new AdventureWorks2014Entities1();

        // GET api/ShoppingCartItem
        public IQueryable<ShoppingCartItem> GetShoppingCartItems()
        {
            return db.ShoppingCartItems;
        }

        // GET api/ShoppingCartItem/5
        [ResponseType(typeof(ShoppingCartItem))]
        public IHttpActionResult GetShoppingCartItem(int id)
        {
            ShoppingCartItem shoppingcartitem = db.ShoppingCartItems.Find(id);
            if (shoppingcartitem == null)
            {
                return NotFound();
            }

            return Ok(shoppingcartitem);
        }

        // PUT api/ShoppingCartItem/5
        public IHttpActionResult PutShoppingCartItem(int id, ShoppingCartItem shoppingcartitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingcartitem.ShoppingCartItemID)
            {
                return BadRequest();
            }

            db.Entry(shoppingcartitem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/ShoppingCartItem
        [ResponseType(typeof(ShoppingCartItem))]
        public IHttpActionResult PostShoppingCartItem(ShoppingCartItem shoppingcartitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingCartItems.Add(shoppingcartitem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shoppingcartitem.ShoppingCartItemID }, shoppingcartitem);
        }

        // DELETE api/ShoppingCartItem/5
        [ResponseType(typeof(ShoppingCartItem))]
        public IHttpActionResult DeleteShoppingCartItem(int id)
        {
            ShoppingCartItem shoppingcartitem = db.ShoppingCartItems.Find(id);
            if (shoppingcartitem == null)
            {
                return NotFound();
            }

            db.ShoppingCartItems.Remove(shoppingcartitem);
            db.SaveChanges();

            return Ok(shoppingcartitem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingCartItemExists(int id)
        {
            return db.ShoppingCartItems.Count(e => e.ShoppingCartItemID == id) > 0;
        }
    }
}