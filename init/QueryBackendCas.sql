Create Database BackendCas
use BackendCas


CREATE TABLE AdministratorsCa (
    IdAdministrator INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255),
    Email VARCHAR(255),
    Password VARBINARY(32)
);

CREATE TABLE TokenLogs (
    IdTokenLog INT PRIMARY KEY IDENTITY(1,1),
    IdAdministrator INT,
    Token VARCHAR(500),
    RefreshToken VARCHAR(200),
    CreatedAt DATETIME,
    ExpiredAt DATETIME,
	EsActivo AS ( iif(FechaExpiracion < getdate(), convert(bit,0),convert(bit,1))),
    FOREIGN KEY (IdAdministrator) REFERENCES AdministratorsCa(IdAdministrator)
);

CREATE TABLE EventsCa (
    IdEvent INT PRIMARY KEY IDENTITY(1,1),
    IdAdministrator INT,
    EventTitle VARCHAR(255),
    EventDescription TEXT,
    ImageUrl VARCHAR(255),
    Modality VARCHAR(50),
    InstitutionInCharge VARCHAR(255),
    Vacancy INT,
    Address VARCHAR(255),
    Speaker VARCHAR(255),
    EventDateAndTime VARCHAR(255),
    EventDuration INT,
    FOREIGN KEY (IdAdministrator) REFERENCES AdministratorsCa(IdAdministrator)
);

CREATE TABLE Participations (
    IdParticipation INT PRIMARY KEY IDENTITY(1,1),
    IdEvent INT,
    DNI VARCHAR(8),
    Name VARCHAR(255),
    Email VARCHAR(255),
    StudyCenter VARCHAR(255),
    Career VARCHAR(255),
    IEEEMembershipCode VARCHAR(25) NULL,
    HasCertificate BIT,
    HasAttended BIT,
    FOREIGN KEY (IdEvent) REFERENCES EventsCa(IdEvent)
);
