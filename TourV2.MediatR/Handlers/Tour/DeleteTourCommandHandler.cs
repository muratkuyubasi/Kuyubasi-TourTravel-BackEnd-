using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class DeleteTourCommandHandler : IRequestHandler<DeleteTourCommand, ServiceResponse<TourDto>>
    {
        private readonly ITourRepository _tourRepository;
        private readonly ITourRecordRepository _recordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly UserInfoToken _userInfoToken;

        public DeleteTourCommandHandler(ITourRepository tourRepository, ITourRecordRepository tourRecordRepository, UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _tourRepository = tourRepository;
            _recordRepository = tourRecordRepository;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourDto>> Handle(DeleteTourCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourRepository.FindByInclude(x=>x.Id ==request.Id, i=>i.TourRecords).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<TourDto>.Return404();
            }
            entityExist.IsDeleted = true;
            entityExist.DeletedDate = DateTime.Now.ToLocalTime();
            entityExist.DeletedBy = Guid.Parse(_userInfoToken.Id);
            if(entityExist.TourRecords != null)
            {
                foreach (var item in entityExist.TourRecords)
                {
                    item.IsDeleted = true;
                    item.DeletedDate = DateTime.Now.ToLocalTime();
                    item.DeletedBy = Guid.Parse(_userInfoToken.Id);
                    _recordRepository.Update(item);
                }
            }


            _tourRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourDto>.Return500();
            }
            return ServiceResponse<TourDto>.ReturnResultWith204();
        }
    }
}
