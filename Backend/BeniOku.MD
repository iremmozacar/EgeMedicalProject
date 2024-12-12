1) Solution oluşturma: dotnet new sln
2) Entity katmanı: dotnet new sln -o EgeApp.Backend.Entity
3) Data katmanı: dotnet new classlib -o EgeApp.Backend.Data
4) Business katmanı: dotnet new classlib -o EgeApp.Backend.Business
5) Shared katman: dotnet new classlib -o EgeApp.Backend.Shared
6) API katmanı: dotnet new webapi --use-controllers -o EgeApp.Backend.API

7) Solution'a proje ekleme:
    dotnet sln add EgeApp.Backend.Shared
    dotnet sln add EgeApp.Backend.Entity
    dotnet sln add EgeApp.Backend.Data
    dotnet sln add EgeApp.Backend.Business
    dotnet sln add EgeApp.Backend.API

8) Solution'da olan projeleri listeleme:
    dotnet sln list

MIGRATION İŞLEMLERİ
* dotnet ef migrations add InitialDb -s ../EgeApp.Backend.API
* dotnet ef database update -s ../EgeApp.Backend.API
