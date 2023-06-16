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

dotnet pack ./src/Proget.Messaging.Brokers.RabbitMq/Proget.Messaging.Brokers.RabbitMq.csproj --configuration Release -p:PackageVersion=$version --output ../../packages

echo Pushing Proget NuGet package to GitHub registry...

dotnet nuget push packages/*$version.nupkg -k $GHCR_PAT -s https://nuget.pkg.github.com/Progmat-Company/index.json
