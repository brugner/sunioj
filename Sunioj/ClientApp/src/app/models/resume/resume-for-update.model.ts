import { ResumeCourseForUpdate } from "./resume-course-for-update.model";
import { ResumeEducationForUpdate } from "./resume-education-for-update.model";
import { ResumeExperienceForUpdate } from "./resume-experience-for-update.model";
import { ResumeInterestForUpdate } from "./resume-interest-for-update.model";
import { ResumeLanguageForUpdate } from "./resume-language-for-update.model";
import { ResumeSkillForUpdate } from "./resume-skill-for-update.model";

export class ResumeForUpdate {
    name: string;
    title: string;
    summary: string;
    phone: string;
    email: string;
    website: string;
    location: string;
    experience: ResumeExperienceForUpdate[];
    education: ResumeEducationForUpdate[];
    courses: ResumeCourseForUpdate[];
    skills: ResumeSkillForUpdate[];
    languages: ResumeLanguageForUpdate[];
    interests: ResumeInterestForUpdate[];
}