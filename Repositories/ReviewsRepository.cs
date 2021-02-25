using System;
using System.Data;
using Dapper;
using reviewsapi.Models;

namespace reviewsapi.Repositories
{
  public class ReviewsRepository
  {
    private readonly IDbConnection _db;

    public ReviewsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal void Create(Review pm)
    {
      string sql = @"
      INSERT INTO reviews
      (memberId, restaurantId, title, body, owner, rating)
      VALUES
      (@MemberId, @RestaurantId, @Title, @Body, @Owner, @Rating);";
      _db.Execute(sql, pm);
    }
    internal Review GetById(int id)
    {
      string sql = "SELECT * FROM reviews WHERE id = @id;";
      return _db.QueryFirstOrDefault<Review>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM reviews WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}