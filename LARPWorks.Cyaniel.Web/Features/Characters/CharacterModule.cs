namespace LARPWorks.Cyaniel.Features.Characters
{
    public class CharacterModule : CyanielModule
    {
        public CharacterModule() : base("Characters")
        {
            Get["/view"] = parameters => View["View.cshtml", GetViewModel<CharacterSheetViewModel>()];
            Get["/index"] = parameters => View["Index.cshtml", GetViewModel<CharacterListViewModel>()];
        }
    }
}
