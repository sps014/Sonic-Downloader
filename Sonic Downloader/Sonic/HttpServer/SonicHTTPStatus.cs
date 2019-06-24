namespace Sonic.HttpServer
{
    public enum StatusCode
    {
        CONTINUE = 100,
        SWITCHING_PROTOCOLS = 101,
        PROCESSING = 102,
        OK = 200,
        CREATED = 201,
        ACCEPTED = 202,
        NON_AUTHORITATIVE_INFORMATION = 203,
        NO_CONTENT = 204,
        RESET_CONTENT = 205,
        PARTIAL_CONTENT = 206,
        MULTIPLE_CHOICES = 300,
        MOVED_PERMANENTLY = 301,
        FOUND = 302,
        SEE_OTHER = 303,
        NOT_MODIFIED = 304,
        USE_PROXY = 305,
        TEMPORARY_REDIRECT = 307,
        BAD_REQUEST = 400,
        UNAUTHORIZED = 401,
        PAYMENT_REQUIRED = 402,
        FORBIDDEN = 403,
        NOT_FOUND = 404,
        METHOD_NOT_ALLOWED = 405,
        NOT_ACCEPTABLE = 406,
        PROXY_AUTHENTICATION_REQUIRED = 407,
        REQUEST_TIMEOUT = 408,
        CONFLICT = 409,
        GONE = 410,
        LENGTH_REQUIRED = 411,
        PRECONDITION_FAILED = 412,
        REQUEST_ENTITY_TOO_LARGE = 413,
        REQUEST_URI_TOO_LARGE = 414,
        UNSUPPORTED_MEDIA_TYPE = 415,
        REQUEST_RANGE_NOT_SATISFIABLE = 416,
        EXPECTATION_FAILED = 417,
        INTERNAL_SERVER_ERROR = 500,
        NOT_IMPLEMENTED = 501,
        BAD_GATEWAY = 502,
        SERVICE_UNAVAILABLE = 503,
        GATEWAY_TIMEOUT = 504,
        HTTP_VERSON_NOT_SUPPORTED = 505
    }

    public static class SonicHTTPStatus
    {

        public static string StatusToString(StatusCode code)
        {
            switch (code)
            {
                case StatusCode.CONTINUE:
                    return "Continue";
                case StatusCode.SWITCHING_PROTOCOLS:
                    return "Switching Protocols";
                case StatusCode.CREATED:
                    return "Created";
                case StatusCode.ACCEPTED:
                    return "Accepted";
                case StatusCode.NON_AUTHORITATIVE_INFORMATION:
                    return "Non-Autoritative Information";
                case StatusCode.NO_CONTENT:
                    return "No Content";
                case StatusCode.RESET_CONTENT:
                    return "Reset Content";
                case StatusCode.PARTIAL_CONTENT:
                    return "Partial Content";
                case StatusCode.MULTIPLE_CHOICES:
                    return "Multiple Choices";
                case StatusCode.MOVED_PERMANENTLY:
                    return "Moved Permanently";
                case StatusCode.FOUND:
                    return "Found";
                case StatusCode.SEE_OTHER:
                    return "See Other";
                case StatusCode.NOT_MODIFIED:
                    return "Not Modified";
                case StatusCode.USE_PROXY:
                    return "Use Proxy";
                case StatusCode.TEMPORARY_REDIRECT:
                    return "Temporary Redirect";
                case StatusCode.BAD_REQUEST:
                    return "Bad Request";
                case StatusCode.UNAUTHORIZED:
                    return "Unauthorized";
                case StatusCode.PAYMENT_REQUIRED:
                    return "Payment Required";
                case StatusCode.FORBIDDEN:
                    return "Forbidden";
                case StatusCode.NOT_FOUND:
                    return "Not Found";
                case StatusCode.METHOD_NOT_ALLOWED:
                    return "Method Not Allowed";
                case StatusCode.NOT_ACCEPTABLE:
                    return "Not Acceptable";
                case StatusCode.PROXY_AUTHENTICATION_REQUIRED:
                    return "Proxy Authentication Required";
                case StatusCode.REQUEST_TIMEOUT:
                    return "Request Time-out";
                case StatusCode.CONFLICT:
                    return "Conflict";
                case StatusCode.GONE:
                    return "Gone";
                case StatusCode.LENGTH_REQUIRED:
                    return "Length Required";
                case StatusCode.PRECONDITION_FAILED:
                    return "Precondition Failed";
                case StatusCode.REQUEST_ENTITY_TOO_LARGE:
                    return "Request Entity Too Large";
                case StatusCode.REQUEST_URI_TOO_LARGE:
                    return "Request-uri Too Large";
                case StatusCode.UNSUPPORTED_MEDIA_TYPE:
                    return "Unsupported Media Type";
                case StatusCode.REQUEST_RANGE_NOT_SATISFIABLE:
                    return "Request range not satisfiable";
                case StatusCode.EXPECTATION_FAILED:
                    return "Expectation Failed";
                case StatusCode.INTERNAL_SERVER_ERROR:
                    return "Internal Server Error";
                case StatusCode.NOT_IMPLEMENTED:
                    return "Not Implemented";
                case StatusCode.BAD_GATEWAY:
                    return "Bad Gateway";
                case StatusCode.SERVICE_UNAVAILABLE:
                    return "Service Unavailable";
                case StatusCode.GATEWAY_TIMEOUT:
                    return "Gateway Time-out";
                case StatusCode.HTTP_VERSON_NOT_SUPPORTED:
                    return "HTTP Version not supported";
                case StatusCode.OK:
                    return "OK";
                default:
                    return null;
            }
        }
    }
}
