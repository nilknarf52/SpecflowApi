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

namespace BDD.Tests.Utils
{
    internal class CustomDefaultValueComparer : IValueComparer
    {
        Regex regexScenarioContext;
        Regex regexFeatureContext;
        public CustomDefaultValueComparer() : base()
        {
            regexFeatureContext = new Regex(@"<f:\w*>");
            regexScenarioContext = new Regex(@"<s:\w*>");
        }
        public bool CanCompare(object actualValue)
        {
            return true;
        }

        public bool Compare(string expectedValue, object actualValue)
        {
            if (actualValue == null)
                return String.IsNullOrWhiteSpace(expectedValue);

            if (expectedValue == "<Hoje>")
            {
                var comparerDt = GetComparer(typeof(DateTime));
                return comparerDt.Compare(DateTime.Now.Date.ToShortDateString(), ((DateTime)actualValue).Date);
            }

            if (expectedValue == "<Inteiro>")
            {
                var numStr = actualValue.ToString();
                if (numStr == "0")
                    return false;

                return new Regex(@"\d*").IsMatch(numStr);
            }

            if (expectedValue == "<Decimal>")
            {
                var numStr = actualValue.ToString();
                Decimal convertedNumber;

                return Decimal.TryParse(numStr, out convertedNumber);
            }


            if (expectedValue == "<String>")
            {
                return !String.IsNullOrWhiteSpace(actualValue.ToString());
            }

            var value = expectedValue;

            value = Translator.Translate(value);

            var comparer = GetComparer(actualValue.GetType());
            return comparer.Compare(value, actualValue);
        }

        private IValueComparer GetComparer(Type type)
        {
            switch (type.Name)
            {
                case "Boolean":
                    return new BoolValueComparer();
                case "DateTime":
                    return new DateTimeValueComparer();
                case "Decimal":
                    return new DecimalValueComparer();
                case "Double":
                    return new DoubleValueComparer();
                case "Float":
                    return new FloatValueComparer();
                case "Guid":
                    return new GuidValueComparer(new GuidValueRetriever());
                default:
                    return new DefaultValueComparer();
            }
        }

        private T ObterDoScenarioContext<T>(String key) where T : class
        {
            return ScenarioContext.Current[key] as T;
        }
    }
}
