# Kodealong Prosjekt

Dette er første dag hvor vi faktisk setter oss ned og Koder et prosjekt sammen

Det er nok ganske overvelmende for mange, og det vil det være. Men prosjektet reflekterer godt hvor dere kan være om ca tre måneder. 

I dag gjorde Ask ferdig [Figma designet](https://www.figma.com/design/Cp5I1YYPJkQ2Nly03rYhtc/Planlegging-til-prosjekt?node-id=0-1&t=Mhb3CClewqL1c4ZU-1) sammen med dere i plenum. Og vi begynte på litt back-end arbeid.

Vi satt opp en grov-skisse, og så litt på hvordan et detaljert kart kunne sett ut. Filen som vi lastet ned fra drawio finner dere i repoet. 

Vi snakket også kjapt om at back-enden kom til å bli skrevet i et annet program enn vs-code for å vise dere hvordan disse andre Integrated Developer Environmentsene også ser ut. 

Vi skriver vårt program i Jetbrains Rider.

## Hva skrev vi i går? (30.09.2025)
Vårt ukesprosjekt er å sette opp en "simpel" livechat applikasjon, for å få et lite inblikk i hvordan utvilking fungerer. 

Vi startet å skrive en applikasjon som skal fungere som en server for vår live-chat.
En server er det som leverer (serverer om du vil) informasjon mellom en eller flere klienter. 

Hvis du går inn på nrk.no, så er det en server som leverer den nettsiden til din nettleser, som gjør at du kan se innholdet på siden.
Dette gjelder også html, css og javascript. En server vil også kunne håndtere og styre dataflyt mellom klienter på siden vår. 
I vårt tilfellet laget vi en enkel ChatHub som skal kunne håndtere flyt av meldinger mellom brukere i applikasjonen vår. 
For å matche så godt vi kunne grov-skissen vår, laget vi også noen hjelperepositorier, som holder informasjon om brukere og channels, og hvordan de henger sammen.

Vi ser at meste parten av dataflyt foregår egentlig ved at vi stiller spørsmål med det vi får.
Vi ser at i hver hub blir programmet vårt levert en forespørsel (en request), og vi stiller spørsmål rundt denne requesten for å se at den tilfredsstiller våre krav. 

For eksempel hvis dere går inn i SignalrHubs/ChatHub, og ser på JoinChannel metoden, ser dere at vi stiller følgende spørsmål til requesten vår, før vi behandler den:
 * Eksisterer channelen brukeren prøver å joine?
 * Ekisterer brukeren i systemet vårt?

Hvis vi får ja på begge spørsmålene over, lar vi en bruker joine channelen de ønsker. 

I dag har vi hovedsaklig fokusert på å sette opp det vi trenger for å stille disse spørsmålene om forespørselene våre på en god måte. 
I morgen skal vi se at spørsmålene vi stiller fungerer som forventet, før Ask igjen kommer tilbake for å hjelpe oss med styling av siden vår, slik at vi klarer å matche siden på en god måte. 


## Hva gjorde vi i går? (01.10.2025)
I dag nådde vi minstekravet for back-end delen av prosjektet.

Vi fikset noen bugs vi hadde i går, hvor vi så vi hadde stilt feil spørsmål for å behandle forespørselet vi mottok. 
I noen av spørsmålene vi stilte mot foresprøselen vi mottok, hadde vi sagt ja, der vi mente nei, og vise versa:

I CreateChannel metoden vår på ChatHub hadde vi sagt at hvis brukeren vår eksisterte i vårt user repository, så var det en feil!
```csharp
if (userRepository.Contains(userName)) throw new HubException("User not found");
```

Vi fikset dette ved å si vi var ute etter det motsatte (vi la til et ! for å flippe vår ja/nei til å bety det motsatte).

```csharp
if (!userRepository.Contains(userName)) throw new HubException("User not found");
```

Vi implementerte også en måte for oss å få frem chatte historikken hvis en bruker joiner en allerede eksisterende kanal.

Vi implementerte også en måte å presentere en oppdatert liste av tilgjengelige kanaler.

Siden vi var litt i tidsklemmen og brukte kort tid på planlegging, ser vi at mye av småbugs dukker opp her og der. 
Selv om det tar en stund, er det alltid lurt å sketche ut og planlegge hvordan programmet skal behandle forespørsler på, på en god måte.

Vedlagt ligger også grovsketchen vi laget for å implementere chatte-historikk.

![Grovskissen av vår chattehistorikkmodell skulle vært her](Hvordan%20implementere%20chattehistorikk.excalidraw.png)

Det som gjengstår for dagen er å starte arbeidet med Front-Enden.

## Hva gjorde vi i dag? (02.10.2025)
I dag ferdigstilte vi front-end designet vårt, samtidig som vi oppdagde mange bugs i koden vår.

Vi brukte litt tid å fikse opp, og ordne opp i disse i Javascript koden vår. 
Etter vi hadde en enkel side som vi trodde ville fungere valgte vi å hoste den live, slik at andre kan teste den. 

Vi hostet den på en platform som heter Microsoft Azure. Her brukte vi deres Platform As A Service (PaaS) løsning som heter App Service, for å lage en Web App som kjører og tilgjengeliggjør prosjektet vårt for resten av internettet. 

Etter vi eksponerte siden for mange brukere samtidig, fant vi enda flere bugs! Men sånn er apputvikling!

Siden er live via denne linken:
[Vår Live-chat applikasjon](https://kartlegging-livechat-applikasjon-2025-f5cdguauamf8eqh2.norwayeast-01.azurewebsites.net/)