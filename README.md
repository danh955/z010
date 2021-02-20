**_Archived_**

# z010 - Gets the stock symbols from NasdaqTrader 

This gets the stock list from the NASDAQ trader website API.  Then writes it out to a SQLite database.
In the process of building this, I wanted to do as much DDD as I can.  I think I got a good structure going here.

### NasdaqTrader refinance:
- [Symbol Lookup](http://www.nasdaqtrader.com/Trader.aspx?id=symbollookup)
- [Symbol Look-Up/Directory Data Fields & Definitions](http://www.nasdaqtrader.com/trader.aspx?id=symboldirdefs)

#### URL to download the symbols:
- http://www.nasdaqtrader.com/dynamic/SymDir/nasdaqlisted.txt
- http://www.nasdaqtrader.com/dynamic/SymDir/otherlisted.txt

### Using

- [Visual Studio v16.8](https://visualstudio.microsoft.com/vs/preview)
- [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

### NuGet

- [CsvHelper](https://www.nuget.org/packages/CsvHelper)
- [MediatR.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/MediatR.Extensions.Microsoft.DependencyInjection)
- [Microsoft.CodeAnalysis.NetAnalyzers](https://www.nuget.org/packages/Microsoft.CodeAnalysis.NetAnalyzers)
- [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)
- [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite)
- [Microsoft.Extensions.Http](https://www.nuget.org/packages/Microsoft.Extensions.Http)
- [Microsoft.NET.Test.Sdk](Microsoft.NET.Test.Sdk) - needed for Visual Studio IDE and xUnit
- [Moq](https://www.nuget.org/packages/Moq)
- [StyleCop.Analyzers](https://www.nuget.org/packages/StyleCop.Analyzers)
- [xUnit](https://www.nuget.org/packages/xunit)
- [xUnit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio)

### Visual Studio Settings

- Tools > Options > Text Editor > C# > Advanced > Place 'System' directives first when sorting using = Checked.

### Visual Studio Extensions

- [CodeMaid](https://marketplace.visualstudio.com/items?itemName=SteveCadwallader.CodeMaid)
- [Visual Studio Spell Checker (VS2017 and Later)](https://marketplace.visualstudio.com/items?itemName=EWoodruff.VisualStudioSpellCheckerVS2017andLater)
