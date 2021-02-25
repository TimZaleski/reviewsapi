using System;
using System.Collections.Generic;
using reviewsapi.Models;
using reviewsapi.Repositories;

namespace reviewsapi.Services
{
  public class RestaurantsService
  {
    private readonly RestaurantsRepository _repo;
    public RestaurantsService(RestaurantsRepository repo)
    {
      _repo = repo;
    }
    internal IEnumerable<Restaurant> GetAll()
    {
      // FIXME Should not be able to get any and all parties
      return _repo.GetAll();
    }

    internal Restaurant GetById(int id)
    {
      var data = _repo.GetById(id);
      if (data == null)
      {
        throw new Exception("Invalid Id");
      }
      return data;
    }

    internal Restaurant Create(Restaurant newProd)
    {
      return _repo.Create(newProd);
    }

    internal Restaurant Edit(Restaurant updated)
    {
      var data = GetById(updated.Id);
      updated.Name = updated.Name != null ? updated.Name : data.Name;
      updated.Location = updated.Location != null ? updated.Location : data.Location;
      updated.Owner = updated.Owner != null ? updated.Owner : data.Owner;
      return _repo.Edit(updated);
    }

    internal Restaurant Delete(int id)
    {
      var data = GetById(id);
      _repo.Delete(id);
      return data;
    }

    internal IEnumerable<RestaurantReviewViewModel> GetByProfileId(string id)
    {
      IEnumerable<RestaurantReviewViewModel> data = _repo.GetRestaurantsByProfileId(id);
      return data;
    }
  }
}