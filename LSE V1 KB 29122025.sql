-- =====================================================
--  London Stock Exchange - DB Script KB V1 - 29-Dec-2025 
-- =====================================================

USE [master]
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'LondonStocksExchange')
    DROP DATABASE [LondonStocksExchange]
GO

CREATE DATABASE [LondonStocksExchange]
 ON PRIMARY ( NAME = N'LondonStocksExchange', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\LondonStocksExchange.mdf' )
 LOG ON ( NAME = N'LondonStocksExchange_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\LondonStocksExchange_log.ldf' )
GO

ALTER DATABASE [LondonStocksExchange] SET COMPATIBILITY_LEVEL = 160
ALTER DATABASE [LondonStocksExchange] SET ENABLE_BROKER 
ALTER DATABASE [LondonStocksExchange] SET QUERY_STORE = ON
GO

USE [LondonStocksExchange]
GO

-- 1. ✅ Brokers (COMPLETE columns)
CREATE TABLE [dbo].[Brokers](
    [Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
    [BrokerCode] [varchar](50) NOT NULL,
    [BrokerName] [varchar](200) NOT NULL,
    [IsActive] [bit] NOT NULL DEFAULT 1,
    [CreatedDate] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedDate] [datetime2](7) NULL,
    CONSTRAINT [PK_Brokers] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

-- 2. ✅ BrokerUsers (Password → test@123)
CREATE TABLE [dbo].[BrokerUsers](
    [Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
    [BrokerId] [varchar](50) NOT NULL,
    [Username] [varchar](100) NOT NULL,
    [Password] [varchar](100) NOT NULL,  -- ✅ CHANGED from PasswordHash
    [CreatedAt] [datetime2](7) NOT NULL DEFAULT SYSUTCDATETIME(),
    [IsActive] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [PK_BrokerUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

-- 3. ✅ Stocks (COMPLETE columns)
CREATE TABLE [dbo].[Stocks](
    [Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
    [Ticker] [varchar](10) NOT NULL,
    [CompanyName] [varchar](200) NOT NULL,
    [IsActive] [bit] NOT NULL DEFAULT 1,
    [CreatedDate] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedDate] [datetime2](7) NULL,  -- ✅ ADDED
    CONSTRAINT [PK_Stocks] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [dbo].[Trades](
    [Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
    [Ticker] [varchar](10) NOT NULL,
    [Price] [decimal](18, 4) NOT NULL,
    [Quantity] [decimal](18, 4) NOT NULL,
    [BrokerId] [varchar](50) NOT NULL,
    [Side] [varchar](4) NOT NULL,
    [ExecutedAt] [datetime2](7) NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [PK_Trades] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [dbo].[StockSummary](
    [Ticker] [varchar](10) NOT NULL,
    [TotalPrice] [decimal](38, 4) NOT NULL,
    [TradeCount] [bigint] NOT NULL,
    [AvgPrice] AS ([TotalPrice]/[TradeCount]) PERSISTED,
    CONSTRAINT [PK_StockSummary] PRIMARY KEY CLUSTERED ([Ticker] ASC)
)
GO

-- 4. INDEXES
ALTER TABLE [dbo].[Brokers] ADD CONSTRAINT [UQ_Brokers_BrokerCode] UNIQUE ([BrokerCode])
CREATE INDEX [IX_Brokers_BrokerCode] ON [dbo].[Brokers] ([BrokerCode])
ALTER TABLE [dbo].[BrokerUsers] ADD CONSTRAINT [UQ_BrokerUsers_Username] UNIQUE ([Username])
ALTER TABLE [dbo].[Stocks] ADD CONSTRAINT [UQ_Stocks_Ticker] UNIQUE ([Ticker])
CREATE INDEX [IX_Stocks_Ticker] ON [dbo].[Stocks] ([Ticker])
CREATE INDEX [IX_Trades_Ticker_ExecutedAt] ON [dbo].[Trades] ([Ticker], [ExecutedAt] DESC)
CREATE INDEX [IX_Trades_BrokerId] ON [dbo].[Trades] ([BrokerId])
GO

-- 5. ✅ PRODUCTION DATA
INSERT [dbo].[Brokers] ([BrokerCode], [BrokerName]) VALUES 
('BRK-JPM', 'J.P. Morgan Securities London'),
('BRK-CITI', 'Citigroup Global Markets Ltd'),
('BRK-GS', 'Goldman Sachs International'),
('BRK-HSBC', 'HSBC Bank plc'),
('BRK-BARX', 'Barclays Capital')
GO

-- ✅ Password = test@123
INSERT [dbo].[BrokerUsers] ([BrokerId], [Username], [Password]) VALUES 
('BRK-JPM', 'jpm.trader@lse.com', 'test@123'),
('BRK-CITI', 'citi.trader@lse.com', 'test@123'),
('BRK-GS', 'gs.trader@lse.com', 'test@123')
GO

INSERT [dbo].[Stocks] ([Ticker], [CompanyName]) VALUES 
('LSE:BARC', 'Barclays PLC'),
('LSE:BP.', 'BP p.l.c.'),
('LSE:HSBA', 'HSBC Holdings plc'),
('LSE:VOD', 'Vodafone Group plc'),
('LSE:SHEL', 'Shell plc'),
('LSE:AZN', 'AstraZeneca PLC'),
('LSE:RR.', 'Rolls-Royce Holdings plc'),
('LSE:GLEN', 'Glencore plc'),
('LSE:BA.', 'Bae Systems plc'),
('LSE:SMIN', 'Smiths Group plc')
GO

-- Initial StockSummary (all stocks)
INSERT [dbo].[StockSummary] ([Ticker], [TotalPrice], [TradeCount]) VALUES 
('LSE:BARC', 21500.00, 10),
('LSE:BP.', 118000.00, 25),
('LSE:HSBA', 34250.00, 5),
('LSE:VOD', 73000.00, 100),
('LSE:SHEL', 28750.00, 1),
('LSE:AZN', 12500.00, 2),
('LSE:RR.', 4500.00, 5),
('LSE:GLEN', 5200.00, 8),
('LSE:BA.', 1350.00, 3),
('LSE:SMIN', 1850.00, 2)
GO

INSERT [dbo].[Trades] ([Ticker], [Price], [Quantity], [BrokerId], [Side]) VALUES 
('LSE:BARC', 2.18, 5000, 'BRK-JPM', 'BUY'),
('LSE:BP.', 4.75, 10000, 'BRK-CITI', 'SELL'),
('LSE:VOD', 0.74, 50000, 'BRK-GS', 'BUY')
GO

-- 6. VALIDATION
SELECT '✅ PERFECT SETUP COMPLETE!' AS Status
SELECT 'Table', 'Count' UNION ALL
SELECT 'Brokers', CAST(COUNT(*) AS varchar) FROM [dbo].[Brokers] UNION ALL
SELECT 'BrokerUsers', CAST(COUNT(*) AS varchar) FROM [dbo].[BrokerUsers] UNION ALL
SELECT 'Stocks', CAST(COUNT(*) AS varchar) FROM [dbo].[Stocks] UNION ALL
SELECT 'StockSummary', CAST(COUNT(*) AS varchar) FROM [dbo].[StockSummary];

-- ✅ TEST LOGIN
SELECT Username, Password, BrokerId FROM [dbo].[BrokerUsers] WHERE Password = 'test@123';
