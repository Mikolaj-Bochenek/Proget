#!/bin/sh

while getopts u:p: flag
do
  case "${flag}" in
    u)
      username=${OPTARG};;
    p)
      password=${OPTARG};;
    *)
      echo 'Taka opcja nie istnieje. Użyj -u, aby przekazać username oraz -p, aby przekazać hasło'
      exit 1;;
  esac
done

cat <<EOT > nuget.config
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="github" value="https://nuget.pkg.github.com/Progmat-Company/index.json" />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <github>
      <add key="Username" value="$username" />
      <add key="ClearTextPassword" value="$password" />
    </github>
  </packageSourceCredentials>
</configuration>
EOT