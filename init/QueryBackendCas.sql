SET
QUOTED_IDENTIFIER ON;
GO

CREATE
DATABASE BackendCas;
GO
USE BackendCas;
GO

CREATE TABLE WebAdministrators
(
    IdAdministrator INT PRIMARY KEY IDENTITY(1,1),
    Name            VARCHAR(255),
    Email           VARCHAR(255),
    Password        VARBINARY(32)
);
GO

CREATE TABLE TokenLogs
(
    IdTokenLog      INT PRIMARY KEY IDENTITY(1,1),
    IdAdministrator INT,
    Token           VARCHAR(500),
    RefreshToken    VARCHAR(200),
    CreatedAt       DATETIME,
    ExpiredAt       DATETIME,
    isActive        BIT,
    FOREIGN KEY (IdAdministrator) REFERENCES WebAdministrators (IdAdministrator)
);
GO

CREATE TRIGGER trg_UpdateIsActive
    ON TokenLogs AFTER INSERT,
UPDATE
    AS
BEGIN
    SET
NOCOUNT ON;
UPDATE TokenLogs
SET isActive = CASE WHEN ExpiredAt < GETDATE() THEN 0 ELSE 1 END
WHERE IdTokenLog IN (SELECT DISTINCT IdTokenLog FROM Inserted);
END;
GO

CREATE TABLE PlatformEvents
(
    IdEvent             INT PRIMARY KEY IDENTITY(1,1),
    IdAdministrator     INT,
    EventTitle          VARCHAR(255),
    EventDescription    TEXT,
    ImageUrl            VARCHAR(255),
    Modality            VARCHAR(50),
    InstitutionInCharge VARCHAR(255),
    Vacancy             INT,
    Address             VARCHAR(255),
    Speaker             VARCHAR(255),
    EventDateAndTime    VARCHAR(255),
    EventDuration       INT,
    FOREIGN KEY (IdAdministrator) REFERENCES WebAdministrators (IdAdministrator)
);
GO

CREATE TABLE Participations
(
    IdParticipation    INT PRIMARY KEY IDENTITY(1,1),
    IdEvent            INT,
    DNI                VARCHAR(8),
    Name               VARCHAR(255),
    Email              VARCHAR(255),
    StudyCenter        VARCHAR(255),
    Career             VARCHAR(255),
    IEEEMembershipCode VARCHAR(25) NULL,
    HasCertificate     BIT DEFAULT 0,
    HasAttended        BIT DEFAULT 0,
    FOREIGN KEY (IdEvent) REFERENCES PlatformEvents (IdEvent)
);
GO
