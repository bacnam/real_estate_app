namespace RealEstate.WebService.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetToken(this HttpContext httpContext)
        {
            string authHeader = httpContext.Request.Headers["authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer ", StringComparison.Ordinal))
            {
                string token = authHeader.Replace("Bearer ", "", StringComparison.Ordinal);
                Console.WriteLine(token);
                return token.TrimStart();
            }
            return string.Empty;
        }
    }
}
