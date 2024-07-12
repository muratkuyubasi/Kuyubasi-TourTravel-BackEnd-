using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetAllContactMessagesQueryHandler: IRequestHandler<GetAllContactMessageQuery, ServiceResponse<List<ContactMessageDto>>>
    {
        public readonly IContactMessageRepository _contactMessageRepository;
        public readonly IMapper _mapper;

        public GetAllContactMessagesQueryHandler(IContactMessageRepository ContactMessageRepository, IMapper mapper)
        {
            _contactMessageRepository = ContactMessageRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ContactMessageDto>>> Handle(GetAllContactMessageQuery request, CancellationToken cancellationToken)
        {
            var contactMessages = await _contactMessageRepository.All.ToListAsync();
            return ServiceResponse<List<ContactMessageDto>>.ReturnResultWith200(_mapper.Map<List<ContactMessageDto>>(contactMessages.Where(x => x.Type == 1)));
        }
    }
}
