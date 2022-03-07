# FanFicFabliaux
## Kratki opis
Sustav za pisanje i čitanje djela iz zajednice

Prijedlog za projekt jest web aplikacija za pisanje knjiga te pregled i čitanje. 
Korištene tehnologije bi uključivale **DOT.NET CORE** sa **SQL server** bazom podataka. 
Za pisanje PDF-ova bi se koristio **DinkToPdf**, a za čitanje otvaranje PDF-a kroz Google-ovu ekstenziju. 
Testiranje bi se provodilo unit testovima pomoću **Moq** biblioteke te **Selenium**-om za integracijske testove. 
Dokumentacija će se održavati **Doxygen**-om.

Funkcionalnosti:
-	Registraciju korisnika
-	Prijavu već registriranog korisnika
-	Izbornik sa zabranama ovisno o autentifikaciji
-	Pisanje knjige unutar aplikacije
-	Podizanje već napisane knjige s vlastitog računala
-	Pregled već napisanih knjiga po žanrovima i oznakama
-	Pretraga knjiga sa servera po naslovu ili autoru
-	Čitanje pojedinačnih knjiga
-	Preuzimanje knjiga na vlastito računalo
-	Označavanje  da se korisniku knjiga sviđa u svrhu rangiranja
-	Mogućnost promjene korisničkih podataka


## Preporučena tehnička potpora
- [Visual Studio Comumnity 2022](https://visualstudio.microsoft.com/downloads/) (ili 2019) sa .Net-om
- GitHub Extension for Visual Studio
- Code Maid ekstenzija

## Korišteni dependency
- AspNetCore 3.1 (Uključujući Identity)
- EntityFrameworkCore SqlServer
- DinkToPdf
- Moq

## Korisni linkovi
- [Create PDF tutorial](https://www.infoworld.com/article/3605276/how-to-create-pdf-documents-in-aspnet-core-5.html)
- [Read PDF tutorial](https://stackoverflow.com/questions/60444003/how-can-i-create-and-display-a-pdf-file-for-a-net-core-mvc-web-application)
