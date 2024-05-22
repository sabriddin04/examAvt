using AutoMapper;
using Domain.Responses;
using Domain.DTOs.PaymentDto;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Domain.Filters;
using System.Data.Common;

namespace Infrastructure.Services.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PaymentService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> CreatePaymentAsync(CreatePayment Payment)
    {
        try
        {
            var mapped = _mapper.Map<Payment>(Payment);

            await _context.Payments.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Payment");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeletePaymentAsync(int id)
    {
        try
        {
            var Payments = await _context.Payments.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (Payments == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Payments not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetPayment>> GetPaymentByIdAsync(int id)
    {
        try
        {
            var Payments = await _context.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (Payments == null)
                return new Response<GetPayment>(HttpStatusCode.BadRequest, "Payment not found");
            var mapped = _mapper.Map<GetPayment>(Payments);
            return new Response<GetPayment>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetPayment>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetPayment>>> GetPaymentsAsync(PaginationFilter filter)
    {
        try
        {
            var Payments = _context.Payments.AsQueryable();

            var response = await Payments
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = Payments.Count();

            var mapped = _mapper.Map<List<GetPayment>>(response);
            return new PagedResponse<List<GetPayment>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetPayment>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetPayment>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }



    public async Task<Response<string>> UpdatePaymentAsync(UpdatePayment Payment)
    {
        try
        {
            var mapped = _mapper.Map<Payment>(Payment);
            _context.Payments.Update(mapped);
            var update = await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "Payments not found");
            return new Response<string>("Payments updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
