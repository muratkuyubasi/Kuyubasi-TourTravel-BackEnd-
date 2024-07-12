using AutoMapper;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Data.Entities;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;

namespace TourV2.Admin.Helpers.Mapping
{
    public class TourProfile: Profile
    {
        public TourProfile()
        {
            CreateMap<TourV2.Data.Tour, TourDto>().ReverseMap();
            CreateMap<TourRecord, TourRecordDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryRecord, CategoryRecordDto>().ReverseMap();
            CreateMap<Period, PeriodDto>().ReverseMap();
            CreateMap<PeriodRecord, PeriodRecordDto>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<RegionRecord, RegionRecordDto>().ReverseMap();
            CreateMap<Departure, DepartureDto>().ReverseMap();
            CreateMap<DepartureRecord, DepartureRecordDto>().ReverseMap();

            CreateMap<TourCommentDto, TourComment>();
            CreateMap<TourComment, TourCommentDto>();
            CreateMap<TourCommentDto, TourComment>().ReverseMap();
            CreateMap<AddTourCommentCommand, TourComment>().ReverseMap();
            CreateMap<AddTourCommentCommand, TourCommentDto>().ReverseMap();
            CreateMap<DeleteTourCommentCommand, TourCommentDto>().ReverseMap();
            CreateMap<GetAllTourCommentQuery, TourCommentDto>().ReverseMap();
            CreateMap<GetTourCommentQuery, TourCommentDto>().ReverseMap();

            CreateMap<AddTourClickCommand, TourClick>().ReverseMap();
            CreateMap<TourClick, TourClickDto>().ReverseMap();
            CreateMap<CostCalculation, CostCalculationDto>().ReverseMap();
            CreateMap<CostCalculation, AddCostCalculationCommand>().ReverseMap();



        }
    }
}
