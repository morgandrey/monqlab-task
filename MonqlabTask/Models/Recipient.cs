namespace MonqlabTask.Models
{
    public partial class Recipient
    {
        public Recipient()
        {
            MailRecipients = new HashSet<MailRecipient>();
        }

        public int RecipientId { get; set; }
        public string RecipientEmail { get; set; } = null!;

        public virtual ICollection<MailRecipient> MailRecipients { get; set; }
    }
}
