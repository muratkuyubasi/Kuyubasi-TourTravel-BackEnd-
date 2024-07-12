using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddActiveTourMediaCommand : IRequest<ServiceResponse<TourMedia>>
    {
        
        public int Id { get; set; }
        public int ActiveTourId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
        public bool IsCover { get; set; }
        public bool IsActive { get; set; }
        public IFormFileCollection FormFile { get; set; }
        public string RootPath { get; set; }

    }
}
