using TourV2.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using TourV2.Helper;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace TourV2.MediatR.Commands
{
    public class UpdateFrontAnnouncementRecordCommand : IRequest<ServiceResponse<FrontAnnouncementRecordDto>>
    {
        public int Id { get; set; }
        public int FrontAnnouncementId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [JsonIgnore]
        public string RootPath { get; set; }
        public IFormFile FormFile { get; set; }
        public string Link { get; set; }
        public string FileUrl { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
    }
}
