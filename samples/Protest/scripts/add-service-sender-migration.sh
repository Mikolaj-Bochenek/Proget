#!/bin/sh

dotnet ef migrations add RecipientInit -c MicroSenderDbContext -p src/Services/MicroSender/src/MicroSender.Core/MicroSender.Core.csproj -s src/Services/MicroSender/src/MicroSender.API/MicroSender.API.csproj -o Infrastructure/Migrations