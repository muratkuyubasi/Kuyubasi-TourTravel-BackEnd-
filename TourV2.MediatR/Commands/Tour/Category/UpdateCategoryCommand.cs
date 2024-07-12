using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class UpdateCategoryCommand : IRequest<ServiceResponse<CategoryRecordDto>>
    {
        public Guid Code { get; set; }
        public int CategoryId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string LanguageCode { get; set; }
        public bool ShowCase { get; set; }
        public bool IsActive { get; set; }
    }
}
