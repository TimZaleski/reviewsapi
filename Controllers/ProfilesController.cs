using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reviewsapi.Models;
using reviewsapi.Services;

namespace reviewsapi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _service;
    private readonly RestaurantsService _rs;

    public ProfilesController(ProfilesService service, RestaurantsService rs)
    {
      _service = service;
      _rs = rs;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile profile = _service.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet(("{id}/parties"))]
    [Authorize]
    public ActionResult<IEnumerable<RestaurantReviewViewModel>> GetParties(string id)
    {
      try
      {
        IEnumerable<RestaurantReviewViewModel> parties = _rs.GetByProfileId(id);
        return Ok(parties);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}