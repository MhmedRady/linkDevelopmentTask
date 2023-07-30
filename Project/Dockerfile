FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /source

COPY *.sln .
COPY NewProject/*.csproj ./NewProject/
COPY NewProject.Domain/*.csproj ./NewProject.Domain/
COPY NewProject.EntityFrameworkCore/*.csproj ./NewProject.EntityFrameworkCore/
COPY NewProject.Repositories/*.csproj ./NewProject.Repositories/
COPY NewProject.Application/*.csproj ./NewProject.Application/
COPY NewProject.Shared/*.csproj ./NewProject.Shared/
COPY NewProject.API/*.csproj ./NewProject.API/
COPY CoursesCenter.Web/*.csproj ./CoursesCenter.Web/

RUN dotnet restore

COPY . .

WORKDIR /app/NewProject

RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /app/NewProject .

ENTRYPOINT ["dotnet", "CoursesCenter.Web.dll"]


