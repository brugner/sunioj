using AutoMapper;
using Sunioj.Core.Resources.Messages;
using Sunioj.Data.Entities;

namespace Sunioj.Core.MappingProfiles
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<Message, MessageDTO>();
            CreateMap<MessageForCreationDTO, Message>();
        }
    }
}
