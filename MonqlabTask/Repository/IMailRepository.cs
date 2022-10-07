using MonqlabTask.Dto;
using MonqlabTask.Models;

namespace MonqlabTask.Repository;

public interface IMailRepository
{
    Task<IEnumerable<Mail>> GetAll();
    Task Add(Mail mail, IEnumerable<string> recipientEmails);
}