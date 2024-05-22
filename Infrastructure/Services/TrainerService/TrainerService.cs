using System.Net;
using Domain.DTOs.TrainerDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Services.FileService;
using Infrastructure.Services.TrainerService;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.BookService;

public class TrainerService(DataContext context, IFileService fileService) : ITrainerService
{
  
    public async Task<PagedResponse<List<GetTrainer>>> GetTrainersAsync(TrainerFilter filter)
    {
        try
        {
            var books = context.Trainers.Include(x => x.Name).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                books = books.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            if (!string.IsNullOrEmpty(filter.Email))
                books = books.Where(x => x.Email!.ToLower().Contains(filter.Email.ToLower()));


            if (filter.Name != null)
                books = books.Where(x => x.Name == filter.Name);
            if (filter.Email != null)
                books = books.Where(x => x.Email == filter.Email);
            

            var response = await books.Select(x => new GetTrainer()
                {
                    Name =x.Name,
                    Specialization=x.Specialization,
                    Email=x.Email,
                    Phone=x.Phone,
                    PathPhoto=x.PathPhoto,

                }).Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            var totalRecord = await books.CountAsync();

            return new PagedResponse<List<GetTrainer>>(response, totalRecord, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetTrainer>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

  

    public async Task<Response<GetTrainer>> GetTrainerByIdAsync(int id)
    {
        try
        {
            var existing = await context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetTrainer>(HttpStatusCode.BadRequest, "Not Found");
            var response = new GetTrainer()
            {
                 Name =existing.Name,
                    Specialization=existing.Specialization,
                    Email=existing.Email,
                    Phone=existing.Phone,
                    PathPhoto=existing.PathPhoto,
            };
            return new Response<GetTrainer>(response);
        }
        catch (Exception e)
        {
            return new Response<GetTrainer>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

   

    public async Task<Response<string>> CreateTrainerAsync(CreateTrainer trainer)
    {
        try
        {
            var book = new Trainer()
            {
                Name=trainer.Name,
                Specialization=trainer.Specialization,
                Email=trainer.Email,
                Phone=trainer.Phone,
                PathPhoto= await fileService.CreateFile(trainer.PathPhoto),
                UpdatedAt=DateTimeOffset.UtcNow,
                CreatedAt=DateTimeOffset.UtcNow,
            };
            await context.Trainers.AddAsync(book);
            await context.SaveChangesAsync();
            return new Response<string>("Successfully created Trainer");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

   

    public async Task<Response<string>> UpdateTrainerAsync(UpdateTrainer trainer)
    {
        try
        {
            var existingTrainer = await context.Trainers.FirstOrDefaultAsync(x => x.Id == trainer.Id);
            if (existingTrainer == null) return new Response<string>(HttpStatusCode.BadRequest, "Trainer not found");

            if (trainer.PathPhoto != null)
            {
                fileService.DeleteFile(existingTrainer.PathPhoto);
                existingTrainer.PathPhoto = await fileService.CreateFile(trainer.PathPhoto);
            }

            existingTrainer.Name=trainer.Name;
            existingTrainer.Phone=trainer.Phone;
            existingTrainer.Email=trainer.Email;
            existingTrainer.Id=trainer.Id;

            existingTrainer.UpdatedAt = DateTimeOffset.UtcNow;
            
            await context.SaveChangesAsync();
            return new Response<string>("Successfully updated the Trainer");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<bool>> DeleteTrainerAsync(int id)
    {
        try
        {
            var existing = await context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<bool>(HttpStatusCode.BadRequest, "Not Found");
            fileService.DeleteFile(existing.PathPhoto);
            context.Trainers.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    
}