#!/bin/sh

while getopts v: flag
do
  case "${flag}" in
    v)
      version=${OPTARG};;
    *)
      echo 'Taka opcja nie istnieje. Użyj -v, aby przekazać wersję'
      exit 1;;
  esac
done

echo Packing Proget NuGet package...

dotnet pack ./src/Proget.CQRS.Logging/Proget.CQRS.Logging.csproj --configuration Release -p:PackageVersion=$version --output packages
# dotnet pack --configuration Release -p:PackageVersion=1.0.0 --no-restore --output packages

echo Pushing Proget NuGet package to GitHub registry...

dotnet nuget push packages/*$version.nupkg -k $GHCR_PAT -s https://nuget.pkg.github.com/Progmat-Company/index.json
# dotnet nuget push packages/*$version.nupkg --source "github"
