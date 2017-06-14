namespace LARPWorks.Cyaniel.Web.Features.Characters
{
    public class CharacterModule : CyanielModule
    {
        public CharacterModule() : base("Characters")
        {
            Get["/view"] = parameters => View["View.cshtml", GetViewModel<CharacterViewModel>()];
            Get["/index"] = parameters => View["Index.cshtml", GetViewModel<CharacterListViewModel>()];
        }
    }
}
