using AutoMapper;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Contracts.UnitsOfWork;
using Sunioj.Core.Resources.Resumes;
using Sunioj.Data.Entities;
using System;
using System.Threading.Tasks;

namespace Sunioj.Core.Services
{
    public class ResumesService : IResumesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ResumesService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResumeDTO> GetAsync()
        {
            var resume = await _unitOfWork.Resumes.GetAsync();

            return _mapper.Map<ResumeDTO>(resume);
        }

        public async Task<ResumeDTO> UpdateAsync(ResumeForUpdateDTO resumeForUpdate)
        {
            var oldResume = await _unitOfWork.Resumes.GetAsync();
            _unitOfWork.Resumes.Remove(oldResume);

            var newResume = _mapper.Map<Resume>(resumeForUpdate);
            newResume.CreatedAt = DateTime.Now;

            _unitOfWork.Resumes.Add(newResume);

            await _unitOfWork.CompleteAsync();

            return await GetAsync();
        }
    }
}
