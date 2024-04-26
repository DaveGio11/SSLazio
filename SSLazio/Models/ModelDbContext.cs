using System.Data.Entity;

namespace SSLazio.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Abbonamenti> Abbonamenti { get; set; }
        public virtual DbSet<Biglietti> Biglietti { get; set; }
        public virtual DbSet<Calciatori> Calciatori { get; set; }
        public virtual DbSet<Calciatrici> Calciatrici { get; set; }
        public virtual DbSet<DettaglioOrdini> DettaglioOrdini { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Trophies> Trophies { get; set; }
        public virtual DbSet<Memories> Memories { get; set; }
        public virtual DbSet<Communications> Communications { get; set; }
        public virtual DbSet<Ordini> Ordini { get; set; }
        public virtual DbSet<Partite> Partite { get; set; }
        public virtual DbSet<Prodotti> Prodotti { get; set; }
        public virtual DbSet<SettoriAbb> SettoriAbb { get; set; }
        public virtual DbSet<SettoriStadio> SettoriStadio { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abbonamenti>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Biglietti>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DettaglioOrdini>()
               .Property(e => e.Totale)
               .HasPrecision(19, 4);

            modelBuilder.Entity<Ordini>()
               .HasMany(e => e.DettaglioOrdini)
               .WithRequired(e => e.Ordini)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Partite>()
                .HasMany(e => e.Biglietti)
                .WithRequired(e => e.Partite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Partite>()
                .HasMany(e => e.SettoriStadio)
                .WithRequired(e => e.Partite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotti>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prodotti>()
                .HasMany(e => e.DettaglioOrdini)
                .WithRequired(e => e.Prodotti)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SettoriAbb>()
                .Property(e => e.Intero)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SettoriAbb>()
                .Property(e => e.Under16)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SettoriAbb>()
                .Property(e => e.Aquilotto)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SettoriAbb>()
                .HasMany(e => e.Abbonamenti)
                .WithRequired(e => e.SettoriAbb)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SettoriStadio>()
                .Property(e => e.Intero)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SettoriStadio>()
                .Property(e => e.Ridotto)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SettoriStadio>()
                .HasMany(e => e.Biglietti)
                .WithRequired(e => e.SettoriStadio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Abbonamenti)
                .WithRequired(e => e.Utenti)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Biglietti)
                .WithRequired(e => e.Utenti)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Ordini)
                .WithRequired(e => e.Utenti)
                .WillCascadeOnDelete(false);
        }


    }
}
