CREATE TABLE [dbo].[Transactions](
	[Account] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
	[CurrencyCode] [nvarchar](3) NOT NULL,
	[Value] [numeric](18, 0) NOT NULL
) ON [PRIMARY]
