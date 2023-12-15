# Task-Management-System
 Simple task management system allowing users to perform CRUD (Create, Read, Update, Delete) operations on tasks.

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)

## Overview

The front-end is a single-page application built with Blazor WebAssembly,
Back-end is RESTful API is implemented using .NET 8, and the repository pattern is used for data access.
Entity Framework Core 8 is used as the Object-Relational Mapping (ORM) tool to interact with the SQLite database.
Global exception handling is set up to catch and handle exceptions at a centralized level, providing consistent error management.
The API includes a background service that runs daily to check certain task statuses and make necessary changes.
This background service enhances the system's automation capabilities, allowing for periodic tasks without user intervention.

## Features

- Create, Read, Update, Delete tasks
- Pagination
- Request validation
- Automatic deadline checking

## Getting Started

- make sure you've dotnet ef tool installed. To install use : 
'dotnet tool install --global dotnet-ef' command.
- run 'dotnet ef migrations add InitialCreate'
- run 'dotnet ef database update'

Note: Make sure that you're running the command inside Tasks.API folder
You can use Postman to request various enpoints via premade templates:https://api.postman.com/collections/28862725-f5261670-867b-4610-8807-134493beedb6?access_key=PMAT-01HHPYDHW8HY4N6XR5D4XMFBHS

## API Reference
### Work Orders

#### Get all work orders

```https
  GET /api/v1/workorders
```
#### Get work order

```https
  GET /api/v1/workorders/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` |  **Required**|

#### Add work order

```https
  POST /api/v1/workorders
```

| Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `title`      | `string` | **Required**|
| `description`      | `string` | **Required**|
| `phone`      | `string` | **Required**|
| `email`      | `string` | **Required**|
| `startAt`    | `date` | **Required**|
| `finishAt`   | `date` | **Required**|

one of the valid phone number formats would be: "XX-XXX-XXX-XXXX"
as for now we don't support country extensions.
```https
  Example payload {

    "title":"order_1",
    "description":"something",
    "phone":"+59967895",
    "email":"farid.mmzd@gmail.com",
    "startAt": "2023-11-26T12:07:12.557",
    "finishAt": "2023-11-29T12:07:12.557"
  }
```
#### Delete work order

```https
  DELETE /api/v1/workorders/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` |  **Required**|

#### Update work order

```https
  PUT /api/v1/workorders/${id}
```
| Parameter       | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` | **Required**|


| Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `title`      | `string` | **Not Required**|
| `description`      | `string` | **Not Required**|
| `phone`      | `string` | **Not Required**|
| `email`      | `string` | **Not Required**|
| `startAt`    | `date` | **Not Required**|
| `finishAt`   | `date` | **Not Required**|

### Visits

#### Get all visits

```https
  GET /api/v1/visits
```
#### Get visit

```https
  GET /api/v1/visits/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` |  **Required**|

#### Add visit

```https
  POST /api/v1/visits
```

| Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `workOrderId`      | `guid` | **Required**|
| `assigneeFullName`      | `string` | **Required**|
| `assignedFrom`      | `date` | **Required**|
| `parts`      | `[]` | **Required**|

Note : in order to add visit you've to a work order first, and within request you should send at least one part to be created, max 3 parts are allowed for each visit and 5 visits for each work order.

```https
  Example payload {

    "workOrderId":"",
    "assigneeFullName":"",
    "assignedFrom":"2023-11-26T12:07:12.557",
    "parts":[
    {
    "description":"",
     "amount": 100,
     "currency" : "usd",
     "quantity" : 2
    },
    {"description":"",
     "amount": 150,
     "currency" : "usd",
     "quantity" : 2
    },
    {"description":"",
     "amount": 150.56,
     "currency" : "EuR",
     "quantity" : 10
    }]
  }
```

#### Delete visit

```https
  DELETE /api/v1/visits/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` |  **Required**|

#### Update visit

```https
  PUT /api/v1/visits/${id}
```
| Parameter       | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` | **Required**|


| Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `assigneeFullName`      | `string` | **Not Required**|
| `assignedFrom`   | `date` | **Not Required**|

### Parts

#### Get all parts

```https
  GET /api/v1/parts
```
#### Get part

```https
  GET /api/v1/parts/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` |  **Required**|

#### Add part

```https
  POST /api/v1/parts
```

| Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `visitId`      | `guid` | **Required**|
| `amount`      | `decimal(18,2)` | **Required**|
| `description`      | `string` | **Required**|
| `currency`      | `string` | **Required**|
| `quantity`      | `int` | **Required**|

Note : in order to see supported list of currencies by API, you can first ping '/api/v1/currencies'

```https
  Example payload {

    "visitId":"",
    "description":"",
    "amount":"567",
    "currency":"eur",
    "quantity": 10
  }
```

#### Delete visit

```https
  DELETE /api/v1/parts/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` |  **Required**|

#### Update visit

```https
  PUT /api/v1/parts/${id}
```
| Parameter       | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` | **Required**|


| Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `description`      | `string` | **Not Required**|
| `amount`      | `decimal(18,2)` | **Not Required**|
| `currency`      | `string` | **Not Required**|
| `quantity`      | `int` | **Not Required**|
