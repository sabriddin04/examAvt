using Domain.DTOs.PaymentDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.PaymentService;

public interface IPaymentService
{
    Task<PagedResponse<List<GetPayment>>> GetPaymentsAsync(PaginationFilter filter);
    Task<Response<GetPayment>> GetPaymentByIdAsync(int id);
    Task<Response<string>> CreatePaymentAsync(CreatePayment Payment);
    Task<Response<string>> UpdatePaymentAsync(UpdatePayment Payment);
    Task<Response<bool>> DeletePaymentAsync(int id);
}
