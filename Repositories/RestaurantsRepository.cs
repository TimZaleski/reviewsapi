using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using reviewsapi.Models;

namespace reviewsapi.Repositories
{
  public class RestaurantsRepository
  {
    private readonly IDbConnection _db;

    public RestaurantsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Restaurant> GetAll()
    {
      string sql = @"
      SELECT * FROM restaurants";
      return _db.Query<Restaurant>(sql);
    }


    // REVIEW[epic=Populate] add the creator to the object
    internal Restaurant GetById(int id)
    {
      string sql = @" 
      SELECT 
      re.*,
      pr.*
      FROM restaurants re
      JOIN profiles pr ON re.creatorId = pr.id
      WHERE re.id = @id";
      return _db.Query<Restaurant, Profile, Restaurant>(sql, (restaurant, profile) =>
      {
        restaurant.Creator = profile;
        return restaurant;
      }, new { id }, splitOn: "id").FirstOrDefault();

    }

    internal Restaurant Create(Restaurant newRestaurant)
    {
      string sql = @"
      INSERT INTO parties 
      (creatorId, name, location, owner) 
      VALUES 
      (@CreatorId, @Name, @Location, @Owner);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newRestaurant);
      newRestaurant.Id = id;
      return newRestaurant;
    }

    internal Restaurant Edit(Restaurant updated)
    {
      string sql = @"
        UPDATE restaurants
        SET
         name = @Name,
         location = @Location,
         owner = @Owner
        WHERE id = @Id;";
      _db.Execute(sql, updated);
      return updated;
    }

    // REVIEW[epic=many-to-many] This sql will add the relationship id to a Restaurant, as the RestaurantReviewViewModel
    // REVIEW[epic=Populate] and get the creator
    internal IEnumerable<RestaurantReviewViewModel> GetRestaurantsByProfileId(string id)
    {
      string sql = @"
      SELECT
      rest.*,
      r.id as ReviewId,
      pr.*
      FROM reviews r
      JOIN restaurants rest ON r.partyId == rest.id
      JOIN profiles pr ON rest.creatorId = pr.id
      WHERE memberId = @id
      ";
      return _db.Query<RestaurantReviewViewModel, Profile, RestaurantReviewViewModel>(sql, (restaurant, profile) =>
      {
        restaurant.Creator = profile;
        return restaurant;
      }
        , new { id }, splitOn: "id");
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM restaurants WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}