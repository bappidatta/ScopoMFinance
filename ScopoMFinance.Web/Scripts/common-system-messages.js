var SystemMessageType;
(function (SystemMessageType) {
    /// <summary>
    /// Enum for system message types
    /// </summary>
    /// <param name="SystemMessageType"> type parameter </param>

    SystemMessageType[SystemMessageType["Success"] = 1] = "Success";
    SystemMessageType[SystemMessageType["Info"] = 2] = "Info";
    SystemMessageType[SystemMessageType["Warning"] = 3] = "Warning";
    SystemMessageType[SystemMessageType["Error"] = 4] = "Error";
})(SystemMessageType || (SystemMessageType = {}));



var XHR_JSON_Result = (function () {
    /// <summary>
    /// XHR_JSON_Result class
    /// </summary>

    function XHR_JSON_Result(data, messages) {
        this.data = data;
        this.messages = messages;
    }
    return XHR_JSON_Result;
})();



var SystemMessage = (function () {
    /// <summary>
    /// Js SystemMessage class must match Core.Common.SystemMessage
    /// </summary>

    function SystemMessage(Text, IsError, MessageType, IsRedirectMsg, IsRedirectActive) {
        /// <summary>
        /// SystemMessage constructor function
        /// </summary>
        /// <param name="Text"> message to be displayed </param>
        /// <param name="IsError"> is the message an error report </param>
        /// <param name="MessageType"> type of the message </param>
        /// <param name="IsRedirectMsg"> true if this message should persist over a redirect </param>
        /// <param name="IsRedirectActive"> true if this message is going to be persisted over a redirect  </param>

        this.Text = Text;
        this.IsError = IsError;
        this.MessageType = MessageType;
        this.IsRedirectMsg = IsRedirectMsg;
        this.IsRedirectActive = IsRedirectActive;
    };


    SystemMessage.Display = function (list) {
        /// <summary>
        /// Function displays messages using the toastr plugin
        /// </summary>
        /// <param name="list"> list of system messages </param>

        if (Object.prototype.toString.call(list) !== '[object Array]') {
            window.console && window.console.log && window.console.log("Error: Tried to call SystemMessage.Display with not array", JSON.stringify(arguments))
            return;
        }

        for (var i = 0; i < list.length; i++) {
            switch (list[i].MessageType) {
                case SystemMessageType.Success:
                    toastr.success(list[i].Text);
                    break;
                case SystemMessageType.Info:
                    toastr.info(list[i].Text);
                    break;
                case SystemMessageType.Warning:
                    toastr.warning(list[i].Text);
                    break;
                case SystemMessageType.Error:
                    toastr.error(list[i].Text);
                    break;
            }

            // Need to record that the message was displayed
            if (list !== __SYSTEMMESSAGES)
                __SYSTEMMESSAGES.push(list[i]);

            if (!(list[i] && list[i].MessageType))
                window.console && window.console.log && window.console.log("Error: Tried to call SystemMessage.Display with object without message type", JSON.stringify(arguments))
        }
    };


    SystemMessage.BindAjaxSuccessDefaultHandler = function () {
        /// <summary>
        /// Binds a global handler to the AJAX Success event. 
        /// The handler parses and displays system messages, or redirects to a specified RedirectURL.
        /// </summary>

        $(document).ajaxSuccess(SystemMessage.SuccessRoutine);
    };

    SystemMessage.AngularInterceptor = ['$q', function ($q) {
        /// <summary>
        /// Defines basic angular response interceptor that will render success or fail of angular response in toastr dialog
        /// </summary>
        return {
            'response': function (response) {
                if (typeof (response.data) === "object" && response.data.messages) {
                    SystemMessage.Display(response.data.messages);
                    response.data.messages = [];
                }
                return response;
            },
            'responseError': function (rejection) {
                SystemMessage.Display([new SystemMessage("The request <i>" + rejection.config.url + "</i> could not be completed: " + rejection.status + "\nParameters: " + JSON.stringify(rejection.config.params), true, SystemMessageType.Error)]);
                return rejection;
            }
        }
    }];

    SystemMessage.SuccessRoutine = function (event, xhr, settings, parsedResponse) {

        // check if data is in JSON format 
        // we need to check what was requested but also the content-type of the response
        if (settings.dataTypes.indexOf('json') > -1 || xhr.getResponseHeader("content-type").indexOf('application/json') > -1) {
            var xhr_response = $.parseJSON(xhr.responseText);

            if (typeof xhr_response.messages !== "undefined")
                SystemMessage.Display(xhr_response.messages);

            if (typeof xhr_response.code !== "undefined" && xhr_response.code === 302) {
                if (xhr_response.data.RedirectURL == "")
                    window.location.reload()
                else
                    window.location = xhr_response.data.RedirectURL;
            }
        }

        // check if data is in HTML format
        if (settings.dataTypes.indexOf('html') > -1) {

            // reset the submit button state for the form that was posted
            $('.__Submit', 'form[action="' + settings.url + '"]').button('reset');
        }
    };


    SystemMessage.BindAjaxErrorDefaultHandler = function () {
        /// <summary>
        /// Binds a global handler to the AJAX Error event. A toastr message will be displayed for the user.
        /// </summary>

        $(document).ajaxError(function (event, xhr, settings) {
            SystemMessage.Display([new SystemMessage("The request <i>" + settings.url + "</i> could not be completed: " + xhr.status + " " + xhr.statusText, true, SystemMessageType.Error)]);
        });
    };

    return SystemMessage;
})();




// Bind auto-message-display for successful ajax requests that return XHR_JSON_Result, and for failed ajax requests in general
SystemMessage.BindAjaxSuccessDefaultHandler();
SystemMessage.BindAjaxErrorDefaultHandler();


$(function () {
    // Display system messages from server
    SystemMessage.Display(__SYSTEMMESSAGES);
});