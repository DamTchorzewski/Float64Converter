# 🧮 Float64Converter

**Symulator konwersji liczby dziesiętnej na liczbę zmiennoprzecinkową 64-bitową (IEEE 754 double)**  
Projekt dydaktyczny realizowany w języku **C#** w ramach przedmiotu *Architektura Systemów Komputerowych*.

---


## 👥 Autorzy 
### Zespół Projektowy 11 – Architektura Systemów Komputerowych

 - **Damian Tchorzewski** 72206 – Kierownik zespołu, integracja i dokumentacja.
 - **Mateusz von Schiller** 76203 – Testy jednostkowe, walidacja i weryfikacja poprawności.
 - **Patryk Wichowski** 60379 – BRAK FUNKCJI
 - **Mateusz Wołoszyk** 72557 – Interfejs użytkownika / Parser wejścia i wyjścia.
 - **Cezary Wroniak** 73636 – BRAK FUNKCJI
 - **Joanna Wysokińska** 71598 – Specyfikacja i projekt algorytmu konwersji (IEEE 754 double).
 - **Mateusz Zątek** 72781 – Implementacja silnika konwersji (moduł główny).
 - **Bartłomiej Zygmunt** 74355 – NIE AKTYWNY
		

---

## 🧠 Opis projektu

Aplikacja konsolowa służąca do symulacji procesu konwersji liczby dziesiętnej na format zmiennoprzecinkowy **IEEE 754 (64-bit, double precision)**.  
Program umożliwia:
- wprowadzenie liczby dziesiętnej w różnych formatach (np. `5,75`, `-0,125`, `1e3`),
- konwersję na reprezentację 64-bitową (znak, wykładnik, mantysa),
- prezentację poszczególnych pól bitowych (S / E / M),
- opcjonalny tryb „krok po kroku" (debug),
- zapis wyników konwersji do pliku `output.txt` (katalog: `bin/Debug/net8.0/`).

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

### 3️⃣ Korzystanie z menu aplikacji

Po uruchomieniu zobaczysz interaktywne menu:
```
=== Symulator konwersji liczby dziesiętnej na IEEE 754 (double precision) ===
1 - Konwersja (wynik binarny + szesnastkowy)
2 - Konwersja (tryb KROK PO KROKU + szczegóły)
3 - Wyświetl zawartość pliku output.txt
4 - Wyczyść plik output.txt
5 - Wyjście
6 - Dostępność
```

**Plik wyników:** `bin/Debug/net8.0/output.txt`  
Program automatycznie zapisuje wyniki konwersji z timestampem w tym pliku.

---

🌿 Zasady pracy zespołowej z Git
#### 1️⃣ Utwórz własnego brancha

Każdy członek zespołu pracuje wyłącznie na swoim branchu:
```bash
git checkout -b nazwa-brancha
```
Przykład:
```bash
git checkout -b feature/moja-funkcjonalnosc
```
#### 2️⃣ Wprowadzaj zmiany tylko na swoim branchu

Dodaj lub edytuj pliki w swoim zakresie (np. moduł, testy, dokumentacja).
Po każdej zmianie wykonaj commit:
```bash
git add .
git commit -m "Opis wprowadzonych zmian"
```
#### 3️⃣ Wysyłaj zmiany na GitHub
```bash
git push origin nazwa-brancha
```
#### 4️⃣ Aktualizowanie swojego brancha (opcjonalnie)

Jeśli repozytorium zostało zaktualizowane na main, wykonaj:
```bash
git fetch origin
git merge origin/main
```
lub (bezpieczniej):
```bash
git rebase origin/main
```

---

## 🧩 Aktualna struktura projektu

Projekt implementuje symulator konwersji IEEE 754 (64-bit) z wykorzystaniem niskopoziomowych komponentów elektronicznych.  
Każdy moduł znajduje się w osobnym pliku, zapewniając modularność i czytelność kodu.

```bash
Float64Converter/
│
├── Float64Converter.sln          # Plik rozwiązania Visual Studio
├── Float64Converter.csproj       # Główny projekt C#
├── Program.cs                    # Punkt wejścia aplikacji (CLI) z menu i trybami
├── README.md                     # Dokumentacja projektu
│
├── core/                         # Główna logika aplikacji
│   ├── ConversionEngine.cs       # Silnik konwersji i wyświetlania wyników IEEE 754
│   │
│   ├── accessibility/            # Moduł dostępności
│   │   └── AccessibilityColors.cs # Profile kolorów dla użytkowników z niepełnosprawnościami
│   │
│   ├── components/               # Komponenty elektroniczne
│   │   └── FullAdder.cs         # Pełny sumator binarny (używany w architekturze)
│   │
│   ├── cpu/                      # Komponenty procesora 64-bit
│   │   ├── ALU64bit.cs          # 64-bitowa jednostka arytmetyczno-logiczna
│   │   ├── CPU64bit.cs          # Procesor 64-bitowy dla operacji IEEE 754
│   │   │
│   │   └── registers/            # Rejestry 64-bitowe
│   │       └── Register64bit.cs # Rejestr 64-bitowy (znak, wykładnik, mantysa)
│   │
│   ├── gates/                    # Bramki logiczne
│   │   ├── AndGate.cs           # Bramka AND
│   │   ├── OrGate.cs            # Bramka OR
│   │   └── XorGate.cs           # Bramka XOR
│   │
│   └── utils/                    # Funkcje pomocnicze - wszystkie ręczne implementacje
│       ├── Float64ConverterUtils.cs  # Ręczna konwersja decimal → IEEE 754
│       ├── IEEE754Validator.cs       # Weryfikacja poprawności przez rekonstrukcję
│       ├── MathHelpers.cs            # Ręczne funkcje matematyczne (Abs, Pow2, IsNaN, etc.)
│       ├── NumberParser.cs           # Ręczny parser liczb i notacji naukowej
│       └── TableFormatResult.cs      # Formatowanie tabelaryczne wyników
│
├── bin/                          # Pliki wynikowe kompilacji
│   ├── Debug/net8.0/            # Kompilacja Debug
│   │   ├── Float64Converter.exe # Plik wykonywalny
│   │   └── output.txt           # ⭐ Plik z wynikami konwersji (generowany przez aplikację)
│   └── Release/net8.0/          # Kompilacja Release
│
└── obj/                          # Pliki pośrednie kompilacji
    ├── Debug/net8.0/            # Pliki pośrednie Debug
    └── Release/net8.0/          # Pliki pośrednie Release
```

---

## 🎯 Kluczowe cechy implementacji

- ✅ **Ręczna konwersja IEEE 754** - bez użycia `BitConverter`, `Convert.ToString()`
- ✅ **Brak `System.Math`** - wszystkie funkcje matematyczne zaimplementowane manualnie
- ✅ **Ręczny parser liczb** - parser dziesiętny i notacji naukowej bez `double.TryParse()`
- ✅ **Walidacja przez rekonstrukcję** - weryfikacja poprawności konwersji
- ✅ **Architektura procesorowa** - symulacja CPU 64-bit z rejestrami i ALU
- ✅ **Interfejs konsolowy** - menu z trybem standardowym i krokiem po kroku
- ✅ **Zapis do pliku** - automatyczne logowanie wyników konwersji do `bin/Debug/net8.0/output.txt`
- ✅ **Zarządzanie plikiem** - podgląd i czyszczenie zawartości przez menu
- ✅ **Formatowanie tabelaryczne** - czytelna prezentacja bitów IEEE 754 w formacie tabelarycznym
- ✅ **Dostępność** - 5 profili kolorów dla użytkowników z niepełnosprawnościami (wysoki kontrast, daltonizm, światłowstręt)

---

## 📝 Format pliku wyników

Plik `output.txt` zawiera timestamped entries w formacie:

```
[2025-12-02 14:30:15] TRYB 1
Wejście : 5.75
double  : 5,75
binary  : 0100000000010110000000000000000000000000000000000000000000000000
hex     : 0x4017000000000000
------------------------------------------------------------
```

---

## 📚 Dokumentacja techniczna
Szczegółowy opis algorytmu, struktury danych i wyników testów znajduje się w raporcie:
[📘 Raport projektu (SharePoint)](https://vistulaedu-my.sharepoint.com/:w:/g/personal/dtchorz2_stu_vistula_edu_pl/EcpNXhLCXCpBplqFaHDzejMB16HtcaoQuKZqgOCc8tObKQ?e=B7cmwq)

---

## 📎 Repozytorium projektu
https://github.com/DamTchorzewski/Float64Converter


## 💡 Kontakt
<a href="mailto:dtchorz2@stu.vistula.edu.pl">dtchorz2@stu.vistula.edu.pl</a> - kierownik zespołu

