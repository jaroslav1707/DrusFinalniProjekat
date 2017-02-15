# DrusFinalniProjekat
Nova verzija projekta iz DRUS-a

# DRUS

Uputstvo:

1. Na prvom mestu je potrebno kreirati bazu podataka. Skript fajl za kreiranje baze podataka se nalazi u samom projektu
MeasureService/Model1.edmx.sql. ali i u fajlu baza_podataka.txt.

2. Da bi se pokrenuo program prvo je potrebno pokrenuti HostService projekat.

3. Nakon toga, posto je baza podataka prazna(ne sadrzi vrednosti merenja), potrebno je pokrenuti meraca odnosno 
projekat MeasureClient.
	- u njemu ce biti potrebno upisati ID meraca(vrednost 1,2 ili 3 jer je sa tim vrednostima popunjena baza podataka)
	-naravno u koliko zelite pokrenuti jos meraca potrebno je ponovo pokrenuti MeasureKlijent u uneti ID novog meraca.

4. Sada kada server kupi vrednosti merenja sa meraca i upisuje ih u bazu podataka mozemo pokrenuti klijente posmatrace.
To radimo pokretanjem MonitporClient. Jedan posmatrac moze da se pretplati na vise meraca.
	-prikazuje se meni koji nam omogucava da odaberemo zeljenu opciju
	-ukoliko odaberete opciju prikupljanja podataka sa meraca, klijent posmatrac ce da ispisuje vrednosti koje dobija
	  od meraca. Preporucujem ukoliko zelite koristiti izvestaje da prekinete ovu opciju kako bi izvestaji bili pregledniji.
	
Napomena: potrebno je dobro paziti unosene podatke jer nije svugde koriscena zastita, ukoliko se unose pogresni podaci
(npr datum mora biti u formatu mm/dd/yyyy) program ce se zaustaviti.

