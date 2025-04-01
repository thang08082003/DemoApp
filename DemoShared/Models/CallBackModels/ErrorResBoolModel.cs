using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoShared.Constants;

namespace DemoShared.Models.CallBackModels
{
    public class ErrorResBoolModel
    {
        /// <summary>
        /// Results returned from the server
        /// </summary>
        public bool resIsSuccess { get; set; } = false;

        /// <summary>
        /// resMess
        /// </summary>
        public string resMess { get; set; } /*= Messages.Error.MSGE9002(_process);*/

        /// <summary>
        /// messKind
        /// </summary>
        /// <param name="response"></param>
        /// <param name="process">
        /// Register, update, delete, retrieve
        /// </param>
        public bool messKind(HttpResponseMessage? response, string process = null)
        {
            // No Internet Check
            if (response is null)
            {
                // Determining if an error has occurred
                this.resIsSuccess = false;
                // Message Judgment
                this.resMess = Messages.Error.MSGE9002("NULL");
                //Data return
                return false;
            }
            // Check the HTTP status code
            var _statusCode = response.StatusCode;
            int errorCode = (int)_statusCode;

            // 404 not found
            if (errorCode >= 400 && errorCode <= 499)
            {
                // Determining if an error has occurred
                this.resIsSuccess = false;
                // Message Judgment
                this.resMess = Messages.Error.MSGE9002(errorCode.ToString());
                return false;
            }

            // 500 internal exception
            if (errorCode == 500)
            {
                // Determining if an error has occurred
                this.resIsSuccess = false;
                // Message Judgment
                this.resMess = Messages.Error.MSGE9002(errorCode.ToString());
                // Data return
               
                return false;
            }

            return true;
        }
    }
}
