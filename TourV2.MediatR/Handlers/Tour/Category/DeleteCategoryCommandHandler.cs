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
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ServiceResponse<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork<TourContext> uow)
        {
            _categoryRepository = categoryRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<CategoryDto>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _categoryRepository.FindBy(x=>x.Id ==request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<CategoryDto>.Return404();
            }
            _categoryRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<CategoryDto>.Return500();
            }
            return ServiceResponse<CategoryDto>.ReturnResultWith204();
        }
    }
}
