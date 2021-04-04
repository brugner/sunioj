using AutoMapper;
using Sunioj.Core.Resources.Resumes;
using Sunioj.Data.Entities;

namespace Sunioj.Core.MappingProfiles
{
    public class ResumeMappingProfile : Profile
    {
        public ResumeMappingProfile()
        {
            CreateMap<Resume, ResumeDTO>();
            CreateMap<ResumeExperience, ResumeExperienceDTO>();
            CreateMap<ResumeEducation, ResumeEducationDTO>();
            CreateMap<ResumeCourse, ResumeCourseDTO>();
            CreateMap<ResumeSkill, ResumeSkillDTO>();
            CreateMap<ResumeLanguage, ResumeLanguageDTO>();
            CreateMap<ResumeInterest, ResumeInterestDTO>();

            CreateMap<ResumeForUpdateDTO, Resume>();
            CreateMap<ResumeExperienceForUpdateDTO, ResumeExperience>();
            CreateMap<ResumeEducationForUpdateDTO, ResumeEducation>();
            CreateMap<ResumeCourseForUpdateDTO, ResumeCourse>();
            CreateMap<ResumeSkillForUpdateDTO, ResumeSkill>();
            CreateMap<ResumeLanguageForUpdateDTO, ResumeLanguage>();
            CreateMap<ResumeInterestForUpdateDTO, ResumeInterest>();
        }
    }
}
