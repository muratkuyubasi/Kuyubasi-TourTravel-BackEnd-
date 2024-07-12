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
    public class GetTourCommentByTourMainIdQueryHandler : IRequestHandler<GetTourCommentByTourMainIdQuery, ServiceResponse<List<TourCommentDto>>>
    {
        private readonly ITourCommentRepository _TourCommentRepository;
        private readonly IMapper _mapper;

        public GetTourCommentByTourMainIdQueryHandler(ITourCommentRepository TourCommentRepository,
            IMapper mapper)
        {
            _TourCommentRepository = TourCommentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<TourCommentDto>>> Handle(GetTourCommentByTourMainIdQuery request, CancellationToken cancellationToken)
        {
            var TourComments = await _TourCommentRepository.FindBy(x => x.ActiveTourId == request.ActiveTourId).ToListAsync();

            return ServiceResponse<List<TourCommentDto>>.ReturnResultWith200(_mapper.Map<List<TourCommentDto>>(TourComments));
        }
    }
}
