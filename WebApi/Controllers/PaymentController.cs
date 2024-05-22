using Domain.DTOs.PaymentDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Seed;

namespace WEBAPI.Cotrollers;
[ApiController]
[Route("[controller]")]

public class PaymentController(IPaymentService paymentService)
{
    [HttpGet]
   [Authorize(Roles.Admin)]
    public async Task<Response<List<GetPayment>>> GetPaymentsAsync([FromQuery]PaginationFilter PaymentFilter)
        => await paymentService.GetPaymentsAsync(PaymentFilter);

    [HttpGet("{PaymentId:int}")]
    [Authorize(Roles.Admin)]
    public async Task<Response<GetPayment>> GetPaymentByIdAsync(int PaymentId)
        => await paymentService.GetPaymentByIdAsync(PaymentId);

    [HttpPost("create")]
    [Authorize]
    public async Task<Response<string>> CreatePaymentAsync([FromBody]CreatePayment Payment)
        => await paymentService.CreatePaymentAsync(Payment);


    [HttpPut("update")]
     [Authorize(Roles.Admin)]
    public async Task<Response<string>> UpdatePaymentAsync([FromBody]UpdatePayment Payment)
        => await paymentService.UpdatePaymentAsync(Payment);

    [HttpDelete("{PaymentId:int}")]
     [Authorize(Roles.Admin)]
    public async Task<Response<bool>> DeletePaymentAsync(int PaymentId)
        => await paymentService.DeletePaymentAsync(PaymentId);
}
