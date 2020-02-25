using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Data
{
    public class CreateDataBaseScript
    {
        #region Create DataBase
        /*
                USE[master]
        GO
        CREATE DATABASE[DesafioDiegoBenedetti]
         CONTAINMENT = NONE
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET COMPATIBILITY_LEVEL = 120
        GO

        IF(1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
        begin
        EXEC[DesafioDiegoBenedetti].[dbo].[sp_fulltext_database] @action = 'enable'
        end
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET ANSI_NULL_DEFAULT OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET ANSI_NULLS OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET ANSI_PADDING OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET ANSI_WARNINGS OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET ARITHABORT OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET AUTO_CLOSE ON
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET AUTO_SHRINK OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET AUTO_UPDATE_STATISTICS ON
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET CURSOR_CLOSE_ON_COMMIT OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET CURSOR_DEFAULT  GLOBAL
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET CONCAT_NULL_YIELDS_NULL OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET NUMERIC_ROUNDABORT OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET QUOTED_IDENTIFIER OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET RECURSIVE_TRIGGERS OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET ENABLE_BROKER
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET DATE_CORRELATION_OPTIMIZATION OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET TRUSTWORTHY OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET ALLOW_SNAPSHOT_ISOLATION OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET PARAMETERIZATION SIMPLE
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET READ_COMMITTED_SNAPSHOT ON
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET HONOR_BROKER_PRIORITY OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET RECOVERY SIMPLE
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET  MULTI_USER
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET PAGE_VERIFY CHECKSUM
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET DB_CHAINING OFF
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF)
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET TARGET_RECOVERY_TIME = 0 SECONDS
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET DELAYED_DURABILITY = DISABLED
        GO

        ALTER DATABASE[DesafioDiegoBenedetti] SET  READ_WRITE
        GO*/
        #endregion

        #region Create Table Movie
        /*
                     USE [DesafioDiegoBenedetti]
            GO

            SET ANSI_NULLS ON
            GO

            SET QUOTED_IDENTIFIER ON
            GO

            CREATE TABLE [dbo].[Movie](
	            [idMovie] [uniqueidentifier] NOT NULL,
	            [name] [nvarchar](50) NOT NULL,
	            [createdAt] [datetime2](7) NOT NULL,
	            [status] [bit] NOT NULL,
	            [idGenreMovie] [uniqueidentifier] NOT NULL,
             CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
            (
	            [idMovie] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]

            GO

            ALTER TABLE [dbo].[Movie]  WITH CHECK ADD  CONSTRAINT [FK_Movie_MovieGenre_idGenreMovie] FOREIGN KEY([idGenreMovie])
            REFERENCES [dbo].[MovieGenre] ([idGenreMovie])
            ON DELETE CASCADE
            GO

            ALTER TABLE [dbo].[Movie] CHECK CONSTRAINT [FK_Movie_MovieGenre_idGenreMovie]
            GO



         */
        #endregion

        #region Create Table MovieGenre
        /*
                USE[DesafioDiegoBenedetti]
        GO

        SET ANSI_NULLS ON
        GO

        SET QUOTED_IDENTIFIER ON
        GO

        CREATE TABLE[dbo].[MovieGenre]
                (

           [idGenreMovie][uniqueidentifier] NOT NULL,

           [name] [nvarchar] (50) NOT NULL,

            [createdAt] [datetime2] (7) NOT NULL,

             [ativo] [bit]
                NOT NULL,
          CONSTRAINT[PK_MovieGenre] PRIMARY KEY CLUSTERED
        (
            [idGenreMovie] ASC
        )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
        ) ON[PRIMARY]

        GO*/
        #endregion

        #region Create Table RentalMovie
        /*
                USE[DesafioDiegoBenedetti]
        GO

        SET ANSI_NULLS ON
        GO

        SET QUOTED_IDENTIFIER ON
        GO

        CREATE TABLE[dbo].[RentalMovie]
                (

           [idRentalMovie][uniqueidentifier] NOT NULL,

           [rentalDate] [datetime2] (7) NOT NULL,

            [cpf] [nvarchar] (450) NOT NULL,

             [idMovie] [uniqueidentifier]
                NOT NULL,
          CONSTRAINT[PK_RentalMovie] PRIMARY KEY CLUSTERED
        (
            [idRentalMovie] ASC
        )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
        ) ON[PRIMARY]

        GO

        ALTER TABLE[dbo].[RentalMovie] WITH CHECK ADD CONSTRAINT[FK_RentalMovie_Movie_idMovie] FOREIGN KEY([idMovie])
        REFERENCES[dbo].[Movie]
                ([idMovie])
        ON DELETE CASCADE
        GO

        ALTER TABLE[dbo].[RentalMovie]
                CHECK CONSTRAINT[FK_RentalMovie_Movie_idMovie]
        GO

        ALTER TABLE[dbo].[RentalMovie] WITH CHECK ADD CONSTRAINT[FK_RentalMovie_User_cpf] FOREIGN KEY([cpf])
        REFERENCES[dbo].[User]
                ([cpf])
        ON DELETE CASCADE
        GO

        ALTER TABLE[dbo].[RentalMovie]
                CHECK CONSTRAINT[FK_RentalMovie_User_cpf]
        GO*/
        #endregion

        #region Create Table User
        /*          USE[DesafioDiegoBenedetti]
          GO

          SET ANSI_NULLS ON
          GO

          SET QUOTED_IDENTIFIER ON
          GO

          CREATE TABLE[dbo].[User]
                  (

             [cpf][nvarchar](450) NOT NULL,

            [fullName] [nvarchar] (50) NOT NULL,

             [nameUser] [nvarchar] (20) NOT NULL,

              [password] [nvarchar] (50) NOT NULL,

               [status] [bit]
                  NOT NULL,

               [createdAt] [datetime2] (7) NOT NULL,
             CONSTRAINT[PK_User] PRIMARY KEY CLUSTERED
           (
               [cpf] ASC
           )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
          ) ON[PRIMARY]

          GO*/
        #endregion

        #region Insert MovieGenre 
        /*
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Ação','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Animação','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Aventura','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Cinema de arte','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Chanchada','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Comédia romântica','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Comédia dramática','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Comédia de ação','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Dança','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Documentário','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Docuficção','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Espionagem','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Faroeste','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Fantasia científica','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Ficção científica','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Filmes de guerra','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Musical','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Filme policial','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Romance','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Seriado','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Suspense','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Terror','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Comédia','1', SYSDATETIME());
    insert into MovieGenre(idGenreMovie, name, ativo, createdAt) values(NEWID(),'Drama','1', SYSDATETIME());
    */
        #endregion
    }
}
