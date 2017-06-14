
using LARPWorks.Cyaniel.Web.Features.Buckets;

namespace LARPWorks.Cyaniel.Web.Features.Characters
{
    public class CharacterModule : CyanielModule
    {
        public CharacterModule() : base("Characters")
        {
            Get["/view"] = parameters => View["View.cshtml", GetViewModel<CharacterViewModel>()];
        }
    }
}
