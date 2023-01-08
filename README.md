# Einführung 
![GitHub all releases](https://img.shields.io/github/downloads/biegomar/fenrir13/total)  
Hallo liebe Textadventure-Fans,
ich freue mich, Euch das Spiel Fenrir 13 zu präsentieren. Hierbei handelt es sich um eine Science Fiction Story rund um den Raumfrachter Fenrir 13 auf seiner Mission zu den entlegenen Monden von Proxima Centauri b.

# Spiel verfügbar machen
Das Spiel ist als Consolen-Spiel konzipiert und läuft auf Windows, Linux und MacOS in deren jeweiligen Consolen-Hosts. Dort nutzt es die Einstellungen, die Ihr aktuell verwendet. Am besten spielt sich Fenrir 13 mit einem schwarzen oder grauen Hintergrund.
Achtet darauf, die für Euer System richtige Version des Spiels zu benutzen!

Achtung! Aufgrund der Rechteeinstellungen wird vermutlich beim Start des Spiels darauf hingewiesen, dass es sich um eine Datei aus einer unbekannten Quelle handelt. Daher müsst Ihr die Ausführung explizit erlauben (Windows). Unter Linux und MacOS müsst Ihr die Ausführungsrechte explizit setzen (z.B. über chmod).
Gibt es dabei Probleme, könnt Ihr mich gerne kontaktieren.

Folgende Versionen stehen zum Download bereit:
* linux-arm -> arm-basierte System, z.B. Raspberry-Pi. (ungetestet)  
* linux-arm64 -> arm-basierte Systeme. Hier konkret Raspberry-Pi 4 auf Ubuntu 20.10. (getestet)  
* linux-x64 -> x64-basierte Systeme. Standard-Rechner unter Linux. (getestet) 
* osx-x64 -> Apple macOS-basierte Systeme. (getestet)
* win-x64 -> x64-basierte Windows-Systeme. Standard-Rechner unter Windows. (getestet)
* win-x86 -> x86-basierte (32 Bit) Windows-Systeme. Standard-Rechner unter Windows. (getestet)

# Spiel starten
Der einfachste Weg, das Spiel zu starten ist, die Datei per Doppelklick aufzurufen. Aber auch der Start über eine bereits geöffnete Console ist möglich. Dazu wechselt Ihr in den Ordner, in dem Ihr Fenrir 13 abgelegt habt und tippt "Fenrir13.exe" (Windows), "./Fenrir13.exe" (Windows Powershell), bzw "./Fenrir13" (Linux und MacOS) ein. 

# Spielstand speichern und laden
Innerhalb des Spiels braucht Ihr nur das Kommando SAVE eingeben und der Spielstand wird in Eurem Dokumentenordner gespeichert. Der korrekte Pfad und der Name der Datei wird Euch nach der Aktion angezeigt. Die Angabe eines eigenen Pfades oder Dateinamens ist aktuell noch nicht vorgesehen. Daher wird bei jedem Speicher die eine Datei immer überschrieben. Wollt Ihr Euch explizit Spielstände aufbewahren, so müsst Ihr selbständig die Datei kopieren und umbenennen. 
Im Moment gibt es noch keine Möglichkeit, den Spielstand direkt aus Fenrir 13 zu laden. Wollt Ihr aber bei einem gespeicherten Spielstand weiter spielen, so müsst Ihr das Spiel direkt von der Console aus starten und den Paramter F benutzen. Das sieht dann wie folgt aus:
Windows: 
./Fenrir13.exe -F "C:\Users\marcb\Documents\heretic_savedgame.txt"
Linux und MacOS:
./Fenrir13 -F "SavedGames/heretic_savedgame.txt"

Bitte denkt daran, dass die Pfade natürlich Euren lokalen Pfaden entsprechen müssen.
Auch hier gilt: gibt es Probleme, könnt Ihr mich gerne kontaktieren.

# Wie geht es weiter?
Sobald der diesjährige Wettbewerb gestartet ist, werde ich das Spiel auch offiziell veröffentlichen. Die Quelltexte werden dann auch auf GitHub verfügbar sein.
Als der Wettbewerb angekündigt wurde, befand sich gerade ein anderes Spiel von mir in der Entwicklung. Heretic. Das ist aber viel größer angelegt als die rund 90 Minuten Spielzeit des Wettbewerbs. Daher habe ich mich kurzfristig entschieden, Fenrir 13 ins Leben zu rufen. Um nicht alles erneut zu programmieren, habe ich mich weiterhin entschieden aus Heretic allgemeine Teile in ein eigenes Framework zu überführen und verfügbar zu machen. Auch dieses Framework (Heretic.InteractiveFiction) wird sehr zeitnah auf GitHub veröffentlicht und für Programmierer als NuGet-Paket verfügbar gemacht.
Es ist durchaus denkbar, dass Fenrir 13 noch technische oder leichte inhaltliche Anpassungen erfährt, vor allem dann, wenn noch echte Fehler entdeckt werden. Aber zumindest die Möglichkeit, aus dem Spiel heraus Spielstände zu laden soll noch ins Framework integriert und dann auch in Fenrir 13 möglich gemacht werden. 

# Kontakt
Falls Fragen sind, könnt Ihr mich gerne kontaktieren: 
* Email: interactive.fiction@online.ms
* twitter: @biegomar
* GitHub: github.com/biegomar


Viele Grüße  
Marc Biegota
