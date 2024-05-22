using AutoMapper;
using Domain.DTOs.ClassSchduleDto;
using Domain.DTOs.MembershipDto;
using Domain.DTOs.PaymentDto;
using Domain.DTOs.TrainerDto;
using Domain.DTOs.UserDto;
using Domain.DTOs.WorkoutDto;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, CreateUser>().ReverseMap();
        CreateMap<User, GetUser>().ReverseMap();
        CreateMap<User, UpdateUser>().ReverseMap();

        CreateMap<Trainer, CreateTrainer>().ReverseMap();
        CreateMap<Trainer, GetTrainer>().ReverseMap();
        CreateMap<Trainer, UpdateTrainer>().ReverseMap();

        CreateMap<Workout, CreateWorkout>().ReverseMap();
        CreateMap<Workout, GetWorkout>().ReverseMap();
        CreateMap<Workout, UpdateWorkout>().ReverseMap();

        CreateMap<Payment, CreatePayment>().ReverseMap();
        CreateMap<Payment, GetPayment>().ReverseMap();
        CreateMap<Payment, UpdatePayment>().ReverseMap();

        CreateMap<Membership, CreateMembership>().ReverseMap();
        CreateMap<Membership, GetMembership>().ReverseMap();
        CreateMap<Membership, UpdateMembership>().ReverseMap();

        CreateMap<ClassSchedule, CreateClassSchedule>().ReverseMap();
        CreateMap<ClassSchedule, GetClassSchedule>().ReverseMap();
        CreateMap<ClassSchedule, UpdateClassSchedule>().ReverseMap();
    }
}
