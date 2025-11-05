# 🧮 Float64Converter

**Symulator konwersji liczby dziesiętnej na liczbę zmiennoprzecinkową 64-bitową (IEEE 754 double)**  
Projekt dydaktyczny realizowany w języku **C#** w ramach przedmiotu *Architektura Systemów Komputerowych*.

---


## 👥 Autorzy
- **Damian Tchorzewski** – Kierownik zespołu, integracja i dokumentacja  
- **Zespół Projektowy 11** – Architektura Systemów Komputerowych

---

## 🧠 Opis projektu

Aplikacja konsolowa służąca do symulacji procesu konwersji liczby dziesiętnej na format zmiennoprzecinkowy **IEEE 754 (64-bit, double precision)**.  
Program umożliwia:
- wprowadzenie liczby dziesiętnej w różnych formatach (np. `5.75`, `-0.125`, `1e3`),
- konwersję na reprezentację 64-bitową (znak, wykładnik, mantysa),
- prezentację poszczególnych pól bitowych (S / E / M),
- opcjonalny tryb „krok po kroku” (debug),
- zapis wyników konwersji do pliku (w kolejnych etapach projektu).

---

## ⚙️ Środowisko i konfiguracja

- **IDE:** Visual Studio 2022 (Community lub wyższe)  
- **Framework:** .NET 8.0  
- **Język:** C#  
- **System kontroli wersji:** Git + GitHub

### Wymagania
- Zainstalowany .NET SDK 8.0  
  👉 [Pobierz z oficjalnej strony Microsoft](https://dotnet.microsoft.com/en-us/download)
- Konto GitHub oraz skonfigurowane połączenie SSH lub HTTPS

---

## 🚀 Uruchomienie projektu lokalnie

### 1️⃣ Sklonuj repozytorium

```bash
git clone https://github.com/DamTchorzewski/Float64Converter.git
cd Float64Converter
```
### 2️⃣ Otwórz projekt w Visual Studio

W Visual Studio wybierz:
File → Open → Project/Solution → Float64Converter.sln

Następnie uruchom projekt skrótem Ctrl + F5
lub przyciskiem ▶ Start (bez debugowania).

🌿 Zasady pracy zespołowej z Git
1️⃣ Utwórz własnego brancha

Każdy członek zespołu pracuje wyłącznie na swoim branchu:
```bash
git checkout -b nazwa-brancha
```
Przykład:
```bash
git checkout -b nazwa-brancha
```
### 2️⃣ Wprowadzaj zmiany tylko na swoim branchu

Dodaj lub edytuj pliki w swoim zakresie (np. moduł, testy, dokumentacja).
Po każdej zmianie wykonaj commit:
```bash
git add .
git commit -m "Opis wprowadzonych zmian"
```
### 3️⃣ Wysyłaj zmiany na GitHub
```bash
git push origin nazwa-brancha
```
### 4️⃣ Aktualizowanie swojego brancha (opcjonalnie)

Jeśli repozytorium zostało zaktualizowane na main, wykonaj:
```bash
git fetch origin
git merge origin/main
```
lub (bezpieczniej):
```bash
git rebase origin/main
```
## 🧩 Proponowana struktura projektu

Poniższa struktura katalogów zapewnia czytelność i modularność kodu w miarę rozwoju projektu.  
Każdy moduł (np. konwersja, formatowanie bitów, interfejs CLI) znajduje się w osobnym pliku lub folderze.

```bash
Float64Converter/
│
├── Float64Converter.sln # Plik rozwiązania Visual Studio
├── Float64Converter.csproj # Główny projekt C#
├── Program.cs # Punkt wejścia aplikacji (CLI)
│
├── Core/ # Logika konwersji i obliczeń
│ ├── ConversionEngine.cs # Główna klasa konwersji IEEE 754
│ ├── BitFormatter.cs # Formatowanie i analiza bitów (znak, wykładnik, mantysa)
│ ├── BinaryHelper.cs # Pomocnicze funkcje binarne (konwersje, maski, itd.)
│ └── DoubleDecomposer.cs # Rozbicie liczby double na części składowe
│
├── Utils/ # Dodatkowe klasy pomocnicze
│ ├── FileExporter.cs # Zapis wyników konwersji do pliku
│ ├── InputValidator.cs # Walidacja danych wejściowych
│ └── ConsoleUI.cs # Obsługa formatowanego wyjścia w konsoli
│
├── Tests/ # Testy jednostkowe projektu
│ ├── ConversionEngineTests.cs # Testy poprawności konwersji IEEE 754
│ ├── BitFormatterTests.cs # Testy formatowania bitów
│ └── InputValidatorTests.cs # Testy walidacji danych wejściowych
│
├── .gitignore # Ignorowane pliki i katalogi (bin, obj, .vs, itd.)
└── README.md # Dokumentacja pomocnicza projektu
```

## 📚 Dokumentacja techniczna
Szczegółowy opis algorytmu, struktury danych i wyników testów znajduje się w raporcie:
[📘 Raport projektu (SharePoint)](https://vistulaedu-my.sharepoint.com/:w:/g/personal/dtchorz2_stu_vistula_edu_pl/EcpNXhLCXCpBplqFaHDzejMB16HtcaoQuKZqgOCc8tObKQ?e=B7cmwq)

---

## 📎 Repozytorium projektu
https://github.com/DamTchorzewski/Float64Converter


## 💡 Kontakt
mailto:dtchorz2@stu.vistula.edu.pl – kierownik zespołu

