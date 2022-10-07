using System.Text.Json.Serialization;

namespace MonqlabTask.Models
{
    public partial class Mail
    {
        public Mail()
        {
            MailRecipients = new HashSet<MailRecipient>();
        }

        public int MailId { get; set; }
        
        public string MailSubject { get; set; } = null!;
        
        public string MailBody { get; set; } = null!;
        public bool MailResult { get; set; }
        public string? MailFailedMessage { get; set; }
        public DateTime MailDate { get; set; }
        
        public virtual ICollection<MailRecipient> MailRecipients { get; set; }
    }
}