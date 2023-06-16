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

echo Packing Proget.WebAPI NuGet package...

dotnet pack ./src/Proget.WebAPI/Proget.WebAPI.csproj --configuration Release -p:PackageVersion=$version --output ../../packages

echo Pushing Proget.WebAPI NuGet package to GitHub registry...

dotnet nuget push packages/*$version.nupkg -k $GHCR_PAT -s https://nuget.pkg.github.com/Progmat-Company/index.json
