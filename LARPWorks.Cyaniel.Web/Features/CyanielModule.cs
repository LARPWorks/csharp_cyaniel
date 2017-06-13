using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LARPWorks.Cyaniel.Web.Features.SharedViews;
using LARPWorks.Cyaniel.Web.Features.Users.Authentication;
using MySQL;
using Nancy;

namespace LARPWorks.Cyaniel.Web.Features
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

        protected TViewModel GetViewModel<TViewModel>() where TViewModel : BaseCyanielViewModel, new()
        {
            var viewModel = new TViewModel();
            viewModel.CurrentUser = CurrentUser;

            return viewModel;
        }
    }
}
