using AutoMapper;
using MonqlabTask.Dto;
using MonqlabTask.Models;

namespace MonqlabTask.Mapping;

public class DomainToDtoProfile : Profile
{
    public DomainToDtoProfile()
    {
        CreateMap<Mail, ReadMailsDto>()
            .ForMember(dst => dst.Body, opt
                => opt.MapFrom(src => src.MailBody))
            .ForMember(dst => dst.Subject, opt
                => opt.MapFrom(src => src.MailSubject))
            .ForMember(dst => dst.MailResult, opt
                => opt.MapFrom<MailResultResolver>())
            .ForMember(dst => dst.MailFailedMessage, opt
                => opt.MapFrom(src => src.MailFailedMessage))
            .ForMember(dst => dst.MailDate, opt
                => opt.MapFrom(src => src.MailDate))
            .ForMember(dst => dst.Recipients, src
                => src.MapFrom(mbr => mbr.MailRecipients.Select(x => x.Recipient.RecipientEmail)));
    }
}