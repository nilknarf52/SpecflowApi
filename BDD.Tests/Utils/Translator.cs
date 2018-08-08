using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BDD.Tests.Utils
{
    public class Translator
    {
        public static string Translate(string args)
        {
            if (args == null)
                return null;

            var regexFeatureContext = new Regex(@"<f:\w*>");
            var regexScenarioContext = new Regex(@"<s:\w*>");

            if (regexScenarioContext.IsMatch(args))
            {
                var key = GetKey(args);
                if (!ScenarioContext.Current.ContainsKey(key))
                    throw new InvalidOperationException(string.Format("O parâmetro de cenário {0} não foi registrado.", key));

                var value = ScenarioContext.Current[key];
                return value != null ? value.ToString() : null;
            }

            if (regexFeatureContext.IsMatch(args))
            {
                var key = GetKey(args);
                if (!FeatureContext.Current.ContainsKey(key))
                    throw new InvalidOperationException(string.Format("O parâmetro de funcionalidade {0} não foi registrado.", key));

                var value = FeatureContext.Current[key];
                return value != null ? value.ToString() : null;
            }

            if (args == "<Hoje>")
                return DateTime.Now.Date.ToShortDateString();

            return args;
        }

        private static String GetKey(String argumento)
        {
            return argumento.Remove(0, 3).Remove(argumento.Length - 4, 1);
        }
    }
}
