using System.Web.Http.ExceptionHandling;

namespace PlaySimple.Common
{
    // TODO: decide if we want this - this is supposed to log errors to db or so
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
        }
    }
}