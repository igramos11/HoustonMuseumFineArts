CREATE TABLE [dbo].[employees] (
    [id]         INT           IDENTITY (1, 1) NULL,
    [name]       VARCHAR (100) NULL,
    [social]     VARCHAR (10)  NULL,
    [email]      VARCHAR (150) NULL,
    [phone]      VARCHAR (20)  NULL,
    [address]    VARCHAR (100) NULL,
    [created_at] DATETIME      DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([email] ASC)
);

