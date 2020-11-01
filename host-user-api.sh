#!/bin/bash
./wait-for.sh mssql:1433 -t 120 -- dotnet ZipCo.Users.WebApi.dll