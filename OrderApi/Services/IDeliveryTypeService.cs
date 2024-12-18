using OrderApi.Models;
using OrderApi.Models.Extensions;

namespace OrderApi.Services
{
    public interface IDeliveryTypeService
    {
        Task<PaginatedResult<DeliveryType>> GetDeliveryTypesAsync(int pageNumber, int pageSize);
        Task<DeliveryTypeDto> GetDeliveryTypeByIdAsync(Guid orderId);
        Task<DeliveryTypeDto> CreateDeliveryTypeAsync(DeliveryTypeDto deliveryTypeDto);
        Task<DeliveryTypeDto> UpdateDeliveryTypeAsync(DeliveryTypeDto deliveryTypeDto);
        Task<bool> DeleteDeliveryTypeAsync(Guid id);
    }
}
