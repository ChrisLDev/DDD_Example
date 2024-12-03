namespace Projects.Infrastructure.RateLimiting
{
	public class RateLimitExceededException(string message) : Exception(message);
}
