using System.ComponentModel.DataAnnotations;

namespace ImplementingLikeButton.Models
{
    public class Knowledge
    {
        public int Id { get; set; }
        public DateTime DateofCreation { get; set; }
        public string? IPAddress { get; set; }
        public string? KnowledgeName { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
    }
}
