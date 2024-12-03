using System.Threading.RateLimiting;

namespace Projects.Infrastructure.RateLimiting
{
	public class RateLimitingHandler(RateLimiter rateLimiter) : DelegatingHandler
	{
		private readonly RateLimiter _rateLimiter = rateLimiter ?? throw new ArgumentNullException(nameof(rateLimiter));

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var lease = await _rateLimiter.AcquireAsync(1, cancellationToken);

			if (lease.IsAcquired)
			{
				try
				{
					return await base.SendAsync(request, cancellationToken);
				}
				finally
				{
					lease.Dispose();
				}
			}

			throw new RateLimitExceededException("Rate limit exceeded. Please try again later.");
		}
	}
}
