# ZipCo User API Dev Guide

### Tech stack
This API is built with major techs as below:
1. ASP.NET CORE
2. EF Core + MSSQL
2. Mediatr
3. AutoMapper
4. FlunentValidation
5. XUnit + Shouldly
6. Docker

### Building and Deploying
To build docker on the local machine, please run the command below
```Bash
cd <project root> 
build-docker.sh
```
Once docker image build on local successfully, to host the app on local, please run
```Bash
serve-dev.sh
```
The serve-dev.sh will start hosting the MSSQL database and apply EF core data migration to the DB. Then the User API will be hosted as kestrel mode on local.

When you see `updatedb_1  | Database has been migrated` from the terminal, then the swagger ui for api will be available from http://localhost:8000/swagger/index.html 
```Bash
2020-11-01 07:46:43.47 spid51      Parallel redo is started for database 'PayCo.User' with worker pool size [1].
2020-11-01 07:46:43.49 spid51      Parallel redo is shutdown for database 'PayCo.User' with worker pool size [1].
updatedb_1  | Database has been migrated
```


### Testing

For unit tests, they can be executed anytime via
```Bash
run-unit-tests.sh
```
For integration tests, all the tests will run against the SQL database. Therefore I Skip all the integration test on Hank Rank to avoid the test failures. To Enable the tests, please go to project/ZipCo.Users.Test.Integration to find two test files AccountIntegrationTests.cs and MemberIntegrationTests.cs. then remove the skip.

``` csharp
        [Fact(Skip = "Hack Rank Not Supported")]         <=========== make sure this line comment out
        [Trait("Category", "Integration")]
        //[Fact]           <============ enable this Fact without skip
        public async Task GivenAccountController_WhenSignUpAccount_ShouldReturnAccount()
        {
            ....
        }
```
After all, skips are removed, we need to start the local dev environment via `serve-dev.sh`, then run the script below to trigger integration tests
```Bash
run-integration-tests.sh
```

### Additional Information
#### Solution Architecture ####
The whole User API design is following with Onion Architecture and CQRS pattern. The solution has been dived by four different areas:
![alt text](https://www.thinktocode.com/wp-content/uploads/2018/08/OnionArrow.png "") 
**referenced from https://www.thinktocode.com/2018/08/16/onion-architecture/**
1. **Domain Layer** : 
This is the layer to represents the business behaviour objects and domain logic. Other layers can reference the domain layer but the domain layer should not reference any other layers. This can make the domain layer independent and isolated.
2. **Infrastructure Layer**
All the infrastructures logic will be facilitated in this layer. It contains repositories for database, third party API service called, logging and so on. 
3. **Application Layer**: 
This layer acts as the integrator among different layers. It will handle the request passed from the presentation layer and lookup the functions from other layers to complete the job. Then it will construct the response and send back to the presentation layer. Similar to the Domain layer, application layer should run and separated from the infrastructure and presentation. 
4. **Presentation Layer**
As the presentation layer, it decides what kind of way to consume the application. For this project, we have two apps in the presentation layer - User API and the DBUpdater

We use Mediatr to apply CQRS pattern between the presentation layer so that it can bring advantages on
1. Keep the single responsibility for each request.
2. Enable event-driven architecture in the app.
3. The mediator can isolate the application layer from the presentation layer so that the application layer will be reusable.

#### Data Model ####
![alt text](https://app.lucidchart.com/publicSegments/view/fcaaedc7-06d4-4dab-8c26-5fe85263dad7/image.png "Logo Title Text 1")

#### Error Handling ####
We use BussinesException call to carry error with error type. BusinessException is a class file located in the Domain layer. It defines the exception with different error types which we have `BadRequest, ResouceNotFound and Critical`

When the exception is caught from the GlobalExceptionHandler(custom middleware from asp.net core ), the error type will be translated to the associated Http status code and response the error back with it.

If you need more calrification from the app, please call 0430106177 or email iamzhaoxu@gmail.com


