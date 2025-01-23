# dockerfiles use # to start a comment

# What will this application require to run?

# To Build...
# sdk/compiler - check!
# source code - check!
# project dependencies - check!
# build/compile the code - check!

# To Run...
# runtime environment (just to run!)
# run the executable/compiled code

###########################################################
# First Stage - Compile the app
# FROM specifies the image to work off of, the base image
# the name of an image has two parts, the name and the tag
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# WORKDIR sets the current working directory - if it exists, you'll just navigate to it. If it doesn't, you'll create the folder, then navigate in.
WORKDIR /app

# COPY from the source to the destination
COPY ./ ./

# RUN to execute a command in the image creation
RUN dotnet restore
RUN dotnet publish -c Release -o ./out
###########################################################
# Secod Stage - run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./ 
# ENTRYPOINT commands are the first/default action when creating the container
ENTRYPOINT ["dotnet", "./Pley.API.dll"]