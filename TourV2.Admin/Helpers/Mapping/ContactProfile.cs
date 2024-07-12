using AutoMapper;
using TourV2.Data.Dto;
using TourV2.Data;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;

namespace TourV2.Admin.Helpers.Mapping
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactMessageDto, ContactMessage>();
            CreateMap<ContactMessage, ContactMessageDto>();
            CreateMap<ContactMessageDto, ContactMessage>().ReverseMap();
            CreateMap<NewsletterSubscriptionDto, NewsletterSubscription>().ReverseMap();


            CreateMap<AddContactMessageCommand, ContactMessage>().ReverseMap();
            CreateMap<AddContactMessageCommand, ContactMessageDto>().ReverseMap();
            CreateMap<AddTourRequestCommand, ContactMessageDto>().ReverseMap();
            CreateMap<AddTourRequestCommand, ContactMessage>().ReverseMap();
            CreateMap<AddNewsletterSubscriptionCommand, NewsletterSubscription>().ReverseMap();
            CreateMap<AddNewsletterSubscriptionCommand, NewsletterSubscriptionDto>().ReverseMap();

            CreateMap<DeleteContactMessageCommand, ContactMessageDto>().ReverseMap();
            CreateMap<DeleteNewsletterSubscriptionCommand, NewsletterSubscriptionDto>().ReverseMap();

            CreateMap<GetAllContactMessageQuery, ContactMessageDto>().ReverseMap();
            CreateMap<GetAllTourRequestsQuery, ContactMessageDto>().ReverseMap();
            CreateMap<GetAllNewsletterSubscriptionQuery, NewsletterSubscriptionDto>().ReverseMap();

            CreateMap<GetContactMessageQuery, ContactMessageDto>().ReverseMap();
            CreateMap<GetNewsletterSubscriptionQuery, NewsletterSubscriptionDto>().ReverseMap();

        }
    }
}