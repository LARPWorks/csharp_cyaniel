using Nancy.ViewEngines.Razor;
using System;
using System.Collections.Generic;

namespace LARPWorks.Cyaniel.Features.Characters.Views
{
    public static class ViewRazerExtensions
    {
        public static HelperResult List<T>(this IEnumerable<T> items, Func<T, HelperResult> template)
        {
            Console.WriteLine("Writing list for: " + items.GetType());
            foreach (T item in items) {
                Console.WriteLine(item.ToString());
            }
            return new HelperResult(writer =>
            {
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        template(item).WriteTo(writer);
                    }
                }
            });
        }
    }
}
