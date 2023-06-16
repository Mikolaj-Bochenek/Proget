#!/bin/sh

dotnet ef migrations add RecipientInit -c RecipientDbContext -p src/Modules/ModularRecipient/ModularRecipient.Core/ModularRecipient.Core.csproj -s src/Bootstraper/Protest.Bootstraper/Protest.Bootstraper.csproj -o Infrastructure/Migrations