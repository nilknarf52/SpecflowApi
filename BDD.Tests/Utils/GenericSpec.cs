using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BDD.Tests.Utils
{
    [Binding]
    public partial class GenericSpecs
    {

        #region Given

        [Given(@"o método http é '(.*)'")]
        public void EOMetodoHttpE(string p0)
        {
            var metodo =   Method.POST;

            switch (p0.ToUpper())
            {
                case "POST":
                    metodo = Method.POST;
                    break;
                case "GET":
                    metodo = Method.GET;
                    break;
                case "PUT":
                    metodo = Method.PUT;
                    break;
                case "DELETE":
                    metodo = Method.DELETE;
                    break;
                case "PATCH":
                    metodo = Method.PATCH;
                    break;
                default:
                    Assert.Fail("Método HTTP não esperado");
                    break;
            }

            ScenarioContext.Current["HttpMethod"] = metodo;
        }

        [Given(@"que a url do endpoint é '(.*)'")]
        public void DadoQueAUrlDoEndpointE(string p0)
        {
            ScenarioContext.Current["Endpoint"] = p0;
        }
        
        [Given(@"informei o seguinte argumento de rota '(.*)' de cenario")]
        public void DadoInformeiOSeguinteArgumentoDeUrlDeCenario(string p0)
        {
            var endpoint = ScenarioContext.Current["Endpoint"];
            var args = ScenarioContext.Current[p0];

            if (endpoint.ToString().EndsWith(""))
                endpoint += $"/{args}";
            else
                endpoint += $"{args}";

            ScenarioContext.Current["Endpoint"] = endpoint;
        }



        [Given(@"informei o seguinte argumento do tipo '(.*)':")]
        public void EOsSeguintesValores(string tipoModel, Table table)
        {
            
            var tipo = Activator.CreateInstance("WebApplication", tipoModel).Unwrap();

            var arg = table.CreateInstance<WebApplication.Models.CustomerModel>();

            //var reflValidarResposta = typeof(TableHelperExtensionMethods).GetMethod("CreateInstance", new[] { typeof(Table) }).GetGenericMethodDefinition();
            //var genericMethod = reflValidarResposta.MakeGenericMethod(tipo.GetType());
            //var arg = genericMethod.Invoke(null, new[] { table });

            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(arg, Formatting.Indented, jsonSerializerSettings);

            ScenarioContext.Current["Data"] = json;
        }



        #endregion

        #region When

        [When(@"chamar o servico")]
        public void QuandoChamarOServico()
        {
            var endpoint = (String)ScenarioContext.Current["Endpoint"];

            ExecutarRequest(endpoint);
        }

        #endregion

        #region Then

        [Then(@"statuscode da resposta deverá ser '(.*)'")]
        public void EntaoStatuscodeDaRespostaDeveraSer(string p0)
        {
            var response = (IRestResponse)ScenarioContext.Current["Response"];

            string errorMessage;

            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.NotFound:
                    errorMessage = "ResponseUri: " + response.ResponseUri;
                    break;
                case HttpStatusCode.Forbidden:
                    var auth = response.Request.Parameters.Where(x => x.Name == "Authorization").FirstOrDefault();
                    errorMessage = "Authorization: " + (auth != null ? auth.Value : "none");
                    break;
                default:
                    errorMessage = response.Content;
                    break;
            }

            Assert.AreEqual(p0, response.StatusCode.ToString(), errorMessage);
        }

       

        [Then(@"uma resposta do tipo '(.*)' deve ser retornada com os seguintes valores:")]
        public void EntaoUmaRespostaDoTipoXSerRetornadaComOsSeguintesValores(string tipoModel, Table table)
        {
            var tipoModelType = Activator.CreateInstance("WebApplication", tipoModel).Unwrap();

            try
            {
                this.ValidarResposta(tipoModelType.GetType(),table);

            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                else

                    throw;
            }
        }

        [Then(@"uma resposta com a uma lista do tipo '(.*)' deve ser retornada com os seguintes valores:")]
        public void EntaoUmaRespostaDeUmaListaXDeveSerRetornadaComOsSeguintesValores(string tipoModel, Table table)
        {
            var tipoModelType = Activator.CreateInstance("WebApplication", tipoModel).Unwrap();

            var listType = typeof(IEnumerable<>).MakeGenericType(tipoModelType.GetType());

            try
            {
                var response = (IRestResponse)ScenarioContext.Current["Response"];
                var content = response.Content;

                var model = JsonConvert.DeserializeObject(content, listType, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                });

                var reflValidarResposta = typeof(SetComparisonExtensionMethods).GetMethod("CompareToSet", System.Reflection.BindingFlags.Instance | BindingFlags.Static | System.Reflection.BindingFlags.Public).GetGenericMethodDefinition();

                var genericMethod = reflValidarResposta.MakeGenericMethod(tipoModelType.GetType());

                genericMethod.Invoke(null, new[] { table, model });
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                else
                    throw;
            }
        }



        #endregion

        #region Public

        public static IRestResponse ExecutarRequest(String endpoint)
        {
            var url = endpoint;

            var request = new RestRequest();

            request.Method = (Method)ScenarioContext.Current["HttpMethod"];

            request.Parameters.Clear();

            if (request.Method == Method.POST || request.Method == Method.PUT || request.Method == Method.PATCH)
            {
                var json = (String)ScenarioContext.Current["Data"];

                if (!String.IsNullOrWhiteSpace(json))
                    request.AddParameter("application/json", json, ParameterType.RequestBody);
            }


            var restClient = new RestClient(url);

            var response = restClient.Execute(request);

            ScenarioContext.Current["Response"] = response;

            return response;
        }
       
     

        public void ValidarResposta(Type typeToCompare, Table table)
        {
            var response = (IRestResponse)ScenarioContext.Current["Response"];
            var content = response.Content;

            var model = JsonConvert.DeserializeObject(content, typeToCompare, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });

            ScenarioContext.Current["ResponseModel"] = model;
            table.CompareToInstance(model);
        }
     
        #endregion

    }

}
