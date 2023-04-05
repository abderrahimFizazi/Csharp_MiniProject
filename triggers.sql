-- Trigger pour incrementer le niveau (mysql)

CREATE TRIGGER Update_Niveau_Eleve AFTER INSERT ON moyennes
 FOR EACH ROW BEGIN
	DECLARE moy float;
    DECLARE niv int ;
    select niveau into niv from eleve
    where code =  new.code_eleve;
	set moy = new.moyenne;
    if moy >= 12 THEN
    UPDATE eleve set niveau = niv+1  WHERE code = new.code_eleve;
    end if;
END



-- Trigger pour calculer la moyenne (mysql)



CREATE TRIGGER Update_Moyenne BEFORE INSERT ON notes
 FOR EACH ROW BEGIN
DECLARE count1 int;
DECLARE som float;
declare der_not float;
declare fil varchar(15);
declare niv varchar(50);
declare nbr_mat int;
select code_fil into fil from eleve WHERE
code = new.code_eleve;
select count(*) into nbr_mat from matiere
where code_module in(
    select code_module from module
    where code_fil = fil);
select niveau into niv from eleve WHERE
code = new.code_eleve;
set der_not = new.note;
select SUM(note) into som from notes
where code_eleve = new.code_eleve;
select count(*) into count1 from notes 
where code_eleve = new.code_eleve;
if count1+1=nbr_mat then 
insert into moyennes(code_eleve,code_fil,niveau,moyenne)
values(new.code_eleve,fil,niv,(som+der_not)/(nbr_mat));
end if;
END