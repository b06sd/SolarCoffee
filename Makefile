# Project Variables
PROJECT_NAME ?= SolarCoffee
ORG_NAME ?= SolarCoffee
REPO_NAME ?= SolarCoffee

.PHONY: migrations db
migrations:
		cd ./SolarCoffee.Data && dotnet ef --startup-Project ../SolarCoffee.Web/ migrations add $(mname) && cd ..
db:
		cd ./SolarCoffee.Data && dotnet ef --startup-Project ../SolarCoffee.Web/ database update && cd ..		