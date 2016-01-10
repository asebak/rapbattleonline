#region Using

using System.Net.Http;
using System.Web.Http;
using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Secruity;

#endregion

namespace FreestyleOnline.classes.Base
{
    public class RapApiController : ApiController, IRapValidate, IRapCore, IRapServiceProvider
    {
        /// <summary>
        /// Validates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public virtual bool Validate(HttpRequestMessage request)
        {
            return WebApiValidation.Validate(request);
        }

        /// <summary>
        /// Gets the core.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetCore<T>() where T : RapClass, new()
        {
            return new T();
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>() where T : BaseRapProvider, new()
        {
            return new T();
        }
    }
}