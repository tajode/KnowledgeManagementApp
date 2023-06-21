namespace ImplementingLikeButton.Models
{
    public class LikedKnowledge
    {
        public int Id { get; set; }
        public string? KnowledgeId { get; set; }
        public string? UserId { get; set; }
        public string? LikeStatus { get; set; }
        public DateTime? DateofCreation { get; set; }
        public DateTime? DateofStatusChange { get; set; }
        public string? IPAddress { get; set; }
    }
}
