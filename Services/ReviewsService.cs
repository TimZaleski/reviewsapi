using System;
using reviewsapi.Models;
using reviewsapi.Repositories;
using reviewsapi.Exceptions;

namespace reviewsapi.Services
{
  public class ReviewsService
  {
    private readonly ReviewsRepository _repo;
    private readonly RestaurantsRepository _rr;
    public ReviewsService(ReviewsRepository repo, RestaurantsRepository rr)
    {
      _repo = repo;
      _rr = rr;
    }

    internal void Create(Review r, string id)
    {
      Restaurant restaurant = _rr.GetById(r.RestaurantId);
      if (restaurant == null)
      {
        throw new Exception("Invalid Id");
      }
      if (restaurant.CreatorId != id)
      {
        throw new NotAuthorized("Not The Owner of this Party");
      }
      _repo.Create(r);
    }

    internal void Delete(int id)
    {
      var data = _repo.GetById(id);
      if (data == null)
      {
        throw new Exception("Invalid Id");
      }
      _repo.Delete(id);
    }
  }
}