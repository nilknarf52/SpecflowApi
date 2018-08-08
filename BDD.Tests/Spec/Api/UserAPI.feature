#language: pt-br

Funcionalidade: Usuario (User)
	Consumir a API de User
	assim poderemos fazer as integrações necessárias no sistema 
	
Cenario: Listar todos os usuarios
	Dado que a url do endpoint é 'http://localhost:64861/api/user'
	E o método http é 'GET'
	Quando chamar o servico
	Entao statuscode da resposta deverá ser 'OK'
	E uma resposta com a uma lista do tipo 'WebApplication.Models.UserModel' deve ser retornada com os seguintes valores:
	| Nome   | Email               | Password |
	| Teste  | teste@teste.com.br  | 123456A# |
	| Teste2 | teste2@teste.com.br | 123456A# |

Cenario: Consultar um usuario pelo seu Identificador
	Dado que a url do endpoint é 'http://localhost:64861/api/user/1'
	E o método http é 'GET'
	Quando chamar o servico
	Entao statuscode da resposta deverá ser 'OK'
	E uma resposta do tipo 'WebApplication.Models.UserModel' deve ser retornada com os seguintes valores:
	| Nome   | Email               | Password |
	| Teste  | teste@teste.com.br  | 123456A# |