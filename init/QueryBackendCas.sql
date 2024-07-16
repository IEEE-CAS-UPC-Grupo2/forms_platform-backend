
CREATE DATABASE [BackendCas]
GO
USE [BackendCas]
GO
/****** Object:  Table [dbo].[administrators]    Script Date: 10/07/2024 21:02:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[administrators](
	[id_administrator] [int] IDENTITY(1,1) NOT NULL,
	[name_administrator] [varchar](255) NULL,
	[email_administrator] [varchar](255) NULL,
	[password_administrator] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_administrator] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO






USE [BackendCas]
GO
/****** Object:  Table [dbo].[events_cas]    Script Date: 10/07/2024 21:02:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[events_cas](
	[id_event] [int] IDENTITY(1,1) NOT NULL,
	[event_title] [varchar](255) NULL,
	[event_description] [text] NULL,
	[image_url] [varchar](255) NULL,
	[modality] [varchar](255) NULL,
	[institution_in_charge] [varchar](255) NULL,
	[vacancy] [int] NULL,
	[address_event] [varchar](255) NULL,
	[speaker] [varchar](255) NULL,
	[event_date_time] [datetime] NULL,
	[event_duration] [int] NULL,
	[id_administrator] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_event] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[events_cas]  WITH CHECK ADD  CONSTRAINT [fk_id_administrator] FOREIGN KEY([id_administrator])
REFERENCES [dbo].[administrators] ([id_administrator])
GO
ALTER TABLE [dbo].[events_cas] CHECK CONSTRAINT [fk_id_administrator]
GO



USE [BackendCas]
GO
/****** Object:  Table [dbo].[HistorialRefreshToken]    Script Date: 10/07/2024 21:02:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialRefreshToken](
	[IdHistorialToken] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[Token] [varchar](500) NULL,
	[RefreshToken] [varchar](200) NULL,
	[FechaCreacion] [datetime] NULL,
	[FechaExpiracion] [datetime] NULL,
	[EsActivo]  AS (case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end),
PRIMARY KEY CLUSTERED 
(
	[IdHistorialToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HistorialRefreshToken]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[administrators] ([id_administrator])
GO



USE [BackendCas]
GO
/****** Object:  Table [dbo].[Participant]    Script Date: 10/07/2024 21:02:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Participant](
	[IdParticipant] [int] IDENTITY(1,1) NOT NULL,
	[Dni] [nvarchar](50) NULL,
	[Name] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[StudyCenter] [nvarchar](100) NULL,
	[Career] [nvarchar](100) NULL,
	[IeeeMembershipCode] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdParticipant] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



USE [BackendCas]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 10/07/2024 21:02:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[IdAttendance] [int] IDENTITY(1,1) NOT NULL,
	[IdParticipant] [int] NULL,
	[IdEvent] [int] NULL,
	[Dni] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAttendance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([IdEvent])
REFERENCES [dbo].[events_cas] ([id_event])
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([IdParticipant])
REFERENCES [dbo].[Participant] ([IdParticipant])
GO



USE [BackendCas]
GO
/****** Object:  Table [dbo].[Certificate]    Script Date: 10/07/2024 21:02:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Certificate](
	[IdCertificate] [int] IDENTITY(1,1) NOT NULL,
	[IdParticipant] [int] NULL,
	[IdEvent] [int] NULL,
	[IsDelivered] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCertificate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD FOREIGN KEY([IdEvent])
REFERENCES [dbo].[events_cas] ([id_event])
GO
ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD FOREIGN KEY([IdParticipant])
REFERENCES [dbo].[Participant] ([IdParticipant])
GO
