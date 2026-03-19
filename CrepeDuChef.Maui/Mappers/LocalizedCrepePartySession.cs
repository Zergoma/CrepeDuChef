using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Maui.Resources.languages;

namespace CrepeDuChef.Maui.Mappers
{
    public class LocalizedCrepePartySession : ITradCrepePartyDefault
    {
        public string Session { get => Traduction.Session;  }
        public string NoName { get => Traduction.NoName;  }
    }
}
