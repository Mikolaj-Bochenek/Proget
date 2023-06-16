#!/bin/sh

dotnet ef migrations add SenderInit -c SenderDbContext -p src/Modules/Sender/Sender.Core/Sender.Core.csproj -s src/Bootstraper/Protest.Bootstraper/Protest.Bootstraper.csproj -o Infrastructure/Migrations