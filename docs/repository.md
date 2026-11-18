# Repozytorium: opis i tagi

Rzeczy, które ustawia się na GitHubie ręcznie (nie da się ich zapisać w plikach repozytorium). Przegląd projektu: [README](../README.md).

## Opis i tagi (About)

GitHub: strona repozytorium → ⚙ obok „About” → pola poniżej. Opis ma limit 350 znaków, tagi (topics) pisze się małymi literami z myślnikami, najwyżej 20.

**Opis (Description):**

```text
Console simulator that converts a decimal number to 64-bit IEEE 754 (double precision) in C#. Hand-written number parser, logic gates, full adder, ALU and a 64-bit CPU model, step-by-step mode, accessibility colour profiles and xUnit tests. University team project (Computer Systems Architecture).
```

**Strona (Website):** puste (aplikacja konsolowa, nie ma strony).

**Tagi (Topics):**

```text
csharp dotnet dotnet-8 ieee-754 floating-point double-precision binary-converter console-application computer-architecture logic-gates alu xunit github-actions university-project
```

Z GitHub CLI (`gh`) to samo jedną komendą:

```bash
gh repo edit DamTchorzewski/Float64Converter --description "Console simulator that converts a decimal number to 64-bit IEEE 754 (double precision) in C#. Hand-written number parser, logic gates, full adder, ALU and a 64-bit CPU model, step-by-step mode, accessibility colour profiles and xUnit tests. University team project (Computer Systems Architecture)." --add-topic csharp,dotnet,dotnet-8,ieee-754,floating-point,double-precision,binary-converter,console-application,computer-architecture,logic-gates,alu,xunit,github-actions,university-project
```

## Raport projektu

Szczegółowy opis algorytmu, struktur danych i wyników testów jest w raporcie zespołu na SharePoint uczelni (dostęp zależy od konta uczelnianego): [raport](https://vistulaedu-my.sharepoint.com/:w:/g/personal/dtchorz2_stu_vistula_edu_pl/EcpNXhLCXCpBplqFaHDzejMB16HtcaoQuKZqgOCc8tObKQ?e=B7cmwq).

## Ustawienia do włączenia

| Gdzie (GitHub)                                          | Co ustawić                                                                  |
| ------------------------------------------------------- | --------------------------------------------------------------------------- |
| Settings → General → Default branch                     | `main` (już jest)                                                           |
| Settings → Branches (albo Rulesets) → `main`, `develop` | zablokuj force push i usuwanie gałęzi                                       |
| `.github/workflows/dotnet-ci.yml`                       | uruchamia się na `main` i `codex/*`; żeby działał też na `develop`, dopisz tę gałąź do `on.push.branches` |
