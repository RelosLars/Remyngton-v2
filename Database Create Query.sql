create table tbl_Tournaments
(
	TournamentName nvarchar(128) primary key,
	StartDate datetime,
	TeamlistLink nvarchar(128)
)

create table tbl_ScoringSystem
(
	ScoringName nvarchar(64) primary key
)

create table tbl_Stage
(
	TournamentName nvarchar(128) foreign key references tbl_Tournaments(TournamentName),
	StageName nvarchar(64),
	ScoringSystem nvarchar(64) foreign key references tbl_ScoringSystem(ScoringName)
)


create table tbl_TournamentParticipants
(
	TournamentName nvarchar(128) foreign key references tbl_Tournaments(TournamentName),
	UserID nvarchar(16),
)