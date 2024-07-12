using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetAllTourCommentsQueryHandler: IRequestHandler<GetAllTourCommentQuery, ServiceResponse<List<TourCommentDto>>>
    {
        public readonly ITourCommentRepository _TourCommentRepository;
        public readonly IMapper _mapper;

        public GetAllTourCommentsQueryHandler(ITourCommentRepository TourCommentRepository, IMapper mapper)
        {
            _TourCommentRepository = TourCommentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<TourCommentDto>>> Handle(GetAllTourCommentQuery request, CancellationToken cancellationToken)
        {
            var TourComments = await _TourCommentRepository.AllIncluding(x => x.ActiveTour).ToListAsync();
            return ServiceResponse<List<TourCommentDto>>.ReturnResultWith200(_mapper.Map<List<TourCommentDto>>(TourComments));
        }
    }
}
