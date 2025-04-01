
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server_side.Models
{
    public class Subjects
    {
        [Key]

        public int SubjectId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string SubjectName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? SubImageName { get; set; }

     


    }
}
