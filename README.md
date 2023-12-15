# Task-Management-System
 Simple task management system allowing users to perform CRUD (Create, Read, Update, Delete) operations on tasks.

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)

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

## API Documentation
#### Get all tasks

```https
  GET /api/v1/tasks
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `pageNumber`      | `int` |  **Non-Required**|
| `pageSize`      | `int` |  **Non-Required**|

#### Get task

```https
  GET /api/v1/tasks/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` |  **Required**|

#### Add task

```https
  POST /api/v1/tasks
```

| Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `title`      | `string` | **Required**|
| `description`      | `string` | **Required**|
| `dueDate`    | `date` | **Required**|

```https
  Example payload {

    "title":"task_1",
    "description":"description_1",
    "dueDate": "2023-12-30T12:07:12.557"
  }
```
#### Delete task

```https
  DELETE /api/v1/tasks/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` |  **Required**|

#### Update task

```https
  PUT /api/v1/tasks/${id}
```
| Parameter       | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `guid` | **Required**|


| Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `title`      | `string` | **Not Required**|
| `description`      | `string` | **Not Required**|
| `status`   | `string` | **Not Required**|
