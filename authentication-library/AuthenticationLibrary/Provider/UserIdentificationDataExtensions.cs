using System.Security.Claims;

namespace AuthenticationLibrary.Provider
{
    public static class UserIdentificationDataExtensions
    {
        public static string GetUserDisplayNameFrom(List<Claim> claims)
        {
            string? firstName = claims.Where(claim => claim.Type == "FirstName").Select(claim => claim.Value).FirstOrDefault();
            string? lastName = claims.Where(claim => claim.Type == "LastName").Select(claim => claim.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                string? email = claims.Where(claim => claim.Type == "Email").Select(claim => claim.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(email))
                {
                    return string.Empty;
                }
                return email;
            }
            return $"{lastName} {firstName}";
        }

        public static string GetUserRoleFrom(List<Claim> claims)
        {
            string? role = claims.Where(claim => claim.Type == "Szerep").Select(claim => claim.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(role))
            {
                return string.Empty;
            }
            return role;
        }

        public static string GetUserEmail(List<Claim> claims)
        {
            if (claims is not null)
            {
                string? email = claims.Where(claim => claim.Type == "Email").Select(claim => claim.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(email))
                {
                    return email;
                }
            }
            return string.Empty;
        }

        public static string GetDebugDataFrom(List<Claim> claims)
        {
            string debugData = string.Empty;
            debugData = "<table cellpadding=10>";
            foreach (Claim claim in claims)
            {
                debugData = $"{debugData}<tr>";
                if (debugData.Any())
                    debugData = $"{debugData} <td>{claim.Type}</td> <td> {claim.Issuer}</td><td> {claim.Value}</td>";
                else
                    debugData = $"<td>{claim.Type} </td><td> {claim.Issuer}</td> <td> {claim.Value}</td>";
                debugData = $"{debugData}</tr>";
            }
            return debugData;
        }

    }
}
