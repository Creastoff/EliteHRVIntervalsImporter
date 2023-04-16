namespace Intervals.Model.Intervals.ICU
{
    public class IntervalsAccess
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public bool IsAllowedToStore { get; set; }

        public string GetEncodedAccessToken()
        {
            string accessToken = $"API_KEY:{AccessToken}";
            var plainTextBytes = System.Text.Encoding.ASCII.GetBytes(accessToken);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
