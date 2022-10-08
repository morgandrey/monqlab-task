using AutoMapper;
using MonqlabTask.Dto;
using MonqlabTask.Models;

namespace MonqlabTask.Mapping;

/// <summary>
/// The customer resolver for mail result.
/// </summary>
public class MailResultResolver : IValueResolver<Mail, ReadMailsDto, string>
{
    public string Resolve(Mail source, ReadMailsDto destination, string destMember, ResolutionContext context)
    {
        return source.MailResult ? "OK" : "Failed";
    }
}