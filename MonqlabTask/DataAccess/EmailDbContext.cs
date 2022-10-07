using Microsoft.EntityFrameworkCore;
using MonqlabTask.Models;

namespace MonqlabTask.DataAccess
{
    public partial class EmailDbContext : DbContext
    {
        public EmailDbContext(DbContextOptions<EmailDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Mail> Mail { get; set; } = null!;
        public virtual DbSet<MailRecipient> MailRecipients { get; set; } = null!;
        public virtual DbSet<Recipient> Recipients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mail>(entity =>
            {
                entity.HasIndex(e => e.MailId, "Mail_mail_id_uindex")
                    .IsUnique();

                entity.Property(e => e.MailId).HasColumnName("mail_id");

                entity.Property(e => e.MailBody).HasColumnName("mail_body");

                entity.Property(e => e.MailDate)
                    .HasColumnType("datetime")
                    .HasColumnName("mail_date");

                entity.Property(e => e.MailFailedMessage).HasColumnName("mail_failed_message");

                entity.Property(e => e.MailResult).HasColumnName("mail_result");

                entity.Property(e => e.MailSubject).HasColumnName("mail_subject");
            });

            modelBuilder.Entity<MailRecipient>(entity =>
            {
                entity.ToTable("Mail_Recipient");

                entity.HasIndex(e => e.Id, "Mail_Recipient_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MailId).HasColumnName("mail_id");

                entity.Property(e => e.RecipientId).HasColumnName("recipient_id");

                entity.HasOne(d => d.Mail)
                    .WithMany(p => p.MailRecipients)
                    .HasForeignKey(d => d.MailId)
                    .HasConstraintName("Mail_Recipient_Mail_mail_id_fk");

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.MailRecipients)
                    .HasForeignKey(d => d.RecipientId)
                    .HasConstraintName("Mail_Recipient_Recipient_recipient_id_fk");
            });

            modelBuilder.Entity<Recipient>(entity =>
            {
                entity.ToTable("Recipient");

                entity.HasIndex(e => e.RecipientId, "Recipient_recipient_id_uindex")
                    .IsUnique();

                entity.Property(e => e.RecipientId).HasColumnName("recipient_id");

                entity.Property(e => e.RecipientEmail).HasColumnName("recipient_email");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
