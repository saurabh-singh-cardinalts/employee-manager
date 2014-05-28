namespace EM.Framework
{
    internal class Constants
    {
        //Caching
        public const double DefaultCachingTimeinMSec = 180000; //30*60*1000

        //Routing /Dispatcher
        public const string VersionPrefix = "V";
        public const int DefaultVersionId = 1;

        public const string AreaKey = "area";
        public const string SectionKey = "section";
        public const string ControllerKey = "controller";
        public const string VersionKey = "version";
        public const string ApiVersionHeaderName = "X-Api-Version";
    }
}
