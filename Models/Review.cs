namespace reviewsapi.Models
{
  public class Review
  {
    // REVIEW[epic=many-to-many] Join Table object
    public int Id { get; set; }
    public string ProfileId { get; set; }
    public int RestaurantId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Owner { get; set; }
    public decimal Rating { get; set; }
  }
}