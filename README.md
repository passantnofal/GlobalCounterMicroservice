# Counter Project
this application to Increment a counter and get the latest value of the counter.

## Prerequisites
You need to install

-Eventstore from https://eventstore.org/downloads/
-Angular Cli version 6

## Installing
- Clone the project
- Install  project dependencies from Nuget.

## How to run

- Navigate to Eventstore Software folder
- run as administrator EventStore.ClusterNode.exe and EventStore.TestClient.exe
- Build project and Run
- Navigate to http://localhost:55089/swagger/index.html

## Running unit tests

Go to Counter.Repository.Tests Project and then run Unit tests
Go to Counter.Command.Tests Project and then run Unit tests


## Running 
Use Swagger to Call Counter API and use Get and Post Counter methods
Or use Postman to counter API  http://localhost:55089/api/Counter
## Used technologies

This application has been built using the following:

- .Net core 2.1.6
- EventStore
- Xunit
- Nsubstituite
- Swagger

# Architecture

This application has used the DDD Architecture pattern to isolate the domain logic from the other responsibilities of an application, where the main domain entity is the Counter and the data manipulation has been implemented using the CQRS architectural pattern along with the Event sourcing which is used to not to save the current state of the application data but the sequence of Domain Events that led the aggregate into its current state. To load an aggregate it is sufficient to read all the events associated with it and to replay them.
