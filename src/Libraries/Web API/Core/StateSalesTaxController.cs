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
    ///     Provides a direct HTTP access to perform various tasks such as adding, editing, and removing State Sales Taxes.
    /// </summary>
    [RoutePrefix("api/v1.5/core/state-sales-tax")]
    public class StateSalesTaxController : ApiController
    {
        /// <summary>
        ///     The StateSalesTax data context.
        /// </summary>
        private readonly MixERP.Net.Schemas.Core.Data.StateSalesTax StateSalesTaxContext;

        public StateSalesTaxController()
        {
            this.LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this.UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.Catalog = AppUsers.GetCurrentUserDB();

            this.StateSalesTaxContext = new MixERP.Net.Schemas.Core.Data.StateSalesTax
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
        ///     Counts the number of state sales taxes.
        /// </summary>
        /// <returns>Returns the count of the state sales taxes.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("count")]
        [Route("~/api/core/state-sales-tax/count")]
        public long Count()
        {
            try
            {
                return this.StateSalesTaxContext.Count();
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
        ///     Returns an instance of state sales tax.
        /// </summary>
        /// <param name="stateSalesTaxId">Enter StateSalesTaxId to search for.</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("{stateSalesTaxId}")]
        [Route("~/api/core/state-sales-tax/{stateSalesTaxId}")]
        public MixERP.Net.Entities.Core.StateSalesTax Get(int stateSalesTaxId)
        {
            try
            {
                return this.StateSalesTaxContext.Get(stateSalesTaxId);
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
        [Route("~/api/core/state-sales-tax/get")]
        public IEnumerable<MixERP.Net.Entities.Core.StateSalesTax> Get([FromUri] int[] stateSalesTaxIds)
        {
            try
            {
                return this.StateSalesTaxContext.Get(stateSalesTaxIds);
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
        ///     Creates a paginated collection containing 25 state sales taxes on each page, sorted by the property StateSalesTaxId.
        /// </summary>
        /// <returns>Returns the first page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("")]
        [Route("~/api/core/state-sales-tax")]
        public IEnumerable<MixERP.Net.Entities.Core.StateSalesTax> GetPagedResult()
        {
            try
            {
                return this.StateSalesTaxContext.GetPagedResult();
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
        ///     Creates a paginated collection containing 25 state sales taxes on each page, sorted by the property StateSalesTaxId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <returns>Returns the requested page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("page/{pageNumber}")]
        [Route("~/api/core/state-sales-tax/page/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Core.StateSalesTax> GetPagedResult(long pageNumber)
        {
            try
            {
                return this.StateSalesTaxContext.GetPagedResult(pageNumber);
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
        ///     Creates a filtered and paginated collection containing 25 state sales taxes on each page, sorted by the property StateSalesTaxId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns the requested page from the collection using the supplied filters.</returns>
        [AcceptVerbs("POST")]
        [Route("get-where/{pageNumber}")]
        [Route("~/api/core/state-sales-tax/get-where/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Core.StateSalesTax> GetWhere(long pageNumber, [FromBody]dynamic filters)
        {
            try
            {
                List<EntityParser.Filter> f = JsonConvert.DeserializeObject<List<EntityParser.Filter>>(filters);
                return this.StateSalesTaxContext.GetWhere(pageNumber, f);
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
        ///     Displayfield is a lightweight key/value collection of state sales taxes.
        /// </summary>
        /// <returns>Returns an enumerable key/value collection of state sales taxes.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("display-fields")]
        [Route("~/api/core/state-sales-tax/display-fields")]
        public IEnumerable<DisplayField> GetDisplayFields()
        {
            try
            {
                return this.StateSalesTaxContext.GetDisplayFields();
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
        ///     A custom field is a user defined field for state sales taxes.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of state sales taxes.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields")]
        [Route("~/api/core/state-sales-tax/custom-fields")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields()
        {
            try
            {
                return this.StateSalesTaxContext.GetCustomFields(null);
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
        ///     A custom field is a user defined field for state sales taxes.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of state sales taxes.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields")]
        [Route("~/api/core/state-sales-tax/custom-fields/{resourceId}")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
            try
            {
                return this.StateSalesTaxContext.GetCustomFields(resourceId);
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
        ///     Adds or edits your instance of StateSalesTax class.
        /// </summary>
        /// <param name="stateSalesTax">Your instance of state sales taxes class to add or edit.</param>
        [AcceptVerbs("PUT")]
        [Route("add-or-edit")]
        [Route("~/api/core/state-sales-tax/add-or-edit")]
        public void AddOrEdit([FromBody]MixERP.Net.Entities.Core.StateSalesTax stateSalesTax)
        {
            if (stateSalesTax == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.StateSalesTaxContext.AddOrEdit(stateSalesTax);
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
        ///     Adds your instance of StateSalesTax class.
        /// </summary>
        /// <param name="stateSalesTax">Your instance of state sales taxes class to add.</param>
        [AcceptVerbs("POST")]
        [Route("add/{stateSalesTax}")]
        [Route("~/api/core/state-sales-tax/add/{stateSalesTax}")]
        public void Add(MixERP.Net.Entities.Core.StateSalesTax stateSalesTax)
        {
            if (stateSalesTax == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.StateSalesTaxContext.Add(stateSalesTax);
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
        ///     Edits existing record with your instance of StateSalesTax class.
        /// </summary>
        /// <param name="stateSalesTax">Your instance of StateSalesTax class to edit.</param>
        /// <param name="stateSalesTaxId">Enter the value for StateSalesTaxId in order to find and edit the existing record.</param>
        [AcceptVerbs("PUT")]
        [Route("edit/{stateSalesTaxId}")]
        [Route("~/api/core/state-sales-tax/edit/{stateSalesTaxId}")]
        public void Edit(int stateSalesTaxId, [FromBody] MixERP.Net.Entities.Core.StateSalesTax stateSalesTax)
        {
            if (stateSalesTax == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.StateSalesTaxContext.Update(stateSalesTax, stateSalesTaxId);
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
        ///     Deletes an existing instance of StateSalesTax class via StateSalesTaxId.
        /// </summary>
        /// <param name="stateSalesTaxId">Enter the value for StateSalesTaxId in order to find and delete the existing record.</param>
        [AcceptVerbs("DELETE")]
        [Route("delete/{stateSalesTaxId}")]
        [Route("~/api/core/state-sales-tax/delete/{stateSalesTaxId}")]
        public void Delete(int stateSalesTaxId)
        {
            try
            {
                this.StateSalesTaxContext.Delete(stateSalesTaxId);
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