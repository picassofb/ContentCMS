using Microsoft.AspNetCore.Antiforgery;
using ContentCMS.Controllers;

namespace ContentCMS.Web.Host.Controllers
{
    public class AntiForgeryController : ContentCMSControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
