# Clean Architecture in Microservice with handling enterprise roles with DDD
Microservice + DDD + CQRS + MongoDB + FluentValidation + AutoMapper + MassTransit for event handling with RabbitMQ

When you want to handling bussines roles, DDD is one of best choice for starting your programming. When you search in the internet you find a lots of source codes and the owners that tell you this source code is so clean and you can download it and use it as template in your project. But maybe it's not true. Actually you should try to find a best architecture for your project and ofcource you can get help from another source codes that published in the GIT or something like that.

In my opinion the fact that's really important about clean architcure in your project is you should check out a lots of sourcecodes and try to find specific solution as a clean  architect template for your project.

When you want to create a microservice at first you should check that has a complex bussines roles or not. If you think has it, you should select DDD as a base of architcure and in Domain-driven design as you know, the start point of your project is Domain layer(core layer).

Before you start you should find all events of this domain and negotiate with domain expert about it. For example OrderRecieved or UserRegistered. And after that you should design the workflow for eath event. In some situation for creating workflow you need to add something in database or send event to another microserive or need another thing, the best choice for this part is Interface. Yes you can just create interfaces that you needed and after your domain layer completed other layer should implement these interfaces that domain use it in it's workflow.
Also validation is one of important step in Domain layer and before you created any object from entities class everything should be validate.
In domain we don't access to create object from entity in unvalidate state.
The things that change the state of entity and we should validate it is just in command part of CQRS. In this project I handled commands in domain layer and in query part of CQRS I sent any query directly from application layer to repository (part of infrastructure layer)
I used fluent validation for validate commands in Domain layer and also used AutoMapper for mapped viewmode in to entity and vice versa. 
MongoDB is my DB in this project and I used MongoDB.Entities for handling every communication with Mongo in repository.
As you know the some times you need to send event to another microservice and RabbitMQ is best choice for it and I used MassTransit for get event from Rabbit and send or publish new event to Rabbit for the use of other microservices.
