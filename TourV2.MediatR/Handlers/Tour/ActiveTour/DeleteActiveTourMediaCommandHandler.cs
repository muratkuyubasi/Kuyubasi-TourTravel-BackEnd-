using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    public class DeleteActiveTourMediaCommandHandler : IRequestHandler<DeleteActiveTourMediaCommand, ServiceResponse<TourMedia>>
    {
        private readonly ITourMediaRepository _mediaRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        public readonly PathHelper _pathHelper;


        public DeleteActiveTourMediaCommandHandler(ITourMediaRepository mediaRepository, PathHelper pathHelper, IUnitOfWork<TourContext> uow)
        {
            _mediaRepository = mediaRepository;
            _uow = uow;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<TourMedia>> Handle(DeleteActiveTourMediaCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _mediaRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<TourMedia>.Return404();
            }
            _mediaRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourMedia>.Return500();
            }
            var filePath = $"{request.RootPath}/{_pathHelper.DocumentPath}";
            string fullPath = Path.Combine(filePath, entityExist.FileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);

            }

            return ServiceResponse<TourMedia>.ReturnResultWith204();
        }
    }
}
