using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    public class AddTourReservationCommandHandler : IRequestHandler<AddTourReservationCommand, ServiceResponse<TourReservationDto>>
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly ITourDepartureRepository _tourDepartureRepository;
        private readonly ITourReservationPersonRepository _tourReservationPersonRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddTourReservationCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public AddTourReservationCommandHandler(ITourReservationRepository tourReservationRepository, ITourReservationPersonRepository tourReservationPersonRepository, IUnitOfWork<TourContext> uow, IMapper mapper, ILogger<AddTourReservationCommandHandler> logger, UserInfoToken userInfoToken, ITourDepartureRepository tourDepartureRepository)
        {
            _tourReservationRepository = tourReservationRepository;
            _tourReservationPersonRepository = tourReservationPersonRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
            _tourDepartureRepository = tourDepartureRepository;
        }

        public async Task<ServiceResponse<TourReservationDto>> Handle(AddTourReservationCommand request, CancellationToken cancellationToken)
        {
            _userInfoToken.Id = "4B352B37-332A-40C6-AB05-E38FCF109719";
            var entity = _mapper.Map<TourReservation>(request);
            entity.Code = Guid.NewGuid();
            entity.ReservationCode = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedDate = DateTime.Now.ToLocalTime();
            entity.AvaliableBalance = 0;
            entity.AdvancedPayment = 0;
            entity.IsCompleted = false;
            entity.IsPayment = false;
            entity.IsDeleted = false;


            //if (await _uow.SaveAsync() <= 0)
            //{
            //    _logger.LogError("Kayıt Gerçekleşmedi");
            //    return ServiceResponse<TourReservationDto>.Return500();
            //}

            if (request.ReservationPersons.Count > 0)
            {
                entity.ReservationPersons = new List<TourReservationPerson>();
                foreach (var person in request.ReservationPersons)
                {
                    entity.ReservationPersons.Add(new TourReservationPerson
                    {
                        TourReservationId = entity.Id,
                        TourDepartureId = person.TourDepartureId,
                        IdentificationNumber = person.IdentificationNumber,
                        TourPriceId = person.TourPriceId,
                        BirthDay = person.BirthDay,
                        Email = person.Email,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Gender = person.Gender,
                        Phone = person.Phone,
                        FilePath = person.FilePath,
                        StudentPath = person.StudentPath,
                        Uyruk = person.Uyruk
                    });

                }
            }
            _tourReservationRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<TourReservationDto>.Return500();
            }

            var entityDto = _mapper.Map<TourReservationDto>(entity);
            return ServiceResponse<TourReservationDto>.ReturnResultWith200(entityDto);



        }
    }
}