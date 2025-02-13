# Update ubuntu 
FROM ubuntu:22.04
RUN apt update 
# Add the .NET sdk 
RUN apt-get install -y dotnet-sdk-7.0
# Copy contents and publish the project 
WORKDIR /app 
COPY . .
# Publish the project 
RUN dotnet publish -c Release -o ./published 
# Move the wroking directory
WORKDIR /app/published 
# Move the database to the current directory 
COPY ./bitcube.db .
# Run the application
CMD ./bitcube --urls=http://0.0.0.0:80