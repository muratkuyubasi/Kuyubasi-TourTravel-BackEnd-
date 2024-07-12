using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetTourCommentQueryHandler: IRequestHandler<GetTourCommentQuery, ServiceResponse<TourCommentDto>>
    {
        private readonly ITourCommentRepository _TourCommentRepository;
        private readonly IMapper _mapper;

        public GetTourCommentQueryHandler(ITourCommentRepository TourCommentRepository,
            IMapper mapper)
        {
            _TourCommentRepository = TourCommentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourCommentDto>> Handle(GetTourCommentQuery request, CancellationToken cancellationToken)
        {
            var TourComment = _TourCommentRepository.Find(request.Id);
            if(TourComment != null)
            {
                return ServiceResponse<TourCommentDto>.ReturnResultWith200(_mapper.Map<TourCommentDto>(TourComment));
            }
            else
            {
                return ServiceResponse<TourCommentDto>.Return404();
            }
        }
    }
}
