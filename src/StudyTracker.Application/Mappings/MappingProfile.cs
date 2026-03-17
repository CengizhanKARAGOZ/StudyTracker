using AutoMapper;
using StudyTracker.Application.DTOs.StudySession;
using StudyTracker.Application.DTOs.Subject;
using StudyTracker.Application.DTOs.Topic;
using StudyTracker.Domain.Entities;

namespace StudyTracker.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Subject
        CreateMap<Subject, SubjectDto>()
            .ForMember(d => d.TopicCount, opt => opt.MapFrom(s => s.Topics.Count));

        CreateMap<CreateSubjectDto, Subject>();

        CreateMap<Subject, SubjectDetailDto>()
            .ForMember(d => d.Topics, opt => opt.MapFrom(s =>
                s.Topics.Where(t => t.ParentTopicId == null).OrderBy(t => t.Order)));

        CreateMap<Topic, TopicTreeDto>()
            .ForMember(d => d.SubTopics, opt => opt.MapFrom(t => t.SubTopics.OrderBy(st => st.Order)));

        // Topic
        CreateMap<Topic, TopicDto>();
        CreateMap<CreateTopicDto, Topic>();
        
        // StudySession
        CreateMap<StudySession, StudySessionDto>()
            .ForMember(d => d.TopicName, opt => opt.MapFrom(s => s.Topic.Name))
            .ForMember(d => d.SubjectName, opt => opt.MapFrom(s => s.Topic.Subject.Name))
            .ForMember(d => d.SubjectColor, opt => opt.MapFrom(s => s.Topic.Subject.Color));
    }
}