using TourV2.Common.UnitOfWork;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.MediatR.Commands;
using TourV2.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;

namespace TourV2.MediatR.Handlers
{
    public class DeletePageCommandHandler : IRequestHandler<DeletePageCommand, ServiceResponse<PageDto>>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        public DeletePageCommandHandler(
           IPageRepository pageRepository,
            IUnitOfWork<TourContext> uow
            )
        {
            _pageRepository = pageRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<PageDto>> Handle(DeletePageCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _pageRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<PageDto>.Return404();
            }
            _pageRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PageDto>.Return500();
            }
            return ServiceResponse<PageDto>.ReturnSuccess();
        }
    }
}
