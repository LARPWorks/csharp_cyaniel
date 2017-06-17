using System.Runtime.Remoting.Contexts;
using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Features.Users.Authentication;
using LARPWorks.Cyaniel.Models;
using Nancy;

namespace LARPWorks.Cyaniel.Features
{
    public abstract class CyanielModule : NancyModule
    {
        protected User CurrentUser
        {
            get
            {
                if (Context == null)
                {
                    return null;
                }

                var userIdentity = Context.CurrentUser as UserIdentity;
                if (userIdentity == null)
                {
                    return null;
                }

                return userIdentity.GetUser();
            }
        }

        protected CyanielModule() : base() { }
        protected CyanielModule(string modulePath) : base(modulePath) { }

        protected TViewModel GetViewModel<TViewModel>(TViewModel instance = null) where TViewModel : BaseCyanielViewModel, new()
        {
            var viewModel = instance ?? new TViewModel();
            viewModel.CurrentUser = CurrentUser;

            return viewModel;
        }
    }
}
