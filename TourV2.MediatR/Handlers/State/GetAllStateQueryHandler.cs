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
using TourV2.MediatR.Queries.State;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.State
{
    public class GetAllStateQueryHandler : IRequestHandler<GetAllStateQuery, ServiceResponse<List<StateDto>>>
    {
        public readonly IStateRepository _stateRepository;
        public readonly IMapper _mapper;

        public GetAllStateQueryHandler(IStateRepository StateRepository, IMapper mapper)
        {
            _stateRepository = StateRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<StateDto>>> Handle(GetAllStateQuery request, CancellationToken cancellationToken)
        {
            var contactMessages = await _stateRepository.All.ToListAsync();
            return ServiceResponse<List<StateDto>>.ReturnResultWith200(_mapper.Map<List<StateDto>>(contactMessages));
        }
    }
}