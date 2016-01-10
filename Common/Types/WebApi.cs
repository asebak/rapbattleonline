namespace Common.Types
{
    public class WebApi
    {
        #region Members

        private readonly string _controllerName;
        private string _baseUrl;
        #endregion

        #region Constructor

        public WebApi(string controllerName)
        {
            this._controllerName = controllerName;
            this.BaseUrl = RapGlobalHelpers.Address;
        }

        public void ChangeToLocalHost()
        {
            this.BaseUrl = RapGlobalHelpers.LocalHostAddress;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the base URL for the Web Api.
        /// </summary>
        /// <value>
        ///     The base URL.
        /// </value>
        protected string BaseUrl
        {
            get
            {
                return this._baseUrl + "api/"; 
            }
            private set { this._baseUrl = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the entire set from controller from web api.
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            return string.Format("{0}{1}/", BaseUrl, _controllerName);
        }

        /// <summary>
        ///     Gets the controller from web api specific entry
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string Get(int id)
        {
            return string.Format("{0}{1}/{2}", BaseUrl, _controllerName, id);
        }

        /// <summary>
        ///     Gets the by action.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <returns></returns>
        public string GetByAction(string actionName)
        {
            return string.Format("{0}{1}/{2}/", BaseUrl, _controllerName, actionName);
        }

        /// <summary>
        ///     Gets the by action.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <returns></returns>
        public string GetByAction(int id, string actionName)
        {
            return string.Format("{0}{1}/{2}/{3}", BaseUrl, _controllerName, actionName, id);
        }

        /// <summary>
        ///     Posts this instance.
        /// </summary>
        /// <returns></returns>
        public string Post()
        {
            return string.Format("{0}{1}/", BaseUrl, _controllerName);
        }

        /// <summary>
        /// Posts the by action.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <returns></returns>
        public string PostByAction(string actionName)
        {
            return string.Format("{0}{1}/{2}", BaseUrl, _controllerName, actionName);
        }

        /// <summary>
        ///     Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string Put(int id)
        {
            return string.Format("{0}{1}/{2}", BaseUrl, _controllerName, id);
        }

        /// <summary>
        /// Puts the by action.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <returns></returns>
        public string PutByAction(int id, string actionName)
        {
            return string.Format("{0}{1}/{2}/{3}", BaseUrl, _controllerName, actionName, id);
        }

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string Delete(int id)
        {
            return string.Format("{0}{1}/{2}", BaseUrl, _controllerName, id);
        }

        public string DeleteByAction(int id, string action)
        {
            return string.Format("{0}{1}/{2}/{3}", BaseUrl, _controllerName, action, id);
        }

        #endregion
    }
}