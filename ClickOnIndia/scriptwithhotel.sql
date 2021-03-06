USE [master]
GO
/****** Object:  Database [Db_ClickOnIndia]    Script Date: 1/4/2019 10:30:40 AM ******/
CREATE DATABASE [Db_ClickOnIndia] ON  PRIMARY 
( NAME = N'Db_ClickOnIndia', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MYINSTANCE\MSSQL\DATA\Db_ClickOnIndia.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Db_ClickOnIndia_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MYINSTANCE\MSSQL\DATA\Db_ClickOnIndia_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Db_ClickOnIndia] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Db_ClickOnIndia].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Db_ClickOnIndia] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET ARITHABORT OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Db_ClickOnIndia] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Db_ClickOnIndia] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Db_ClickOnIndia] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Db_ClickOnIndia] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Db_ClickOnIndia] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Db_ClickOnIndia] SET  MULTI_USER 
GO
ALTER DATABASE [Db_ClickOnIndia] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Db_ClickOnIndia] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Db_ClickOnIndia', N'ON'
GO
USE [Db_ClickOnIndia]
GO
/****** Object:  Table [dbo].[tbl_Bus]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Bus](
	[Busid] [int] IDENTITY(1,1) NOT NULL,
	[BusName] [varchar](50) NULL,
	[BusNum] [varchar](50) NULL,
	[Type] [int] NULL,
	[Status] [bit] NULL,
	[Seatcount] [int] NULL,
	[CostsAdult] [decimal](18, 0) NULL,
	[CostsChild] [decimal](18, 0) NULL,
	[Date]  AS (sysdatetime()),
	[StartTime] [time](7) NULL,
	[Uid] [int] NULL,
 CONSTRAINT [PK_tbl_Bus] PRIMARY KEY CLUSTERED 
(
	[Busid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_BusBoking]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BusBoking](
	[Bbid] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NULL,
	[From_BrId] [int] NULL,
	[To_BrId] [int] NULL,
	[TotalAmount] [decimal](18, 0) NULL,
	[Date]  AS (sysdatetime()),
	[Transid] [int] NULL,
	[JourneyDate]  AS (sysdatetime()),
	[Status] [bit] NULL,
	[CancelDate] [datetime] NULL,
 CONSTRAINT [PK_tbl_BusBoking] PRIMARY KEY CLUSTERED 
(
	[Bbid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_BusLoc]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BusLoc](
	[Blid] [int] IDENTITY(1,1) NOT NULL,
	[LocationXY] [varchar](max) NULL,
	[Status] [bit] NULL,
	[Type] [int] NULL,
	[Date] [date] NULL,
	[Uid] [int] NULL,
	[LocationName] [varchar](100) NULL,
 CONSTRAINT [PK_tbl_BusLoc] PRIMARY KEY CLUSTERED 
(
	[Blid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_BusRoute]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BusRoute](
	[BrId] [int] IDENTITY(1,1) NOT NULL,
	[Blid] [int] NULL,
	[Status] [bit] NULL,
	[Sortorder] [int] NULL,
	[Date]  AS (sysdatetime()),
	[BusId] [int] NULL,
 CONSTRAINT [PK_tbl_BusRoute] PRIMARY KEY CLUSTERED 
(
	[BrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Compartment]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Compartment](
	[Compid] [int] IDENTITY(1,1) NOT NULL,
	[CompName] [varchar](50) NULL,
	[Type] [int] NULL,
	[Status] [bit] NULL,
	[Tid] [int] NULL,
	[Date]  AS (sysdatetime()),
	[Uid] [int] NULL,
 CONSTRAINT [PK_tbl_Compartment] PRIMARY KEY CLUSTERED 
(
	[Compid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_HotelBooking]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_HotelBooking](
	[Hbid] [int] IDENTITY(1,1) NOT NULL,
	[Hid] [int] NULL,
	[Rid] [int] NULL,
	[Transid] [int] NULL,
	[Amount] [decimal](18, 0) NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[CancelDate] [datetime] NULL,
	[Status] [bit] NULL,
	[Type] [int] NULL,
	[Uid] [int] NULL,
	[Date]  AS (sysdatetime()),
 CONSTRAINT [PK_tbl_HotelBooking] PRIMARY KEY CLUSTERED 
(
	[Hbid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_HotelRoom]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_HotelRoom](
	[HotelRoomId] [int] IDENTITY(1,1) NOT NULL,
	[Hid] [int] NULL,
	[RoomTypeId] [int] NULL,
	[Status] [bit] NULL,
	[Uid] [int] NULL,
	[Date]  AS (sysdatetime()),
 CONSTRAINT [PK_tbl_HotelRoom] PRIMARY KEY CLUSTERED 
(
	[HotelRoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Hotels]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Hotels](
	[Hid] [int] IDENTITY(1,1) NOT NULL,
	[HotelName] [varchar](200) NULL,
	[Type] [int] NULL,
	[Status] [bit] NULL,
	[RoomCount] [int] NULL,
	[Uid] [int] NULL,
	[Date]  AS (sysdatetime()),
	[Location] [varchar](100) NULL,
 CONSTRAINT [PK_tbl_Hotels] PRIMARY KEY CLUSTERED 
(
	[Hid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Passenger]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Passenger](
	[Pid] [int] IDENTITY(1,1) NOT NULL,
	[PassName] [varchar](50) NULL,
	[PassAge] [int] NULL,
	[PassGender] [varchar](10) NULL,
	[BusBookId] [int] NULL,
	[TrainBookId] [int] NULL,
	[BusSeatNo] [varchar](50) NULL,
	[TrainSeatNo] [varchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_tbl_Pass] PRIMARY KEY CLUSTERED 
(
	[Pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Role]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Role](
	[Rid] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NULL,
	[Status] [bit] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_tbl_Role] PRIMARY KEY CLUSTERED 
(
	[Rid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_RoomTypes]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_RoomTypes](
	[RoomTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NULL,
	[Uid] [int] NULL,
	[Status] [bit] NULL,
	[Bed_Count] [int] NULL,
	[Room_Class] [varchar](100) NULL,
	[Date]  AS (sysdatetime()),
 CONSTRAINT [PK_HotelRooms] PRIMARY KEY CLUSTERED 
(
	[RoomTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Route]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Route](
	[Roid] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [varchar](50) NULL,
	[Status] [bit] NULL,
	[Type] [int] NULL,
	[LocationXY] [varchar](max) NULL,
	[Date]  AS (sysdatetime()),
	[Uid] [int] NULL,
 CONSTRAINT [PK_tbl_Route] PRIMARY KEY CLUSTERED 
(
	[Roid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_SeatClass]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SeatClass](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[SName] [varchar](50) NULL,
	[Type] [int] NULL,
	[Count] [int] NULL,
	[Compid] [int] NULL,
	[CostsAdult] [decimal](18, 0) NULL,
	[CostsChild] [decimal](18, 0) NULL,
	[Date]  AS (sysdatetime()),
 CONSTRAINT [PK_tbl_SeatClass] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Train]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Train](
	[Tid] [int] IDENTITY(1,1) NOT NULL,
	[TrainName] [varchar](50) NULL,
	[Status] [bit] NULL,
	[Type] [int] NULL,
	[Date]  AS (sysdatetime()),
	[StartTime] [time](7) NULL,
	[TrainNum] [varchar](50) NULL,
	[Uid] [int] NULL,
 CONSTRAINT [PK_tbl_train] PRIMARY KEY CLUSTERED 
(
	[Tid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_TrainBooking]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_TrainBooking](
	[Bid] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NULL,
	[Tid] [int] NULL,
	[from_Trid] [int] NULL,
	[to_Trid] [int] NULL,
	[Sid] [int] NULL,
	[TotalAmount] [decimal](18, 0) NULL,
	[Transid] [int] NULL,
	[Status] [bit] NULL,
	[CancelStatus] [bit] NULL,
	[Date]  AS (sysdatetime()),
	[CancelDate] [datetime] NULL,
	[JourneyDate] [date] NULL,
 CONSTRAINT [PK_tbl_Booking] PRIMARY KEY CLUSTERED 
(
	[Bid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_TrainRoute]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_TrainRoute](
	[Trid] [int] IDENTITY(1,1) NOT NULL,
	[Tid] [int] NULL,
	[Roid] [int] NULL,
	[Status] [bit] NULL,
	[SortOrder] [int] NULL,
	[Date]  AS (sysdatetime()),
	[Uid] [int] NULL,
	[TimeBet] [int] NULL,
 CONSTRAINT [PK_tbl_TrainRoute] PRIMARY KEY CLUSTERED 
(
	[Trid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Transaction]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Transaction](
	[Transid] [int] IDENTITY(1,1) NOT NULL,
	[Ran_Transid] [varchar](50) NULL,
	[Price] [decimal](18, 0) NULL,
	[Status] [bit] NULL,
	[StatusMsg] [varchar](50) NULL,
	[Type] [int] NULL,
	[Date]  AS (sysdatetime()),
 CONSTRAINT [PK_tbl_Transaction] PRIMARY KEY CLUSTERED 
(
	[Transid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_UserDetail]    Script Date: 1/4/2019 10:30:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_UserDetail](
	[Uid] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Rid] [int] NULL,
	[Status] [bit] NULL,
	[Date]  AS (sysdatetime()),
	[Address] [varchar](max) NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_UserDetail] PRIMARY KEY CLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_tbl_UserDetail]    Script Date: 1/4/2019 10:30:41 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_UserDetail] ON [dbo].[tbl_UserDetail]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_Bus]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Bus_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_Bus] CHECK CONSTRAINT [FK_tbl_Bus_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_BusBoking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_BusBoking_tbl_BusRoute] FOREIGN KEY([From_BrId])
REFERENCES [dbo].[tbl_BusRoute] ([BrId])
GO
ALTER TABLE [dbo].[tbl_BusBoking] CHECK CONSTRAINT [FK_tbl_BusBoking_tbl_BusRoute]
GO
ALTER TABLE [dbo].[tbl_BusBoking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_BusBoking_tbl_BusRoute1] FOREIGN KEY([To_BrId])
REFERENCES [dbo].[tbl_BusRoute] ([BrId])
GO
ALTER TABLE [dbo].[tbl_BusBoking] CHECK CONSTRAINT [FK_tbl_BusBoking_tbl_BusRoute1]
GO
ALTER TABLE [dbo].[tbl_BusBoking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_BusBoking_tbl_Transaction] FOREIGN KEY([To_BrId])
REFERENCES [dbo].[tbl_BusRoute] ([BrId])
GO
ALTER TABLE [dbo].[tbl_BusBoking] CHECK CONSTRAINT [FK_tbl_BusBoking_tbl_Transaction]
GO
ALTER TABLE [dbo].[tbl_BusBoking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_BusBoking_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_BusBoking] CHECK CONSTRAINT [FK_tbl_BusBoking_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_BusLoc]  WITH CHECK ADD  CONSTRAINT [FK_tbl_BusLoc_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_BusLoc] CHECK CONSTRAINT [FK_tbl_BusLoc_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_BusRoute]  WITH CHECK ADD  CONSTRAINT [FK_tbl_BusRoute_tbl_Bus] FOREIGN KEY([Blid])
REFERENCES [dbo].[tbl_BusLoc] ([Blid])
GO
ALTER TABLE [dbo].[tbl_BusRoute] CHECK CONSTRAINT [FK_tbl_BusRoute_tbl_Bus]
GO
ALTER TABLE [dbo].[tbl_BusRoute]  WITH CHECK ADD  CONSTRAINT [FK_tbl_BusRoute_tbl_Bus1] FOREIGN KEY([BusId])
REFERENCES [dbo].[tbl_Bus] ([Busid])
GO
ALTER TABLE [dbo].[tbl_BusRoute] CHECK CONSTRAINT [FK_tbl_BusRoute_tbl_Bus1]
GO
ALTER TABLE [dbo].[tbl_Compartment]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Compartment_tbl_Train] FOREIGN KEY([Tid])
REFERENCES [dbo].[tbl_Train] ([Tid])
GO
ALTER TABLE [dbo].[tbl_Compartment] CHECK CONSTRAINT [FK_tbl_Compartment_tbl_Train]
GO
ALTER TABLE [dbo].[tbl_Compartment]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Compartment_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_Compartment] CHECK CONSTRAINT [FK_tbl_Compartment_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_HotelBooking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_HotelBooking_tbl_HotelRoom] FOREIGN KEY([Rid])
REFERENCES [dbo].[tbl_HotelRoom] ([HotelRoomId])
GO
ALTER TABLE [dbo].[tbl_HotelBooking] CHECK CONSTRAINT [FK_tbl_HotelBooking_tbl_HotelRoom]
GO
ALTER TABLE [dbo].[tbl_HotelBooking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_HotelBooking_tbl_Hotels] FOREIGN KEY([Hid])
REFERENCES [dbo].[tbl_Hotels] ([Hid])
GO
ALTER TABLE [dbo].[tbl_HotelBooking] CHECK CONSTRAINT [FK_tbl_HotelBooking_tbl_Hotels]
GO
ALTER TABLE [dbo].[tbl_HotelBooking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_HotelBooking_tbl_Transaction] FOREIGN KEY([Transid])
REFERENCES [dbo].[tbl_Transaction] ([Transid])
GO
ALTER TABLE [dbo].[tbl_HotelBooking] CHECK CONSTRAINT [FK_tbl_HotelBooking_tbl_Transaction]
GO
ALTER TABLE [dbo].[tbl_HotelBooking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_HotelBooking_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_HotelBooking] CHECK CONSTRAINT [FK_tbl_HotelBooking_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_HotelRoom]  WITH CHECK ADD  CONSTRAINT [FK_tbl_HotelRoom_tbl_Hotels] FOREIGN KEY([Hid])
REFERENCES [dbo].[tbl_Hotels] ([Hid])
GO
ALTER TABLE [dbo].[tbl_HotelRoom] CHECK CONSTRAINT [FK_tbl_HotelRoom_tbl_Hotels]
GO
ALTER TABLE [dbo].[tbl_HotelRoom]  WITH CHECK ADD  CONSTRAINT [FK_tbl_HotelRoom_tbl_RoomTypes] FOREIGN KEY([RoomTypeId])
REFERENCES [dbo].[tbl_RoomTypes] ([RoomTypeId])
GO
ALTER TABLE [dbo].[tbl_HotelRoom] CHECK CONSTRAINT [FK_tbl_HotelRoom_tbl_RoomTypes]
GO
ALTER TABLE [dbo].[tbl_Hotels]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Hotels_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_Hotels] CHECK CONSTRAINT [FK_tbl_Hotels_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_Passenger]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Pass_tbl_Booking1] FOREIGN KEY([TrainBookId])
REFERENCES [dbo].[tbl_TrainBooking] ([Bid])
GO
ALTER TABLE [dbo].[tbl_Passenger] CHECK CONSTRAINT [FK_tbl_Pass_tbl_Booking1]
GO
ALTER TABLE [dbo].[tbl_Passenger]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Pass_tbl_BusBoking] FOREIGN KEY([BusBookId])
REFERENCES [dbo].[tbl_BusBoking] ([Bbid])
GO
ALTER TABLE [dbo].[tbl_Passenger] CHECK CONSTRAINT [FK_tbl_Pass_tbl_BusBoking]
GO
ALTER TABLE [dbo].[tbl_RoomTypes]  WITH CHECK ADD  CONSTRAINT [FK_HotelRooms_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_RoomTypes] CHECK CONSTRAINT [FK_HotelRooms_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_Route]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Route_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_Route] CHECK CONSTRAINT [FK_tbl_Route_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_SeatClass]  WITH CHECK ADD  CONSTRAINT [FK_tbl_SeatClass_tbl_Compartment] FOREIGN KEY([Compid])
REFERENCES [dbo].[tbl_Compartment] ([Compid])
GO
ALTER TABLE [dbo].[tbl_SeatClass] CHECK CONSTRAINT [FK_tbl_SeatClass_tbl_Compartment]
GO
ALTER TABLE [dbo].[tbl_Train]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Train_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_Train] CHECK CONSTRAINT [FK_tbl_Train_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_TrainBooking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Booking_tbl_SeatClass] FOREIGN KEY([Sid])
REFERENCES [dbo].[tbl_SeatClass] ([Sid])
GO
ALTER TABLE [dbo].[tbl_TrainBooking] CHECK CONSTRAINT [FK_tbl_Booking_tbl_SeatClass]
GO
ALTER TABLE [dbo].[tbl_TrainBooking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Booking_tbl_TrainRoute] FOREIGN KEY([from_Trid])
REFERENCES [dbo].[tbl_TrainRoute] ([Trid])
GO
ALTER TABLE [dbo].[tbl_TrainBooking] CHECK CONSTRAINT [FK_tbl_Booking_tbl_TrainRoute]
GO
ALTER TABLE [dbo].[tbl_TrainBooking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Booking_tbl_TrainRoute1] FOREIGN KEY([to_Trid])
REFERENCES [dbo].[tbl_TrainRoute] ([Trid])
GO
ALTER TABLE [dbo].[tbl_TrainBooking] CHECK CONSTRAINT [FK_tbl_Booking_tbl_TrainRoute1]
GO
ALTER TABLE [dbo].[tbl_TrainBooking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Booking_tbl_Transaction] FOREIGN KEY([Transid])
REFERENCES [dbo].[tbl_Transaction] ([Transid])
GO
ALTER TABLE [dbo].[tbl_TrainBooking] CHECK CONSTRAINT [FK_tbl_Booking_tbl_Transaction]
GO
ALTER TABLE [dbo].[tbl_TrainBooking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Booking_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_TrainBooking] CHECK CONSTRAINT [FK_tbl_Booking_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_TrainRoute]  WITH CHECK ADD  CONSTRAINT [FK_tbl_TrainRoute_tbl_Route] FOREIGN KEY([Roid])
REFERENCES [dbo].[tbl_Route] ([Roid])
GO
ALTER TABLE [dbo].[tbl_TrainRoute] CHECK CONSTRAINT [FK_tbl_TrainRoute_tbl_Route]
GO
ALTER TABLE [dbo].[tbl_TrainRoute]  WITH CHECK ADD  CONSTRAINT [FK_tbl_TrainRoute_tbl_train] FOREIGN KEY([Tid])
REFERENCES [dbo].[tbl_Train] ([Tid])
GO
ALTER TABLE [dbo].[tbl_TrainRoute] CHECK CONSTRAINT [FK_tbl_TrainRoute_tbl_train]
GO
ALTER TABLE [dbo].[tbl_TrainRoute]  WITH CHECK ADD  CONSTRAINT [FK_tbl_TrainRoute_tbl_UserDetail] FOREIGN KEY([Uid])
REFERENCES [dbo].[tbl_UserDetail] ([Uid])
GO
ALTER TABLE [dbo].[tbl_TrainRoute] CHECK CONSTRAINT [FK_tbl_TrainRoute_tbl_UserDetail]
GO
ALTER TABLE [dbo].[tbl_UserDetail]  WITH CHECK ADD  CONSTRAINT [FK_tbl_UserDetail_tbl_Role] FOREIGN KEY([Rid])
REFERENCES [dbo].[tbl_Role] ([Rid])
GO
ALTER TABLE [dbo].[tbl_UserDetail] CHECK CONSTRAINT [FK_tbl_UserDetail_tbl_Role]
GO
USE [master]
GO
ALTER DATABASE [Db_ClickOnIndia] SET  READ_WRITE 
GO
