using System;

namespace reviewsapi.Exceptions
{
  public class NotAuthorized : Exception
  {
    public NotAuthorized(string message) : base(message)
    {
    }
  }
}