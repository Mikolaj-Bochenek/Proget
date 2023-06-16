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

echo Packing all Proget NuGet packages...

dotnet pack -c Release -p:PackageVersion=$version --output packages

echo Pushing all Proget NuGet packages to GitHub registry...

dotnet nuget push packages/*$version.nupkg -k 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 -s https://nuget.pkg.github.com/Progmat-Company/index.json --skip-duplicate
