using TourV2.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TourV2.Data.Entities;
using TourV2.Data.Entities.TourTravel;

namespace TourV2.Domain
{
    public class TourContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public TourContext(DbContextOptions options) : base(options)
        {
        }
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Data.Action> Actions { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageAction> PageActions { get; set; }
        public DbSet<NLog> NLog { get; set; }
        public DbSet<LoginAudit> LoginAudits { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<UserAllowedIP> UserAllowedIPs { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailSMTPSetting> EmailSMTPSettings { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        public DbSet<TourClick> TourClicks { get; set; }
        public DbSet<FrontAnnouncement> FrontAnnouncements { get; set; }
        public DbSet<FrontAnnouncementRecord> FrontAnnouncementRecords { get; set; }

        //TOUR&TRAVEL
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourRecord> TourRecords { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Category> CategoryRecords { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<PeriodRecord> PeriodRecords { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionRecord> RegionRecords { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<DepartureRecord> DepartureRecords { get; set; }
        public DbSet<ActiveTour> ActiveTours { get; set; }
        public DbSet<TourDeparture> TourDepartures { get; set; }
        public DbSet<TourCategory> TourCategories { get; set; }
        public DbSet<TourTransportation> TourTransportations { get; set; }
        public DbSet<TourSpecification> TourSpecifications { get; set; }
        public DbSet<TourDay> TourDays { get; set; }
        public DbSet<TourPrice> TourPrices { get; set; }
        public DbSet<TourMedia> TourMedias { get; set; }
        public DbSet<TourReservation> TourReservations { get; set; }
        public DbSet<TourReservationPerson> TourReservationPersons { get; set; }
        public DbSet<TourComment> TourComments { get; set; }
        public DbSet<CostCalculation> CostCalculations { get; set; }
        public DbSet<EducationForm>EducationForms { get; set; }
        public DbSet <PeriodEducation> PeriodEducations { get; set; }
        public DbSet<State> States  { get; set; }
        public DbSet <Mosque>Mosques { get; set; }
        public DbSet<Survey> Surveys { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ContactMessage>(b =>
            {
                b.Property(x => x.Type)
                    .HasDefaultValue(1);
            });

            builder.Entity<User>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Role>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();

                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);

            });

            builder.Entity<Data.Action>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<PageAction>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Page>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<UserAllowedIP>().HasKey(c => new { c.UserId, c.IPAddress });

            builder.Entity<EmailSMTPSetting>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            //builder.Entity<EducationForm>(b =>
            //{
            //    b.HasOne(e => e.PeriodEducation)
            //        .WithMany()
            //        .HasForeignKey(ur => ur.PeriodEducationId)
            //        .OnDelete(DeleteBehavior.NoAction);

 
            //});

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserToken>().ToTable("UserTokens");
            builder.DefalutMappingValue();
            builder.DefalutDeleteValueFilter();
        }
    }
}
