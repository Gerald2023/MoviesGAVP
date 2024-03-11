using GV.DVDCentral.UI.Extensions;

namespace GV.DVDCentral.UI.Models
{
    public class Authenticate
    {
        public static bool IsAuthenticated(HttpContext context)
        {
            if (context.Session.GetObject<User>("user") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
