using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MonqlabTask.Dto;
using MonqlabTask.Helpers;
using MonqlabTask.Models;
using MonqlabTask.Repository;
using MonqlabTask.Services;

namespace MonqlabTask.Controllers;

[ApiController]
[Route("api/mails")]
public class MailController : ControllerBase
{
    private readonly ILogger<MailController> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMailRepository _mailRepository;
    private readonly IMailService _mailService;
    private readonly IMapper _mapper;

    public MailController(ILogger<MailController> logger, IMailRepository mailRepository, IMailService mailService,
        IMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _mailRepository = mailRepository;
        _mailService = mailService;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }

    [HttpGet(Name = "GetEmails")]
    public async Task<IActionResult> Get()
    {
        var mails = await _mailRepository.GetAll();
        var mailDtos = _mapper.Map<List<ReadMailsDto>>(mails);
        return Ok(mailDtos);
    }

    [HttpPost(Name = "PostEmail")]
    public async Task<IActionResult> Post(CreateMailDto createMailDto)
    {
        var mailResult = true;
        var mailFailedMessage = string.Empty;

        var listOfRecipientsEmails = createMailDto.Recipients.ToList();

        try
        {
            await _mailService.Send(listOfRecipientsEmails,
                createMailDto.Subject,
                createMailDto.Body);
        }
        catch (SmtpCommandException ex)
        {
            mailFailedMessage = ex.Message;
            _logger.LogError("{Error}", mailFailedMessage);
            mailResult = false;
        }

        var mail = new Mail
        {
            MailSubject = createMailDto.Subject,
            MailBody = createMailDto.Body,
            MailDate = _dateTimeProvider.Now,
            MailResult = mailResult,
            MailFailedMessage = mailFailedMessage
        };

        await _mailRepository.Add(mail, listOfRecipientsEmails);

        if (!mailResult)
        {
            return BadRequest(mailFailedMessage);
        }
        
        return Ok();
    }
}