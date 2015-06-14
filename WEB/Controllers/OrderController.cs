using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WEB.Models;

namespace WEB.Controllers
{
    public class OrderController : ApiController
    {
        private registerEntities db = new registerEntities();
        public class OrdersAnswer
        {
            public DateTime date { get; set; }
            public string number { get; set; }
            public string name { get; set; }
            public int id { get; set; }
        }
        // GET api/Order
        public IEnumerable<OrdersAnswer> Getcommissionorders()
        {
            List<OrdersAnswer> ans = new List<OrdersAnswer>();
            foreach (var item in db.commissionorders)
            {
                ans.Add(new OrdersAnswer
                {
                    id = item.Experts_idExperts,
                    number = item.commissionOrderNumber.ToString(),
                    date = Convert.ToDateTime(item.CommissionOrderDate),
                    name = item.commissionName
                });
            }

            return ans.AsEnumerable();
        }

        // GET api/Order/5
        public OrdersAnswer Getcommissionorders(int id)
        {
            commissionorders commissionorders = db.commissionorders.Find(id);
            if (commissionorders == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return new OrdersAnswer
            {
                number = commissionorders.commissionOrderNumber.ToString(),
                date = Convert.ToDateTime(commissionorders.CommissionOrderDate),
                name = commissionorders.commissionName
            };
        }

        // PUT api/Order/5
        public HttpResponseMessage Postcommissionorders(commissionorders commissionorders)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

           /* if (id != commissionorders.idCommissionOrders)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            */
            db.Entry(commissionorders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Order
        public HttpResponseMessage Putcommissionorders(commissionorders commissionorders)
        {
            if (ModelState.IsValid)
            {
                db.commissionorders.Add(commissionorders);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, commissionorders);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = commissionorders.idCommissionOrders }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Order/5
        public HttpResponseMessage Deletecommissionorders(int id)
        {
            commissionorders commissionorders = db.commissionorders.Find(id);
            if (commissionorders == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.commissionorders.Remove(commissionorders);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, commissionorders);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}