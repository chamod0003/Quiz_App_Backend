using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using server_side.Models;

namespace server_side.Models
{
    public class QuizDbcontext : IdentityDbContext
    {
        public QuizDbcontext(DbContextOptions<QuizDbcontext> options) : base(options)
        {
        }

        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<server_side.Models.Subjects> Subjects { get; set; } = default!;
        public DbSet<server_side.Models.Quiz_java> Quiz_java { get; set; } = default!;
        public DbSet<server_side.Models.Quiz_c> Quiz_c { get; set; } = default!;
        public DbSet<server_side.Models.Participant_c> Participant_c { get; set; } = default!;
        public DbSet<server_side.Models.Participant_java> Participant_java { get; set; } = default!;
    }
}
