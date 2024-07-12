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
    public class GetContactMessageQueryHandler: IRequestHandler<GetContactMessageQuery, ServiceResponse<ContactMessageDto>>
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IMapper _mapper;

        public GetContactMessageQueryHandler(IContactMessageRepository ContactMessageRepository,
            IMapper mapper)
        {
            _contactMessageRepository = ContactMessageRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ContactMessageDto>> Handle(GetContactMessageQuery request, CancellationToken cancellationToken)
        {
            var contactMessage = _contactMessageRepository.Find(request.Id);
            if(contactMessage != null)
            {
                return ServiceResponse<ContactMessageDto>.ReturnResultWith200(_mapper.Map<ContactMessageDto>(contactMessage));
            }
            else
            {
                return ServiceResponse<ContactMessageDto>.Return404();
            }
        }
    }
}
