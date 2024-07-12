using AutoMapper;
using TourV2.Data;

namespace TourV2.Admin.Helpers.Mapping
{
    public static class MapperConfig
    {
        public static IMapper GetMapperConfigs()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ActionProfile());
                mc.AddProfile(new PageProfile());
                mc.AddProfile(new RoleProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new PageActionProfile());
                mc.AddProfile(new AppSettingProfile());
                mc.AddProfile(new NLogProfile());
                mc.AddProfile(new EmailTemplateProfile());
                mc.AddProfile(new EmailProfile());
                mc.AddProfile(new TourProfile());
                mc.AddProfile(new ActiveTourProfile());
                mc.AddProfile(new ContactProfile());
                mc.AddProfile(new FrontAnnouncementProfile());
                mc.AddProfile(new EducationFormProfile());
                mc.AddProfile(new SurveyProfile());

            });
            return mappingConfig.CreateMapper();
        }
    }
}
