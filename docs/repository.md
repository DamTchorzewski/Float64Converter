# Repozytorium: opis i tagi

Rzeczy, które ustawia się na GitHubie ręcznie (nie da się ich zapisać w plikach repozytorium). Przegląd projektu: [README](../README.md).

## Nazwa repozytorium: `ieee754-conversion-simulator`

Standard nazw repozytoriów: `reops/standards/repo-names.md`. Repozytorium nazywało się wcześniej `Float64Converter`; od 2026-11-20 nazywa się `ieee754-conversion-simulator` (nazwa mówi, czym jest projekt: fraza szukana na GitHubie). GitHub przekierowuje stary adres repozytorium. Projekt, rozwiązanie i przestrzeń nazw C# nadal nazywają się `Float64Converter` (zmiana nazwy repozytorium nie dotyka kodu).

## Opis i tagi (About)

GitHub: strona repozytorium → ⚙ obok „About” → pola poniżej. Opis ma limit 350 znaków, tagi (topics) pisze się małymi literami z myślnikami, najwyżej 20.

**Opis (Description):**

```text
Console simulator of IEEE 754 double-precision conversion in C#, with step-by-step output.
```

**Strona (Website):** puste (aplikacja konsolowa, nie ma strony).

**Tagi (Topics):**

```text
csharp dotnet dotnet-8 ieee-754 floating-point double-precision binary-converter console-application computer-architecture logic-gates alu xunit github-actions
```

Z GitHub CLI (`gh`) to samo jedną komendą:

```bash
gh repo edit DamTchorzewski/ieee754-conversion-simulator --description "Console simulator of IEEE 754 double-precision conversion in C#, with step-by-step output." --add-topic csharp --add-topic dotnet --add-topic dotnet-8 --add-topic ieee-754 --add-topic floating-point --add-topic double-precision --add-topic binary-converter --add-topic console-application --add-topic computer-architecture --add-topic logic-gates --add-topic alu --add-topic xunit --add-topic github-actions --add-topic
```

## Raport projektu

Szczegółowy opis algorytmu, struktur danych i wyników testów jest w raporcie zespołu na SharePoint uczelni (dostęp zależy od konta uczelnianego): [raport](https://vistulaedu-my.sharepoint.com/:w:/g/personal/dtchorz2_stu_vistula_edu_pl/EcpNXhLCXCpBplqFaHDzejMB16HtcaoQuKZqgOCc8tObKQ?e=B7cmwq).

## Ustawienia do włączenia

| Gdzie (GitHub) | Co ustawić |
| ------------------------------------------------------- | --------------------------------------------------------------------------- |
| Settings → General → Default branch | `main` (już jest) |
| Settings → Branches (albo Rulesets) → `main`, `develop` | zablokuj force push i usuwanie gałęzi |
| `.github/workflows/dotnet-ci.yml` | uruchamia się na `main` i `codex/*`; żeby działał też na `develop`, dopisz tę gałąź do `on.push.branches` |
