using TourV2.Common.UnitOfWork;
using TourV2.Repository;
using Microsoft.Extensions.DependencyInjection;
using TourV2.Data;
using TourV2.Repository.PeriodEducation;
using TourV2.Repository.Survey;

namespace TourV2.Admin.Helpers
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IPropertyMappingService, PropertyMappingService>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IActionRepository, ActionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IPageActionRepository, PageActionRepository>();
            services.AddScoped<ILoginAuditRepository, LoginAuditRepository>();
            services.AddScoped<IUserAllowedIPRepository, UserAllowedIPRepository>();
            services.AddScoped<IAppSettingRepository, AppSettingRepository>();
            services.AddScoped<INLogRespository, NLogRespository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IEmailSMTPSettingRepository, EmailSMTPSettingRepository>();
            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<ITourRecordRepository, TourRecordRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryRecordRepository, CategoryRecordRepository>();
            services.AddScoped<IPeriodRepository, PeriodRepository>();
            services.AddScoped<IPeriodRecordRepository, PeriodRecordRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IRegionRecordRepository, RegionRecordRepository>();
            services.AddScoped<IDepartureRepository, DepartureRepository>();
            services.AddScoped<IDepartureRecordRepository, DepartureRecordRepository>();
            services.AddScoped<IActiveTourRepository, ActiveTourRepository>();
            services.AddScoped<ITourCategoryRepository, TourCategoryRepository>();
            services.AddScoped<ITourDepartureRepository, TourDepartureRepository>();
            services.AddScoped<ITourDayRepository, TourDayRepository>();
            services.AddScoped<ITourPriceRepository, TourPriceRepository>();
            services.AddScoped<ITourMediaRepository, TourMediaRepository>();
            services.AddScoped<ITourSpecificationRepository, TourSpecificationRepository>();
            services.AddScoped<ITourTransportationRepository, TourTransportationRepository>();
            services.AddScoped<ITourReservationRepository, TourReservationRepository>();
            services.AddScoped<ITourReservationPersonRepository, TourReservationPersonRepository>();
            services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
            services.AddScoped<INewsletterSubscriptionRepository, NewsletterSubscriptionRepository>();
            services.AddScoped<ITourCommentRepository, TourCommentRepository>();
            services.AddScoped<ITourClickRepository, TourClickRepository>();
            services.AddScoped<IFrontAnnouncementRepository, FrontAnnouncementRepository>();
            services.AddScoped<IFrontAnnouncementRecordRepository, FrontAnnouncementRecordRepository>();
            services.AddScoped<ICostCalculationRepository, CostCalculationRepository>();
            services.AddScoped<IEducationFormRepository, EducationFormRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IMosqueRepository, MosqueRepository>();
            services.AddScoped<IPeriodEducationRepository, PeriodEducationRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();

        }
    }
}
