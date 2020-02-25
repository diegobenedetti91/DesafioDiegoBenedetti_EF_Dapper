# DesafioDiegoBenedetti_EF_Dapper

Primeiro passo:
Acessar seu SQL Management Studio e criar a base de dados, o Script esta na pasta do Projeto Data DesafioDiegoBenedetti_EF_Dapper/Data/
e abrir o arquivo CreateDatabase Script. Verifique que contém os insert da tabela genero.
Caso prefira utilizar os comandos Migrations do Visual Studio, que irá criar o banco de dados.


#Configurando a Conexão com o Banco de dados
No arquivo appsettings.json, precisa informar na ConnectionString o DataSource do seu SQL Server, e o nome da Base de Dados(Default name Database = DesafioDiegoBenedetti).
Caso queira utilizar a connection string do banco de dados do Azure abaixo segue a conexão para alterar no appsettings.json:
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=dbserverdesafiobenedetti.database.windows.net;Initial Catalog=DesafioDiegoBenedetti;User ID=diego;Password=Santos0291;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;"
  }


