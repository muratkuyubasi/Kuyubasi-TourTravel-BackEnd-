using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Helper;
using TourV2.MediatR.Queries.Mosque;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.Mosque
{
    public class GetAllMosqueByStateIdQueryHandler : IRequestHandler<GetAllMosqueByStateIdQuery, ServiceResponse<List<MosqueDto>>>
    {
        public readonly IMosqueRepository _mosqueRepository;
        public readonly IMapper _mapper;

        public GetAllMosqueByStateIdQueryHandler(IMosqueRepository MosqueRepository, IMapper mapper)
        {
            _mosqueRepository = MosqueRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<MosqueDto>>> Handle(GetAllMosqueByStateIdQuery request, CancellationToken cancellationToken)
        {
            var contactMessages = await _mosqueRepository.All.Where(x =>x.StateId == request.StateId).ToListAsync();
            return ServiceResponse<List<MosqueDto>>.ReturnResultWith200(_mapper.Map<List<MosqueDto>>(contactMessages));
        }
    }
}