
create table import.bankDetails (
	reference nvarchar(200) primary key,
    odsReference nvarchar(200) not null,
    dataloadId uniqueidentifier not null,
	isProcessed bit not null default(0),
	result int null,
	processedId uniqueidentifier null,
    matchUniqueId uniqueidentifier null,
    friendlyName nvarchar(250) null,
    accountHoldersName nvarchar(250) null,
    bankName nvarchar(100) null,
    bankSwiftBic nvarchar(100) null,
    createdDate datetime not null,
    isBusinessAccount bit not null
)
create index IX_bankDetails_getReferences on import.bankDetails(dataloadId, result) include (reference)

alter table import.bankDetails add constraint FK_bankDetails_ods_reference foreign key (odsReference) references import.ods(reference)
