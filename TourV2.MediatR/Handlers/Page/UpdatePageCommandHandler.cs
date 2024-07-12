using AutoMapper;
using TourV2.Common.UnitOfWork;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.MediatR.Commands;
using TourV2.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using Microsoft.Extensions.Logging;

namespace TourV2.MediatR.Handlers
{
    public class UpdatePageCommandHandler : IRequestHandler<UpdatePageCommand, ServiceResponse<PageDto>>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePageCommandHandler> _logger;
        public UpdatePageCommandHandler(
           IPageRepository pageRepository,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            ILogger<UpdatePageCommandHandler> logger
            )
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<PageDto>> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _pageRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id)
                .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Page Name Already Exist.");
                return ServiceResponse<PageDto>.Return409("Page Name Already Exist.");
            }
            entityExist = await _pageRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync();
            entityExist.Name = request.Name;
            entityExist.Url = request.Url;
            _pageRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PageDto>.Return500();
            }
            return ServiceResponse<PageDto>.ReturnResultWith200(_mapper.Map<PageDto>(entityExist));
        }
    }
}
