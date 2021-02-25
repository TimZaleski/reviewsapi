namespace reviewsapi.Models
{
  public class Restaurant
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Owner { get; set; }
    // REVIEW[epic=Populate] This value will be populated when pulled from the DB will not be stored in the db
    // it is effectively a 'virtual'
    public Profile Creator { get; set; }
  }


  // NOTE[epic=many-to-many] Adds the relationshipId to the base class
  public class RestaurantReviewViewModel : Restaurant
  {
    public int ReviewId { get; set; }
  }
}