using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MonqlabTask.Dto;
using MonqlabTask.Repository;
using MonqlabTask.Services;

namespace MonqlabTask.Controllers;

[ApiController]
[Route("api/mails")]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly IRepository _repository;
    private readonly IEmailService _emailService;

    public EmailController(ILogger<EmailController> logger, IRepository repository, IEmailService emailService)
    {
        _logger = logger;
        _repository = repository;
        _emailService = emailService;
    }

    [HttpGet(Name = "GetEmails")]
    public IResult Get()
    {
        return Results.Ok(_repository.GetAllMessages());
    }

    [HttpPost(Name = "PostEmails")]
    public IResult Post(MailCreateDto mailCreate)
    {
        try
        {
            _emailService.Send(mailCreate.Recipients.ToList(),
                mailCreate.Subject,
                mailCreate.Body);

            return Results.Ok();
        }
        catch (SmtpCommandException ex)
        {
            _logger.LogError("{Error}", ex.Message);
        }

        return Results.BadRequest();
    }
}