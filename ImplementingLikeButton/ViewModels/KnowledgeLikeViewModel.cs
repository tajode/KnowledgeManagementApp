namespace ImplementingLikeButton.ViewModels
{
    public class KnowledgeLikeViewModel
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
