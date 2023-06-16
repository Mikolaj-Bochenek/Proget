#!/bin/sh

dotnet ef migrations add RecipientInit -c RecipientDbContext -p src/Services/MicroRecipient/src/MicroRecipient.Core/MicroRecipient.Core.csproj -s src/Services/MicroRecipient/src/MicroRecipient.API/MicroRecipient.API.csproj -o Infrastructure/Migrations