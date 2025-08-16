using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactureAbonnement.API.Models
{
    //enums
    public enum  SubscriptionType
    {
        Mensuel,
        Trimestriel,
        Annuel
    }

    public enum SubscriptionStatut
    {
        Actif,
        Suspendu,
        Resilie
    }

    //class Abonnement
    public class Abonnement
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public SubscriptionType Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Tarif { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TarifModifie { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }

        [Required]
        public SubscriptionStatut Statut { get; set; } = SubscriptionStatut.Actif;

        // ====== MÉTHODES =====

        public void Suspendre()
        {
            if (Statut == SubscriptionStatut.Actif)
            {
                Statut = SubscriptionStatut.Suspendu;
            }
        }

        public void Resilier()
        {
            if (Statut != SubscriptionStatut.Resilie)
            {
                Statut = SubscriptionStatut.Resilie;
                DateFin = DateTime.UtcNow;
            }
        }

        public void Modifier(decimal nouveauTarif, DateTime nouvelleDateFin)
        {
            if (Statut == SubscriptionStatut.Actif)
                throw new InvalidOperationException("Seul un abonnement actif peut etre modifié");

            TarifModifie = nouveauTarif;
            DateFin = nouvelleDateFin;
        }

        public int CalculDureeEffective()
        {
            return (DateTime.UtcNow - DateDebut).Days;
        }
    }
}
