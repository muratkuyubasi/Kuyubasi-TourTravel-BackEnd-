using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.MediatR.Queries.EducationForm;
using TourV2.MediatR.Queries.Mosque;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.Mosque
{
    public class GetAllMosqueQueryHandler : IRequestHandler<GetAllMosqueQuery, ServiceResponse<List<MosqueDto>>>
    {
        public readonly IMosqueRepository _mosqueRepository;
        public readonly IMapper _mapper;

        public GetAllMosqueQueryHandler(IMosqueRepository MosqueRepository, IMapper mapper)
        {
            _mosqueRepository = MosqueRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<MosqueDto>>> Handle(GetAllMosqueQuery request, CancellationToken cancellationToken)
        {
            var contactMessages = await _mosqueRepository.All.ToListAsync();
            return ServiceResponse<List<MosqueDto>>.ReturnResultWith200(_mapper.Map<List<MosqueDto>>(contactMessages));
        }
    }
}