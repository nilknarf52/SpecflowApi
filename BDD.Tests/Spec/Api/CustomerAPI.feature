#language: pt-br

Funcionalidade: Cliente (Customer)
	Consumir a API do Customer 
	assim poderemos fazer as integrações necessárias no sistema 
	
Cenario: Listar todos os clientes 
	Dado que a url do endpoint é 'http://localhost:64861/api/customer'
	E o método http é 'GET'
	Quando chamar o servico
	Entao statuscode da resposta deverá ser 'OK'
	#E uma resposta com a uma lista do tipo 'WebApplication.Models.CustomerModel' deve ser retornada com os seguintes valores:
	#| NomeCompleto  | Endereco         | DataNascimento | CPF            | Email                      |
	#| Rafael Cruz   | Rua Teste de API | 13/03/1981     | 000.000.000-00 | rafaelcruz.net81@gmail.com |
	#| Rafael Cruz 2 | Rua Teste de API | 13/03/1981     | 111.111.111-11 | rafaelcruz.net81@gmail.com |
	#| Rafael Cruz 3 | Rua Teste de API | 13/03/1981     | 222.222.222-22 | rafaelcruz.net81@gmail.com |

@criarCliente	
Cenario: Consultar um Cliente pelo seu Identificador
	Dado que a url do endpoint é 'http://localhost:64861/api/customer'
	E informei o seguinte argumento de rota 'id' de cenario
	E o método http é 'GET'
	Quando chamar o servico
	Entao statuscode da resposta deverá ser 'OK'
	E uma resposta do tipo 'WebApplication.Models.CustomerModel' deve ser retornada com os seguintes valores:
	| Id        | NomeCompleto | Endereco         | DataNascimento | CPF            | Email                      | DataCadastro |
	| <Inteiro> | Rafael Cruz  | Rua Teste de API | 13/03/1981     | 000.000.000-00 | rafaelcruz.net81@gmail.com | <Hoje>       |

Cenario: Criar um Cliente
	Dado que a url do endpoint é 'http://localhost:64861/api/customer'
	E o método http é 'POST'
	E informei o seguinte argumento do tipo 'WebApplication.Models.CustomerModel':
	| NomeCompleto | Endereco         | DataNascimento | CPF            | Email                      |
	| Rafael Cruz  | Rua Teste de API | 13/03/1981     | 000.000.000-00 | rafaelcruz.net81@gmail.com |
	Quando chamar o servico
	Entao statuscode da resposta deverá ser 'Created'
	E uma resposta do tipo 'WebApplication.Models.CustomerModel' deve ser retornada com os seguintes valores:
	| NomeCompleto | Endereco         | DataNascimento | CPF            | Email                      | Id        |
	| Rafael Cruz  | Rua Teste de API | 13/03/1981     | 000.000.000-00 | rafaelcruz.net81@gmail.com | <Inteiro> |
