using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddFrontAnnouncementRecordCommand : IRequest<ServiceResponse<FrontAnnouncementRecordDto>>
    {
        public int FrontAnnouncementId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Link { get; set; }
        [JsonIgnore]
        public string RootPath { get; set; }
        public IFormFile FormFile { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
    }
}
