#Abioka Scrum#

Abioka Scrum is an open source lite scrum tool and trello alternative. You can find a demo [here](http://scrum.abioka.com/).

##Technologies used##
- HTML5
- AngularJS (according to [John Papa Style Guide](https://github.com/johnpapa/angular-styleguide/blob/master/a1/README.md))
- Asp.Net Web Api
-	Sql Server 
- Mongodb (not completed)
- Json Web Token
- Google OAuth

##Installation##
1. Create the sql server database using the installation script. You can find the script below.
2. Replace connection string value with the new database informations recently installed.
3. For RestApi deploy [api](api) as a web application or a web site on IIS
4. Open [app.config.js](www/app/app.config.js). Replace apiUrl value with the new installed application's url and replace googleClientId value with the your application client id. Click [here](https://developers.google.com/identity/sign-in/web/devconsole-project) for more information.
5. For Web UI deploy [www](www) folder as a web application or a web site.


##Installation Script##

```
GO
CREATE TABLE [dbo].[Board](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedUser] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL
)

GO
CREATE TABLE [dbo].[BoardUser](
	[BoardId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL
)

GO
CREATE TABLE [dbo].[Card](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[EstimatedPoints] [tinyint] NOT NULL,
	[ListId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Order] [int] NOT NULL
)

GO
CREATE TABLE [dbo].[CardLabel](
	[CardId] [uniqueidentifier] NOT NULL,
	[LabelId] [uniqueidentifier] NOT NULL
)

GO
CREATE TABLE [dbo].[CardUser](
	[CardId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL
)

GO
CREATE TABLE [dbo].[Comment](
	[Id] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](4000) NOT NULL,
	[CardId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL
)

GO
CREATE TABLE [dbo].[Label](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Type] [varchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL
)

GO
CREATE TABLE [dbo].[List](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[BoardId] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL
)

GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Initials] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](300) NOT NULL,
	[Password] [varchar](64) NULL,
	[Token] [varchar](3000) NULL,
	[ImageUrl] [varchar](500) NULL,
	[IsDeleted] [bit] NOT NULL,
	[ProviderToken] [nvarchar](3000) NULL,
	[AuthProvider] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL
)
```
