using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.EntityParser;
using Newtonsoft.Json;
using PetaPoco;

namespace MixERP.Net.Api.Core
{
    /// <summary>
    ///     Provides a direct HTTP access to perform various tasks such as adding, editing, and removing Payment Cards.
    /// </summary>
    [RoutePrefix("api/v1.5/core/payment-card")]
    public class PaymentCardController : ApiController
    {
        /// <summary>
        ///     The PaymentCard data context.
        /// </summary>
        private readonly MixERP.Net.Schemas.Core.Data.PaymentCard PaymentCardContext;

        public PaymentCardController()
        {
            this.LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this.UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.Catalog = AppUsers.GetCurrentUserDB();

            this.PaymentCardContext = new MixERP.Net.Schemas.Core.Data.PaymentCard
            {
                Catalog = this.Catalog,
                LoginId = this.LoginId
            };
        }

        public long LoginId { get; }
        public int UserId { get; private set; }
        public int OfficeId { get; private set; }
        public string Catalog { get; }

        /// <summary>
        ///     Counts the number of payment cards.
        /// </summary>
        /// <returns>Returns the count of the payment cards.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("count")]
        [Route("~/api/core/payment-card/count")]
        public long Count()
        {
            try
            {
                return this.PaymentCardContext.Count();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Returns an instance of payment card.
        /// </summary>
        /// <param name="paymentCardId">Enter PaymentCardId to search for.</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("{paymentCardId}")]
        [Route("~/api/core/payment-card/{paymentCardId}")]
        public MixERP.Net.Entities.Core.PaymentCard Get(int paymentCardId)
        {
            try
            {
                return this.PaymentCardContext.Get(paymentCardId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [AcceptVerbs("GET", "HEAD")]
        [Route("get")]
        [Route("~/api/core/payment-card/get")]
        public IEnumerable<MixERP.Net.Entities.Core.PaymentCard> Get([FromUri] int[] paymentCardIds)
        {
            try
            {
                return this.PaymentCardContext.Get(paymentCardIds);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a paginated collection containing 25 payment cards on each page, sorted by the property PaymentCardId.
        /// </summary>
        /// <returns>Returns the first page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("")]
        [Route("~/api/core/payment-card")]
        public IEnumerable<MixERP.Net.Entities.Core.PaymentCard> GetPagedResult()
        {
            try
            {
                return this.PaymentCardContext.GetPagedResult();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a paginated collection containing 25 payment cards on each page, sorted by the property PaymentCardId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <returns>Returns the requested page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("page/{pageNumber}")]
        [Route("~/api/core/payment-card/page/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Core.PaymentCard> GetPagedResult(long pageNumber)
        {
            try
            {
                return this.PaymentCardContext.GetPagedResult(pageNumber);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a filtered and paginated collection containing 25 payment cards on each page, sorted by the property PaymentCardId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns the requested page from the collection using the supplied filters.</returns>
        [AcceptVerbs("POST")]
        [Route("get-where/{pageNumber}")]
        [Route("~/api/core/payment-card/get-where/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Core.PaymentCard> GetWhere(long pageNumber, [FromBody]dynamic filters)
        {
            try
            {
                List<EntityParser.Filter> f = JsonConvert.DeserializeObject<List<EntityParser.Filter>>(filters);
                return this.PaymentCardContext.GetWhere(pageNumber, f);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Displayfield is a lightweight key/value collection of payment cards.
        /// </summary>
        /// <returns>Returns an enumerable key/value collection of payment cards.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("display-fields")]
        [Route("~/api/core/payment-card/display-fields")]
        public IEnumerable<DisplayField> GetDisplayFields()
        {
            try
            {
                return this.PaymentCardContext.GetDisplayFields();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     A custom field is a user defined field for payment cards.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of payment cards.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields")]
        [Route("~/api/core/payment-card/custom-fields")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields()
        {
            try
            {
                return this.PaymentCardContext.GetCustomFields(null);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     A custom field is a user defined field for payment cards.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of payment cards.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields")]
        [Route("~/api/core/payment-card/custom-fields/{resourceId}")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
            try
            {
                return this.PaymentCardContext.GetCustomFields(resourceId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Adds or edits your instance of PaymentCard class.
        /// </summary>
        /// <param name="paymentCard">Your instance of payment cards class to add or edit.</param>
        [AcceptVerbs("PUT")]
        [Route("add-or-edit")]
        [Route("~/api/core/payment-card/add-or-edit")]
        public void AddOrEdit([FromBody]MixERP.Net.Entities.Core.PaymentCard paymentCard)
        {
            if (paymentCard == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.PaymentCardContext.AddOrEdit(paymentCard);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Adds your instance of PaymentCard class.
        /// </summary>
        /// <param name="paymentCard">Your instance of payment cards class to add.</param>
        [AcceptVerbs("POST")]
        [Route("add/{paymentCard}")]
        [Route("~/api/core/payment-card/add/{paymentCard}")]
        public void Add(MixERP.Net.Entities.Core.PaymentCard paymentCard)
        {
            if (paymentCard == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.PaymentCardContext.Add(paymentCard);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Edits existing record with your instance of PaymentCard class.
        /// </summary>
        /// <param name="paymentCard">Your instance of PaymentCard class to edit.</param>
        /// <param name="paymentCardId">Enter the value for PaymentCardId in order to find and edit the existing record.</param>
        [AcceptVerbs("PUT")]
        [Route("edit/{paymentCardId}")]
        [Route("~/api/core/payment-card/edit/{paymentCardId}")]
        public void Edit(int paymentCardId, [FromBody] MixERP.Net.Entities.Core.PaymentCard paymentCard)
        {
            if (paymentCard == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.PaymentCardContext.Update(paymentCard, paymentCardId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Deletes an existing instance of PaymentCard class via PaymentCardId.
        /// </summary>
        /// <param name="paymentCardId">Enter the value for PaymentCardId in order to find and delete the existing record.</param>
        [AcceptVerbs("DELETE")]
        [Route("delete/{paymentCardId}")]
        [Route("~/api/core/payment-card/delete/{paymentCardId}")]
        public void Delete(int paymentCardId)
        {
            try
            {
                this.PaymentCardContext.Delete(paymentCardId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


    }
}