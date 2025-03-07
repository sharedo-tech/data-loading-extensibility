-- create people and ods

begin tran 

declare @qty int = 100
declare @cnt int = 0

while @cnt <= @qty
begin 
    declare @batchPos int = 0
    declare @reference nvarchar(100) = 'ref-' + format(@cnt, '000000')

    insert into source.ods (reference, name, type)
    values (@reference, 'Mr John Smith', 'person')

    insert into source.person (odsReference, title, gender, firstname, surname, middlenameOrInitial, dateOfBirth, dateOfDeath, niNumber,
        employeeCode, primaryTeamId, externalReference, contactHoursFrom, contactHouseTo, preferredContactDetailsReference, personReference)
    values (
        @reference, 'Mr', 'male', 'John-' + cast(@cnt as nvarchar), 'Smith', null, '01 January 1980', '01 January 2024', 'QQ 12 34 56 A',
        'employ-' + cast(@cnt as nvarchar), null, 'extref-' + cast(@cnt as nvarchar), '09:00', '17:00', null, 'peref-' + cast(@cnt as nvarchar))

    insert into source.bankDetails (reference, matchUniqueId, friendlyName, accountHoldersName, bankName, bankSwiftBic, createdDate, isBusinessAccount)
    values
        ('ref-bd-' + cast(@cnt as nvarchar(10)), null, 'Mr John Smith Personal', 'Mr John Smith', 'Banklyfried', 'ABCDUK01999', getutcdate(), 0)

    set @cnt = @cnt+1
end

commit