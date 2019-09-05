# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS builder
WORKDIR /source

# caches restore result by copying csproj file separately
#COPY /NuGet.config /source/
COPY /DeveVipAccess/*.csproj /source/DeveVipAccess/
COPY /DeveVipAccess.ConsoleApp/*.csproj /source/DeveVipAccess.ConsoleApp/
COPY /DeveVipAccess.Tests/*.csproj /source/DeveVipAccess.Tests/
COPY /DeveVipAccess.sln /source/
RUN ls
RUN dotnet restore

# copies the rest of your code
COPY . .
RUN dotnet build --configuration Release
RUN dotnet test --configuration Release ./DeveVipAccess.Tests/DeveVipAccess.Tests.csproj
RUN dotnet publish ./DeveVipAccess.ConsoleApp/DeveVipAccess.ConsoleApp.csproj --output /app/ --configuration Release

# Stage 2
FROM mcr.microsoft.com/dotnet/core/runtime:2.2-alpine3.9
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "DeveVipAccess.ConsoleApp.dll"]