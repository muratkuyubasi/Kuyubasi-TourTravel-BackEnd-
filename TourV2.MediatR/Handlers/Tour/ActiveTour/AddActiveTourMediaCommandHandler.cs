using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
    public class AddActiveTourMediaCommandHandler : IRequestHandler<AddActiveTourMediaCommand, ServiceResponse<TourMedia>>
    {
        private readonly ITourMediaRepository _tourMediaRepository;
        public readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public AddActiveTourMediaCommandHandler(ITourMediaRepository tourMediaRepository, PathHelper pathHelper, IUnitOfWork<TourContext> uow, IMapper mapper)
        {

            _tourMediaRepository = tourMediaRepository;
            _pathHelper = pathHelper;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourMedia>> Handle(AddActiveTourMediaCommand request, CancellationToken cancellationToken)
        {
            var entity = new TourMedia();
            var filePath = $"{request.RootPath}/{_pathHelper.DocumentPath}";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // delete existing file
            // save new file
            if (request.FormFile.Any())
            {
                var mediaFile = request.FormFile[0];
                var newMedia = $"{Guid.NewGuid()}{Path.GetExtension(mediaFile.Name)}";
                string fullPath = Path.Combine(filePath, newMedia);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    mediaFile.CopyTo(stream);
                }
                entity.ActiveTourId = Convert.ToInt32(mediaFile.FileName);
                entity.FileName = newMedia;
                entity.FileType = mediaFile.ContentType;
                entity.IsCover = request.IsCover;
                //entity.IsActive = request.IsActive;
                entity.IsActive = true;

                _tourMediaRepository.Add(entity);
                if (await _uow.SaveAsync() <= 0)
                {
                    return ServiceResponse<TourMedia>.Return500();
                }
            }
            var entityDto = _mapper.Map<TourMedia>(entity);
            return ServiceResponse<TourMedia>.ReturnResultWith200(entityDto);
        }


    }
}
