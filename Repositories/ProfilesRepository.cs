using System.Collections.Generic;
using System.Data;
using Dapper;
using reviewsapi.Models;

namespace reviewsapi.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetById(string id)
    {
      string sql = "SELECT * FROM profiles WHERE id = @id";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }


    internal IEnumerable<ProfileReviewViewModel> GetByRestaurantId(int id)
    {
      string sql = @"
      SELECT
      prof.*
      r.id as ReviewId
      FROM reviews r
      JOIN profiles prof ON r.memberId = prof.id
      WHERE restaurantId = @id
      ";
      return _db.Query<ProfileReviewViewModel>(sql, new { id });
    }

    internal Profile Create(Profile newProfile)
    {
      string sql = @"
            INSERT INTO profiles
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newProfile);
      return newProfile;
    }
  }
}