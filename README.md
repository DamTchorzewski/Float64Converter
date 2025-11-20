# 🧮 Float64Converter

**Symulator konwersji liczby dziesiętnej na liczbę zmiennoprzecinkową 64-bitową (IEEE 754 double)**  
Projekt dydaktyczny realizowany w języku **C#** w ramach przedmiotu *Architektura Systemów Komputerowych*.

---


## 👥 Autorzy 
### Zespół Projektowy 11 – Architektura Systemów Komputerowych

 - **Damian Tchorzewski** 72206 – Kierownik zespołu, integracja i dokumentacja 
 - **Mateusz von Schiller** 76203
 - **Patryk Wichowski** 60379
 - **Cezary Wroniak** 73636
 - **Joanna Wysokińska** 71598 – Specyfikacja i projekt algorytmu konwersji (IEEE 754 double)
 - **Mateusz Zątek** 72781
 - **Bartłomiej Zygmunt** 74355
		

---

## 🧠 Opis projektu

Aplikacja konsolowa służąca do symulacji procesu konwersji liczby dziesiętnej na format zmiennoprzecinkowy **IEEE 754 (64-bit, double precision)**.  
Program umożliwia:
- wprowadzenie liczby dziesiętnej w różnych formatach (np. `5,75`, `-0,125`, `1e3`),
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
#### 1️⃣ Utwórz własnego brancha

Każdy członek zespołu pracuje wyłącznie na swoim branchu:
```bash
git checkout -b nazwa-brancha
```
Przykład:
```bash
git checkout -b nazwa-brancha
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
## 🧩 Aktualna struktura projektu

Projekt implementuje symulator procesora 64-bitowego z bramkami logicznymi, rejestrami i jednostkami arytmetyczno-logicznymi.  
Każdy moduł (bramki, rejestry, ALU, CPU) znajduje się w osobnym pliku lub folderze zapewniając modularność kodu.

```bash
Float64Converter/
│
├── Float64Converter.sln          # Plik rozwiązania Visual Studio
├── Float64Converter.csproj       # Główny projekt C#
├── Program.cs                    # Punkt wejścia aplikacji (CLI)
├── README.md                     # Dokumentacja projektu
│
├── core/                         # Główna logika aplikacji
│   ├── ConversionEngine.cs       # Silnik konwersji IEEE 754
│   │
│   ├── components/               # Komponenty elektroniczne
│   │   ├── FullAdder.cs         # Pełny sumator binarny
│   │   └── HalfAdder.cs         # Półsumator binarny
│   │
│   ├── cpu/                      # Komponenty procesora
│   │   ├── Adder8bit.cs         # 8-bitowy sumator
│   │   ├── ALU8bit.cs           # 8-bitowa jednostka arytmetyczno-logiczna
│   │   ├── ALU64bit.cs          # 64-bitowa jednostka arytmetyczno-logiczna
│   │   ├── Bus8bit.cs           # 8-bitowa magistrala danych
│   │   ├── Clock.cs             # Generator sygnału zegarowego
│   │   ├── CPU.cs               # Główna klasa procesora 8-bit
│   │   ├── CPU64bit.cs          # Procesor 64-bitowy
│   │   ├── Demux8bit.cs         # 8-bitowy demultiplekser
│   │   ├── Mux8bit.cs           # 8-bitowy multiplekser
│   │   │
│   │   ├── DEMUX/               # Komponenty demultipleksera
│   │   │   └── SendToRegister.cs # Przesyłanie danych do rejestru
│   │   │
│   │   └── registers/            # Rejestry i przerzutniki
│   │       ├── BitRegister.cs   # Rejestr 1-bitowy
│   │       ├── DFlipFlop.cs     # Przerzutnik typu D
│   │       ├── Register8bit.cs  # Rejestr 8-bitowy
│   │       ├── Register64bit.cs # Rejestr 64-bitowy
│   │       └── SRLatch.cs       # Przerzutnik SR
│   │
│   ├── gates/                    # Bramki logiczne
│   │   ├── AndGate.cs           # Bramka AND
│   │   ├── NandGate.cs          # Bramka NAND
│   │   ├── NorGate.cs           # Bramka NOR
│   │   ├── NotGate.cs           # Bramka NOT
│   │   ├── OrGate.cs            # Bramka OR
│   │   └── XorGate.cs           # Bramka XOR
│   │
│   └── utils/                    # Funkcje pomocnicze
│       └── Float64ConverterUtils.cs # Narzędzia konwersji
│
├── tests/                        # Testy jednostkowe projektu
│
├── bin/                          # Pliki wynikowe kompilacji
│   ├── Debug/net8.0/            # Kompilacja Debug
│   └── Release/net8.0/          # Kompilacja Release
│
└── obj/                          # Pliki pośrednie kompilacji
    ├── Debug/net8.0/            # Pliki pośrednie Debug
    └── Release/net8.0/          # Pliki pośrednie Release
```

## 📚 Dokumentacja techniczna
Szczegółowy opis algorytmu, struktury danych i wyników testów znajduje się w raporcie:
[📘 Raport projektu (SharePoint)](https://vistulaedu-my.sharepoint.com/:w:/g/personal/dtchorz2_stu_vistula_edu_pl/EcpNXhLCXCpBplqFaHDzejMB16HtcaoQuKZqgOCc8tObKQ?e=B7cmwq)

---

## 📎 Repozytorium projektu
https://github.com/DamTchorzewski/Float64Converter


## 💡 Kontakt
<a href="mailto:dtchorz2@stu.vistula.edu.pl">dtchorz2@stu.vistula.edu.pl</a> - kierownik zespołu

