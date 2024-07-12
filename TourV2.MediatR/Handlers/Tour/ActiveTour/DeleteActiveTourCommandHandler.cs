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
    public class DeleteActiveTourCommandHandler : IRequestHandler<DeleteActiveTourCommand, ServiceResponse<ActiveTourDto>>
    {
        private readonly IActiveTourRepository _tourRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly UserInfoToken _userInfoToken;

        public DeleteActiveTourCommandHandler(IActiveTourRepository tourRepository, UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _tourRepository = tourRepository;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<ActiveTourDto>> Handle(DeleteActiveTourCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourRepository.FindBy(x=>x.Id ==request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<ActiveTourDto>.Return404();
            }
            entityExist.IsDeleted = true;
            entityExist.DeletedDate = DateTime.Now.ToLocalTime();
            entityExist.DeletedBy = Guid.Parse(_userInfoToken.Id);
           
            _tourRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ActiveTourDto>.Return500();
            }
            return ServiceResponse<ActiveTourDto>.ReturnResultWith204();
        }
    }
}
