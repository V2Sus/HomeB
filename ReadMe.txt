Solution description.
Include two projects: 
CurrenciesWinS - windows service type,
CurrenciesWebS - webapi service type.
CurrenciesWinS for get currency rates from origin site and save data to DB.
CurrenciesWebS for send data to client.
Advantages of CurrenciesWinS:
A separate task is launched for each currency duo.
To add or change currency Duo -  insert or update row in CurrenciesDuo table in DB.
Advantages of CurrenciesWebS:
Responds to Web Get request.
Sends data in JSON formate

To run these services:
1. Create in SQLServer OfakimTest DB
2. In SQLServer execute following script:

USE [OfakimTest]
GO
CREATE TABLE [dbo].[CurrenciesDuo](
	[Name] [nvarchar](10) NOT NULL PRIMARY KEY,
	[Rate] [float] NOT NULL DEFAULT (0),
	[Date] [datetime] NOT NULL DEFAULT (getdate())
) ON [PRIMARY]
GO
INSERT INTO [dbo].[CurrenciesDuo] ([Name]) VALUES ('EUR/USD')
INSERT INTO [dbo].[CurrenciesDuo] ([Name]) VALUES ('EUR/JPY')
INSERT INTO [dbo].[CurrenciesDuo] ([Name]) VALUES ('GBP/EUR')
INSERT INTO [dbo].[CurrenciesDuo] ([Name]) VALUES ('USD/ILS')
GO

3. Check and edit (if needed) connection string in CurrenciesWinS.exe.config and appsettings.json
4. Install CurrenciesWinS service. for example type on the command prompt with admin rights:
c:\Ofakim\OfakimCurrencies\CurrenciesWinS\bin\Release>C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil CurrenciesWinS.exe
4. And start the service:
Net Start Currencies

* Make sure the security settings on the SQL Server are correct