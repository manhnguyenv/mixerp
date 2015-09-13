using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.EntityParser;
using Newtonsoft.Json;
using PetaPoco;

namespace MixERP.Net.Api.Config
{
    /// <summary>
    ///     Provides a direct HTTP access to perform various tasks such as adding, editing, and removing Attachment Factories.
    /// </summary>
    [RoutePrefix("api/v1.5/config/attachment-factory")]
    public class AttachmentFactoryController : ApiController
    {
        /// <summary>
        ///     The AttachmentFactory data context.
        /// </summary>
        private readonly MixERP.Net.Schemas.Config.Data.AttachmentFactory AttachmentFactoryContext;

        public AttachmentFactoryController()
        {
            this.LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this.UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.Catalog = AppUsers.GetCurrentUserDB();

            this.AttachmentFactoryContext = new MixERP.Net.Schemas.Config.Data.AttachmentFactory
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
        ///     Counts the number of attachment factories.
        /// </summary>
        /// <returns>Returns the count of the attachment factories.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("count")]
        [Route("~/api/config/attachment-factory/count")]
        public long Count()
        {
            try
            {
                return this.AttachmentFactoryContext.Count();
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
        ///     Returns an instance of attachment factory.
        /// </summary>
        /// <param name="key">Enter Key to search for.</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("{key}")]
        [Route("~/api/config/attachment-factory/{key}")]
        public MixERP.Net.Entities.Config.AttachmentFactory Get(string key)
        {
            try
            {
                return this.AttachmentFactoryContext.Get(key);
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
        [Route("~/api/config/attachment-factory/get")]
        public IEnumerable<MixERP.Net.Entities.Config.AttachmentFactory> Get([FromUri] string[] keys)
        {
            try
            {
                return this.AttachmentFactoryContext.Get(keys);
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
        ///     Creates a paginated collection containing 25 attachment factories on each page, sorted by the property Key.
        /// </summary>
        /// <returns>Returns the first page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("")]
        [Route("~/api/config/attachment-factory")]
        public IEnumerable<MixERP.Net.Entities.Config.AttachmentFactory> GetPagedResult()
        {
            try
            {
                return this.AttachmentFactoryContext.GetPagedResult();
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
        ///     Creates a paginated collection containing 25 attachment factories on each page, sorted by the property Key.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <returns>Returns the requested page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("page/{pageNumber}")]
        [Route("~/api/config/attachment-factory/page/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Config.AttachmentFactory> GetPagedResult(long pageNumber)
        {
            try
            {
                return this.AttachmentFactoryContext.GetPagedResult(pageNumber);
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
        ///     Creates a filtered and paginated collection containing 25 attachment factories on each page, sorted by the property Key.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns the requested page from the collection using the supplied filters.</returns>
        [AcceptVerbs("POST")]
        [Route("get-where/{pageNumber}")]
        [Route("~/api/config/attachment-factory/get-where/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Config.AttachmentFactory> GetWhere(long pageNumber, [FromBody]dynamic filters)
        {
            try
            {
                List<EntityParser.Filter> f = JsonConvert.DeserializeObject<List<EntityParser.Filter>>(filters);
                return this.AttachmentFactoryContext.GetWhere(pageNumber, f);
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
        ///     Displayfield is a lightweight key/value collection of attachment factories.
        /// </summary>
        /// <returns>Returns an enumerable key/value collection of attachment factories.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("display-fields")]
        [Route("~/api/config/attachment-factory/display-fields")]
        public IEnumerable<DisplayField> GetDisplayFields()
        {
            try
            {
                return this.AttachmentFactoryContext.GetDisplayFields();
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
        ///     A custom field is a user defined field for attachment factories.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of attachment factories.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields")]
        [Route("~/api/config/attachment-factory/custom-fields")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields()
        {
            try
            {
                return this.AttachmentFactoryContext.GetCustomFields(null);
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
        ///     A custom field is a user defined field for attachment factories.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of attachment factories.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields")]
        [Route("~/api/config/attachment-factory/custom-fields/{resourceId}")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
            try
            {
                return this.AttachmentFactoryContext.GetCustomFields(resourceId);
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
        ///     Adds or edits your instance of AttachmentFactory class.
        /// </summary>
        /// <param name="attachmentFactory">Your instance of attachment factories class to add or edit.</param>
        [AcceptVerbs("PUT")]
        [Route("add-or-edit")]
        [Route("~/api/config/attachment-factory/add-or-edit")]
        public void AddOrEdit([FromBody]MixERP.Net.Entities.Config.AttachmentFactory attachmentFactory)
        {
            if (attachmentFactory == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.AttachmentFactoryContext.AddOrEdit(attachmentFactory);
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
        ///     Adds your instance of AttachmentFactory class.
        /// </summary>
        /// <param name="attachmentFactory">Your instance of attachment factories class to add.</param>
        [AcceptVerbs("POST")]
        [Route("add/{attachmentFactory}")]
        [Route("~/api/config/attachment-factory/add/{attachmentFactory}")]
        public void Add(MixERP.Net.Entities.Config.AttachmentFactory attachmentFactory)
        {
            if (attachmentFactory == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.AttachmentFactoryContext.Add(attachmentFactory);
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
        ///     Edits existing record with your instance of AttachmentFactory class.
        /// </summary>
        /// <param name="attachmentFactory">Your instance of AttachmentFactory class to edit.</param>
        /// <param name="key">Enter the value for Key in order to find and edit the existing record.</param>
        [AcceptVerbs("PUT")]
        [Route("edit/{key}")]
        [Route("~/api/config/attachment-factory/edit/{key}")]
        public void Edit(string key, [FromBody] MixERP.Net.Entities.Config.AttachmentFactory attachmentFactory)
        {
            if (attachmentFactory == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.AttachmentFactoryContext.Update(attachmentFactory, key);
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
        ///     Deletes an existing instance of AttachmentFactory class via Key.
        /// </summary>
        /// <param name="key">Enter the value for Key in order to find and delete the existing record.</param>
        [AcceptVerbs("DELETE")]
        [Route("delete/{key}")]
        [Route("~/api/config/attachment-factory/delete/{key}")]
        public void Delete(string key)
        {
            try
            {
                this.AttachmentFactoryContext.Delete(key);
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