using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoShared.Constants;

namespace DemoShared.Models.CallBackModels
{
    public class ErrorResModel<T> where T : class
    {
        /// <summary>
        /// Result returned from the server
        /// </summary>
        public bool resIsSuccess { get; set; } = true;

        /// <summary>
        /// Returned data
        /// </summary>
        public T resData { get; set; }

        /// <summary>
        /// resMess
        /// </summary>
        public string resMess { get; set; }

        /// <summary>
        /// messKind
        /// </summary>
        /// <param name="response"></param>
        /// <param name="process">
        /// Registration, update, deletion, retrieval
        /// </param>
        public bool messKind(HttpResponseMessage? response, string process = null)
        {
            // Check for no internet connection
            if (response is null)
            {
                // Determine error occurrence
                this.resIsSuccess = false;
                // Determine message
                this.resMess = Messages.Error.MSGE9002("NULL");
                // Return data
                return false;
            }

            // Check HTTP status code
            var _statusCode = response.StatusCode;
            int errorCode = (int)_statusCode;

            // 404 not found
            if (errorCode >= 400 && errorCode <= 499)
            {
                // Determine error occurrence
                this.resIsSuccess = false;
                // Determine message
                this.resMess = Messages.Error.MSGE9002(errorCode.ToString());
                return false;
            }

            // 500 internal exception
            if (errorCode == 500)
            {
                // Determine error occurrence
                this.resIsSuccess = false;
                // Determine message
                this.resMess = Messages.Error.MSGE9002(errorCode.ToString());
                // Return data
                return false;
            }

            return true;
        }
    }
}