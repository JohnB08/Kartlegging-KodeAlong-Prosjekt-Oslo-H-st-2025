# Kodealong Prosjekt

Dette er første dag hvor vi faktisk setter oss ned og Koder et prosjekt sammen

Det er nok ganske overvelmende for mange, og det vil det være. Men prosjektet reflekterer godt hvor dere kan være om ca tre måneder. 

I dag gjorde Ask ferdig [Figma designet](https://www.figma.com/design/Cp5I1YYPJkQ2Nly03rYhtc/Planlegging-til-prosjekt?node-id=0-1&t=Mhb3CClewqL1c4ZU-1) sammen med dere i plenum. Og vi begynte på litt back-end arbeid.

Vi satt opp en grov-skisse, og så litt på hvordan et detaljert kart kunne sett ut. Filen som vi lastet ned fra drawio finner dere i repoet. 

Vi snakket også kjapt om at back-enden kom til å bli skrevet i et annet program enn vs-code for å vise dere hvordan disse andre Integrated Developer Environmentsene også ser ut. 

Vi skriver vårt program i Jetbrains Rider.

## Hva skrev vi i dag? (30.09.2025)
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


