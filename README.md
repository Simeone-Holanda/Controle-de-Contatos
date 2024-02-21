# Sistema de Controle de Cadastro

## Funcionalidades: <br>

* Cadastro e Login de Usuários do tipo Admin ou Comum
* Gerenciamento de sessões e permissões de acordo com o tipo do perfil
* Criação de listas de contatos por usuário
* Cryptografia e troca de senha
* Resete de senha com envio de email

## Tecnologias: <br>

* Asp.Net Core MVC
* Entity Framework(Postgres)
* Boostrap 
* Razor Pages
* Jquery
* Javascrip

## Pré-requisitos
- [.NET Core SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0) - Certifique-se de ter o SDK do .NET Core instalado na sua máquina. Para esse projeto foi usado a versão 6.0

## Execução e Configuração
* git clone https://github.com/Simeone-Holanda/Controle-de-Contatos.git
* Lembre-se de fazer configuração do seu banco de dados e do serviço de Email em appsettings.json. Caso não seja configurado corretamente, é possível que se tenha alguns problemas.
* No seu Package Manager Console no Visual Studio use o comando a seguir para subir as migrations: Update-Database -Context ConnectionContext
* Execute o comando abaixo para adicionar o usuário padrão no seu banco de dados. A senha será 123456.<br>
```
INSERT INTO public."Users" ("Name", "Login", "Email", "Profile", "Password", "CreatedAt")
VALUES ('Administrador', 'Admin', 'admin@admin.com', 1, '7c4a8d09ca3762af61e59520943dc26494f8941b', CURRENT_TIMESTAMP);
```
* Agora so executar no seu visual studio code com o comando ctrl f5

<br>

*OBS: O intuito desse projeto foi evoluir com os estudos em C# ampliando os conhecimento para Asp.Net Core MVC.*