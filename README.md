# OOP_EmmerSolution
Uitwerking Usecase en Unit Tests

----

v1.0
verbeterpunten:

		-p refactoring property names to PascalCasing;
		-p refactoring event names to PascalCasing;
			+ hernoem VerbNoun/WerkwoordZelfstandignaamwoord naar Noun/Zelfstandignaamwoord 
				(IsFull-> Full, WillOverflow->Overflows, HasOverflown->Overflown )
		-p Naamgeving Engels/Nederlands;
			simpele refactoring, 3 minuten

		-p 'readonly' constant values naar acces modifier 'constant'
		-d Content publieke property maken; custom-implementatie bestaat uit het limiteren van Content aan [0..Capacity]
			-p Tests implementeren voor Content property (minimum, maximum)
				access modifier aanpassen; 1 minuut

		-pd hernoem TotalCapacity naar Capacity;
			simpele refactoring; 1 minuut
		-d donor- en ontvanger-Emmer mag alleen type Emmer zijn; Niet van de andere container-types
			verplaatsing van methods, tests updaten/verwijderen; 5 minuten
		-d een emmer mag zichzelf vullen
			exception uit code halen, test updaten; 1 minuut
		-p overloading ipv optional parameters
			nieuwe constructors maken en tests implementeren; 10-20 minuten

		-pd naamgeving RegenTon.Size naar Capacity enum;
		-p enum 'regenton_size', used as flag instead of numeric values;
			simpele refacoring, remove constructor switch/case, 3 minuten

		
		-pd CapacityLeft overbodig/private maken
			access modifier aanpassen; 1 minuut
		-p Dubbele code in Fill()/Fill(b)
			+d emmer vullen met emmer, laat de oude emmer ongemoeid (dus Content word niet verplaatst, maar gecopieerd)
				en laat de verantwoordelijkheid van het oude emmer 

		-pd multi-cast event verwijderen
			+d baseer resultaat op laatste event-result
			+d implementeer out-parameter 'CancelEventArgs'
			code verwijderen, nieuwe implementatie maken, test aanpassen; 20 minuten
		
		-p verbetering van unit-tests: Een groot deel doet meerdere tests tegelijk;
			Geen idee hoe op te lossen;
