namespace AutofacGenericAPI
{
    using System.Web.Http;

    public class Bootstrapper
    {
        public static void Run()
        {
            IocConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}