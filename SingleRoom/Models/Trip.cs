using SingleRoom.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SingleRoom.Models
{
    public class Trip
    {
        [Key]
        public Guid TripId { get; set; } = new Guid();

        [DisplayName("Nazwa wycieczki")]
        [Required(ErrorMessage = "Nazwa wycieczki jest wymagana")]
        public string TripName { get; set; }

        [DisplayName("Biuro Podróży")]
        [Required(ErrorMessage = "Biuro podróży jest wymagane")]
        public TravelAgency TravelAgencyName { get; set; }

        [DisplayName("Miejsce wylotu")]
        [Required(ErrorMessage = "Miejsce wylotu jest wymagane")]
        public string StartingAirport { get; set; }

        [DisplayName("Cel")]
        [Required(ErrorMessage = "Cel jest wymagany")]
        public string Destination { get; set; }

        [DisplayName("Wylot")]
        [Required(ErrorMessage = "Data wylotu jest wymagana")]
        public DateTime StartDate { get; set; }

        [DisplayName("Powrót")]
        [Required(ErrorMessage = "Data powrotu jest wymagana")]
        public DateTime EndDate { get; set; }

        [DisplayName("Adres Email")]
        [Required(ErrorMessage = "Adres email jest wymagany")]
        public string EmailAddress { get; set; }

        [DisplayName("Email uczestnika")]
        public string? ParticipantEmail { get; set; }
    }
}
