using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace server_side.Models
{
    public class Participant_c
    {

        [Key]

        public int ParticipantId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }
        public int Score { get; set; }

        public int TimeTaken { get; set; }
        public int SubjectId { get; set; }


    }
    public class ParticipantResult_c
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int SubjectId { get; set; }
        public int Score { get; set; }
        public int TimeTaken { get; set; } // seconds
        public DateTime AttemptedAt { get; set; }

        [ForeignKey("SubjectId")]
        public Subjects Subject { get; set; }

    }



}
