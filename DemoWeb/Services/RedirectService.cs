using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace DemoWeb.Services
{
    public interface IRedirectService
    {
        /// <summary>
        /// RedirectNormal
        /// </summary>
        /// <param name="router"></param>
        void RedirectNormal(string router);
        /// <summary>
        /// RedirectForce
        /// </summary>
        /// <param name="router"></param>
        void RedirectForce(string router);
        /// <summary>
        /// RedirectParameter
        /// </summary>
        /// <param name="router"></param>
        /// <param name="passData"></param>
        void RedirectParameter(string router, Dictionary<string, string> passData);
        /// <summary>
        /// RedirectForceParameter
        /// </summary>
        /// <param name="router"></param>
        /// <param name="passData"></param>
        void RedirectForceParameter(string router, Dictionary<string, string> passData);
        /// <summary>
        /// Get base uri
        /// </summary>
        string GetBaseURI();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Uri GetURI();
    }

    public class RedirectService : IRedirectService
    {
        private readonly NavigationManager _NavigManager;

        public RedirectService(
            NavigationManager NavigManager
        )
        {
            _NavigManager = NavigManager;
        }

        /// <summary>
        /// RedirectNormal
        /// </summary>
        /// <param name="router"></param>
        public void RedirectNormal(string router)
        {
            _NavigManager.NavigateTo(router);
        }
        /// <summary>
        /// RedirectForce
        /// </summary>
        /// <param name="router"></param>
        public void RedirectForce(string router)
        {
            _NavigManager.NavigateTo(router, forceLoad: true);
        }
        /// <summary>
        /// RedirectForceParameter
        /// </summary>
        /// <param name="router"></param>
        /// <param name="passData"></param>
        public void RedirectForceParameter(string router, Dictionary<string, string> passData)
        {
            _NavigManager.NavigateTo(QueryHelpers.AddQueryString(router, passData), forceLoad: true);
        }
        /// <summary>
        /// RedirectParameter
        /// </summary>
        /// <param name="router"></param>
        /// <param name="passData"></param>
        public void RedirectParameter(string router, Dictionary<string, string> passData)
        {
            _NavigManager.NavigateTo(QueryHelpers.AddQueryString(router, passData));
        }

        public Uri GetURI()
        {
            return _NavigManager.ToAbsoluteUri(_NavigManager.Uri);
        }

        public string GetBaseURI()
        {
            return _NavigManager.BaseUri;
        }
    }
}
