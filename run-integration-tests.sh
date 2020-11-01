#!/bin/bash

rm -rf reports && dotnet build && dotnet test --logger xunit --filter "Category=Integration" --results-directory ./reports/ 