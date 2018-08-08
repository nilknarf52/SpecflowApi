using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Assist.ValueComparers;
using TechTalk.SpecFlow.Assist.ValueRetrievers;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace BDD.Tests.Utils
{
    [Binding]
    public static class GenericParsers
    {
        [BeforeTestRun(Order = 1)]
        static void Initialize()
        {
            SetupStepArgumentConverterValueComparer();
        }

        private static void SetupStepArgumentConverterValueComparer()
        {
            var assistService = TechTalk.SpecFlow.Assist.Service.Instance;

            foreach (var valueComparer in assistService.ValueComparers.ToArray())
                assistService.UnregisterValueComparer(valueComparer);

            assistService.RegisterValueComparer(new CustomDefaultValueComparer());
        }

      
    }
}
