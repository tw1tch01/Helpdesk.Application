using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.WebAPI.Common
{
    [ApiController]
    [Route("api/[area]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public abstract class AbstractController : Controller
    {
        protected readonly int _defaultPageSize = 50;

        protected (int page, int pageSize) GetPagination()
        {
            return (GetPage(), GetPageSize());
        }

        #region Private Methods

        private int GetPage()
        {
            Request.Headers.TryGetValue(ApiConfig.PageHeader, out var headerValues);

            var headerValue = headerValues.FirstOrDefault();

            if (headerValue != null && int.TryParse(headerValue, out var pageValue))
            {
                return pageValue;
            }
            else
            {
                return 0;
            }
        }

        private int GetPageSize()
        {
            Request.Headers.TryGetValue(ApiConfig.PageSizeHeader, out var headerValues);

            var headerValue = headerValues.FirstOrDefault();

            if (headerValue != null && int.TryParse(headerValue, out var pageSizeValue))
            {
                return pageSizeValue;
            }
            else
            {
                return _defaultPageSize;
            }
        }

        #endregion Private Methods
    }
}