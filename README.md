 ## URUCHOMIENIE

Sklonuj repozytorium i otwórz konsolę w  folderze, następnie wykonaj:

`cd APBD-Cw1-s31191/`

`dotnet run`

## STRUKTURA

Projekt opiera się na podziale obowiązków z wykorzystaniem warstw Repository i Service. Taki podział plików i klas został wybrany, aby ułatwić rozwój i utrzymanie kodu poprzez oddzielenie logiki i zachowania pojedynczej odpowiedzialności dla każdej klasy.

* **Kohezja:** Każda klasa ma z góry zdefiniowany cel. Repozytoria odpowiadają wyłącznie za operacje na danych, natomiast serwisy skupia logikę biznesową.

* **Coupling:** Aby zredukować coupling, w projekcie wykorzystano mechanizm Dependency Injection. Poszczególne serwisy i repozytoria są przekazywane przez konstruktory. Klasy nie tworzą zależności samemu.
