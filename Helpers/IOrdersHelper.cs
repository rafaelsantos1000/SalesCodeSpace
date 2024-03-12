using SalesCodeSpace.Responses;

namespace SalesCodeSpace.Helpers;

public interface IOrdersHelper
{
    Task<Response> ProcessOrderAsync(ShowCartViewModel model);
}
