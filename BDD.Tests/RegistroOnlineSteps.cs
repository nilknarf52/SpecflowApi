using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using OpenQA.Selenium.IE;

namespace BDD.Tests
{
    [Binding]
    public class RegistroOnlineSteps
    {
        private IWebDriver Browser;

        [BeforeScenario("Robo")]
        public void CreateWebDriver()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();

            ChromeOptions chromeOpts = new ChromeOptions();
            chromeOpts.AddArgument("--headless");
            
            //Cria a instancia do browser antes de executar os cenarios
            Browser = new ChromeDriver(service, chromeOpts);
        }

        [AfterScenario("Robo")]
        public void CloseWebDriver()
        {
            //Fecha o browser depois que termina os cenarios
            Browser.Close();
            Browser.Dispose();
        }

        [Given(@"naveguei para a página de cadastro")]
        public void DadoNavegueiParaAPaginaDeCadastro()
        {
            //Navega para a URL da pagina de Cadastro
            Browser.Navigate().GoToUrl("http://localhost:64861/Account/Create");
        }

        [Given(@"não inseri a informação de email")]
        public void DadoNaoInseriAsInformacoesDeEmail()
        {
            //Pegando os elementos e gerando valores de testes
            var txtNome = Browser.FindElement(By.Id("Nome"));
            var txtPassword = Browser.FindElement(By.Id("Password"));

            //Envia os dados para o formulário
            txtNome.SendKeys("Rafael Cruz");
            txtPassword.SendKeys("123Mudar");
        }

        [Then(@"pagina de cadastro deve exibir uma mensagem de erro")]
        public void EntaoPaginaDeCadastroDeveExibirUmaMensagemDeErro()
        {
            Assert.IsTrue(Browser.FindElement(By.XPath("//span[@data-valmsg-for='Email']")).Text == "Email é obrigatório");
        }



        [Given(@"que sou um novo usuário")]
        public void DadoQueSouUmNovoUsuario()
        {
            
        }


        [Given(@"inseri todas as informações do formulário corretas")]
        public void DadoInseriTodasAsInformacoesDoFormularioCorretas()
        {
            //Pegando os elementos e gerando valores de testes
            var txtNome = Browser.FindElement(By.Id("Nome"));
            var txtEmail = Browser.FindElement(By.Id("Email"));
            var txtPassword = Browser.FindElement(By.Id("Password"));

            //Envia os dados para o formulário
            txtNome.SendKeys("Rafael Cruz");
            txtEmail.SendKeys("teste@teste.com.br");
            txtPassword.SendKeys("123Mudar");
        }


        [When(@"clicar no botão de criar conta")]
        public void QuandoClicarNoBotaoDeCriarConta()
        {
            var btnCriarConta = Browser.FindElement(By.Id("btnSubmit"));
            //Faz o submit no formulario
            btnCriarConta.Submit();
        }


        [Then(@"o usuário deve ser redirecionado para a Home Page")]
        public void EntaoOUsuarioDeveSerRedirecionadoParaAHomePage()
        {
            Assert.IsTrue(Browser.Title == "Home Page - My ASP.NET Application");

        }
     
    }
}
