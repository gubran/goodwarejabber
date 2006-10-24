using System;
using System.Collections.Generic;
using System.Text;

namespace Goodware.Jabber.Library
{
    /// Поставување на пакет со грешка, мислам дека нема да има проблеми :)
    public class ErrorTool
    {
        static public String getCode(int codeNumber)
        {
            switch (codeNumber)
            {
                case 302: return "Redirect";
                case 400: return "Bad Request";
                case 401: return "Unauthorized";
                case 402: return "Payment Required";
                case 403: return "Forbidden";
                case 404: return "Not Found";
                case 405: return "Not Allowed";
                case 406: return "Not Acceptable";
                case 407: return "Registration Required";
                case 408: return "Request Timeout";
                case 409: return "Conflict";
                case 500: return "Internal Server Error";
                case 501: return "Not Implemented";
                case 502: return "Remote Server Error";
                case 503: return "Service Unavailable";
                case 504: return "Remote Server Timeout";
                default: return "Unknown error code";
            }
        }

        static public void setError(Packet packet, int code, String message)
        {
            packet.Type = "error";
            if (message == null)
            {
                message = getCode(code);
            }

            Packet e = new Packet("error",message);
            e["code"] = Convert.ToString(code);
            e.Parent = packet;
        }
    }
}
