using PishiStirayNET.Data.DbEntities;

namespace PishiStirayNET.Infrastructure
{
    internal static class CurrentUser
    {
        public static UserDB User { get; set; }
    }
}
