# Counter Project
This application Implement the idea of having a global counter that get incremented across all users requests

## Prerequisites
You need to install

- .Net core 2.1.6
- Eventstore from https://eventstore.org/downloads/
- Download latest update of Visual Studio


## Installing
- Clone the project

## How to run

- Navigate to Eventstore Software folder
- Run these executable files EventStore.ClusterNode.exe and EventStore.TestClient.exe as administrator
- Go back to project folder and open Counter_kfzteile24_Task.sln file
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

## Architecture

This application has used the DDD Architecture pattern to isolate the domain logic from the other responsibilities of an application, where the main domain entity is the Counter and the data manipulation has been implemented using the CQRS architectural pattern along with the Event sourcing which is used to not to save the current state of the application data but the sequence of Domain Events that led the aggregate into its current state. To load an aggregate it is sufficient to read all the events associated with it and to replay them.

## Deployment
- Download Eventstore software from https://eventstore.org/downloads/
- Download AlwaysUp software from https://www.coretechnologies.com/products/AlwaysUp/AlwaysUp_Installer.exe
- Double click on AlwaysUp_Installer.exe and go with the wizard
- After AlwaysUp installation finish open it
- Click Application > Add (Add Application window will appears)
- Select General tab
- In Name property add "Event store" 
- In Application Property browse to EventStore software folder and select "EventStore.ClusterNode.exe"
- Select Logon tab and add the authorized user and password
- Select Startup tab and check the check box of "Ensure that the Windows Networking components have started". This tells AlwaysUp that Event Store needs the TCP/IP stack to function.
- Click save
- Click Application Start "Event Store"
- Open OS new Command Line Prompt
- cd to the project path
- run: dotnet publish -o ./publish Counter_kfzteile24_Task
- run: cd Counter_kfzteile24_Task\publish
- run: dotnet counter.api.dll
