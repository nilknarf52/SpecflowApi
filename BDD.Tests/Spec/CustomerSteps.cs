using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace BDD.Tests.Spec
{
    [Binding]
    public sealed class CustomerSteps
    {
        [BeforeScenario("criarCliente")]
        public void Before()
        {
            ScenarioContext.Current["id"] = 1;
        }

        [AfterScenario("criarCliente")]
        public void After()
        {
            //REMOVE DO BANCO DO DADOS
        }

    }
}
