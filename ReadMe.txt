# README #
This file contains brief information about technology, code and tests taken for implementing oddestodds.com solution.
It also contains the information for setting up the dev environment.

=> What is this repository for? ###

* This repository contains the POC code for the oddesodds.com .
* Version - 1.0


> Presentation

>> Public Website - It will display all Odds posted by Admin at real time to users.

>> BackEnd - Internal users/admin will use to Add/edit/delete odds from website

> Business Logic

>> Rest API to communicate with both Back End and Public Website i.e. provide odds data to them

> Data Model

>> Entity Framework using Code First Approach with database SQL Server

> Messaging

>> Rabbit MQ-  Direct Exchange protocol

> Real Time update

>> SignalR Web Sockets- Broadcast data to attached subscribers as soon as data is published to Queue.


**Assumptions**

*  Basic Implementation of RabbitMQ has been done to satisfy the current need.

*  Basic set up of SignalR has been done to support the broadcasting need.

*   Odds Website Public , Odds Admin and Business logic all are hosted separately and communication between them is done through API's because Admin and Public have different business requirements
  . It is assumed that website and public will have scaling requirement so kept it separately.

* I have hosted SignalR on different server other than public website assuming push notification will have different scaling requirement.



** Technology Stack**

* Public and Web Office Website ( .Net MVC ) 

lighweight, cross platform support makes a good choice for .net application devlopment 

* SignalR Hub ( ASP.NET traditional framework ) 

SignalR hub using Web Sockets protocol is being used for making duplex communication between client and server.

* SQL Server 

It is being used as a data store for now but it can change to NO SQL kind of DB based on scalability of system.


* Messaging 

RabbitMQ to provide scalable pub-sub option. it provides the services for asynchronous communication between SignalR server and Backend website.



 Unit Test Framework 

1. Moq for Mocking 



* Dependency Injection
1. Autofac Container

* ORM

1. Entity Framework with Code First




### How do I get set up? ###

** Install required software**

* Erlang

(http://www.erlang.org/downloads)

* RabbitMQ 

(https://www.rabbitmq.com/download.html)

* Sql Server- SQl Server 2008 and above, express/standard edition*


** Install development frameworks/tools ** 

2. .net 4.5+
3. VS 2015


** What is my code structure**

** Is there any component to set up**

1. RabbitMQ Window Service with default settings along with Erlang(required to run RabbitMQ)

2. Host all different projects separately and it will work as expected and nothing for you to setup additionally as i have done some configuration hardcoaded for your ease purpose.


## Important Notes ##

1. Jquery Data Table grid has been used for front end development considering the given timeline and prior experience.
2. Unit Test cases have been performed on Controller level as of now.
3. Branches have been created Module wise instead of story wise.