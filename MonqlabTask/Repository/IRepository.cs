using MonqlabTask.Models;

namespace MonqlabTask.Repository;

public interface IRepository
{
    Task SaveChanges();
    Task<IEnumerable<Mail>> GetAllMessages();
    Task AddNewEmail(Mail mail);
}