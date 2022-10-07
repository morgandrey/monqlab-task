namespace MonqlabTask.Models
{
    public partial class MailRecipient
    {
        public int Id { get; set; }
        public int MailId { get; set; }
        public int RecipientId { get; set; }

        public virtual Mail Mail { get; set; } = null!;
        public virtual Recipient Recipient { get; set; } = null!;
    }
}
