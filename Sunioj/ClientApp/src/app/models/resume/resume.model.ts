import { ResumeCourse } from "./resume-course.model";
import { ResumeEducation } from "./resume-education.model";
import { ResumeExperience } from "./resume-experience.model";
import { ResumeInterest } from "./resume-interest.model";
import { ResumeLanguage } from "./resume-language.model";
import { ResumeSkill } from "./resume-skill.model";

export class Resume {
    name: string;
    title: string;
    summary: string;
    phone: string;
    email: string;
    website: string;
    location: string;
    experience: ResumeExperience[];
    education: ResumeEducation[];
    courses: ResumeCourse[];
    skills: ResumeSkill[];
    languages: ResumeLanguage[];
    interests: ResumeInterest[];
}