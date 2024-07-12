using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class AddLogCommandHandler : IRequestHandler<AddLogCommand, ServiceResponse<NLogDto>>
    {
        private readonly INLogRespository _nLogRespository;
        private readonly IUnitOfWork<TourContext> _uow;
        public AddLogCommandHandler(
           INLogRespository nLogRespository,
            IUnitOfWork<TourContext> uow
            )
        {
            _nLogRespository = nLogRespository;
            _uow = uow;
        }
        public async Task<ServiceResponse<NLogDto>> Handle(AddLogCommand request, CancellationToken cancellationToken)
        {
            _nLogRespository.Add(new NLog
            {
                Id = Guid.NewGuid(),
                Logged = DateTime.Now.ToLocalTime(),
                Level = "Error",
                Message = request.ErrorMessage,
                Source = "Angular",
                Exception = request.Stack
            });
            await _uow.SaveAsync();
            return ServiceResponse<NLogDto>.ReturnSuccess();
        }
    }
}
