using AutoMapper;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Contracts.UnitsOfWork;
using Sunioj.Core.Exceptions;
using Sunioj.Core.Resources.ServiceFaqs;
using Sunioj.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Core.Services
{
    public class ServiceFaqsService : IServiceFaqsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceFaqsService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ServiceFaqDTO>> GetAllAsync()
        {
            var faqs = await _unitOfWork.ServiceFaqs.GetAllAsync();

            return _mapper.Map<IEnumerable<ServiceFaqDTO>>(faqs);
        }

        public async Task<ServiceFaqDTO> CreateAsync(ServiceFaqForCreationDTO faqForCreation)
        {
            var faq = _mapper.Map<ServiceFaq>(faqForCreation);
            faq.CreatedAt = DateTime.Now;

            _unitOfWork.ServiceFaqs.Add(faq);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ServiceFaqDTO>(faq);
        }

        public async Task DeleteAsync(int id)
        {
            var faq = await _unitOfWork.ServiceFaqs.GetByIdAsync(id);

            if (faq == null)
            {
                throw new NotFoundException($"Service FAQ {id} not found");
            }

            _unitOfWork.ServiceFaqs.Remove(faq);
            await _unitOfWork.CompleteAsync();
        }
    }
}
