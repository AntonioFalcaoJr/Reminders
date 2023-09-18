# Behavior-driven Reminder

This project offers a unique approach to scheduling and managing tasks through a behavior-driven solution rather than
relying on static and external services like traditional job schedulers.

![](.assets/Reminder.png)

## The Philosophy

Our goal is to provide a flexible and event-driven way to handle reminders and tasks. Instead of depending on external
job schedulers, this system allows you to define reminders with specific behaviors and trigger actions based on their
state changes. The core concepts of this system include:

### Project Structure

#### Domain Layer

The Domain namespace contains the core domain logic for managing reminders. Key classes and concepts in this layer
include:

* **Reminder**: An aggregate root representing a reminder with properties like timer settings, status, and scheduled
  time. It allows defining, marking as **completed**, and marking as **failed**;
* **DomainEvent**: A set of domain events related to reminders, such as **ReminderDefined**, **ReminderCompleted**, and
  **ReminderFailed**.
* **DelayedEvent**: Represents an event triggered when a reminder's scheduled time elapses;
* Value objects and enumerations used within the domain, such as **Timer**, **Address**, and **ReminderStatus**.

#### Application Layer

The Application namespace contains the application layer responsible for handling user interactions and orchestrating
domain actions. Key classes and concepts in this layer include:

* **DefineReminderCommand**: A command to define a new reminder, specifying timer settings and an address.
* **DefineReminderResponse**: A response containing information about the defined reminder, including its ID, status,
  and time left.
* **IDefineReminderInteractor**: An interface for the interactor responsible for defining reminders.
* **CallbackWhenReminderElapsedInteractor**: An interactor for handling events when a reminder's scheduled time elapses.
  It attempts to send a callback to an address and updates the reminder's status accordingly.
* **IApplicationService**: An interface defining application-level services for loading aggregates, appending events,
  and scheduling event publication.

#### Infrastructure Layer

The infrastructure layer is responsible for the technical concerns that support the domain and application layers. It
provides implementations for the interfaces declared in the application layer.

##### Event Bus Gateway

The **EventBusGateway** class provides the specific implementation for the **IEventBusGateway** that is declared in the
application layer. It is responsible for publishing domain events and scheduling delayed domain events. The class
leverages MassTransit, a free, open-source distributed application framework for .NET, to make the message routing more
manageable and decoupled.

* The **SchedulePublishAsync** method schedules events to be published at a specific time.
* The **PublishAsync** method publishes events immediately.

This separation of message routing into an infrastructure service facilitates the testing of application layer
use-cases, as you can replace the actual event bus with a mock implementation for unit testing.

##### Event Store Gateway

Event sourcing persisting strategy is implemented through the **EventStoreGateway** class, that provides an implementation
for the **IEventStoreGateway** contract.

* The **AppendAsync** method appends new events to the event store, which in this case is a DbContext that can represent a
relational database.

* The **GetStreamAsync** method retrieves a stream (sequence) of events for a specific aggregate instance, identified by id,
from the stored event stream.

This persistent storage for the domain events permits replaying events to reconstruct the state of an aggregate root, a
significant advantage for troubleshooting and auditing purposes. But also, the event store acts as a write-side storage
model in the **Command Query Responsibility Segregation** (CQRS) pattern.

## Running

### Staging

Based on a containerized system, the staging environment is provided via Docker Compose. On
the `appsettings.Staging.json` the integrations are configured by name, taking advantage from the Docker
network interface with DNS services.

#### Docker-compose

The resources were split into two files:

- [`docker-compose.Staging.Infrastructure.yaml`](./docker-compose.Staging.Infrastructure.yaml), connection ports are
  privately exposed only;
- [`docker-compose.Staging.Services.yaml`](./docker-compose.Staging.Services.yaml), services connected by DNS names.

```bash
docker-compose \
-f ./docker-compose.Staging.Infrastructure.yaml \
-f ./docker-compose.Staging.Services.yaml \
up -d
```

##### Deployment

Replicas count and resources allocation can be configured straight on respective composes files:

```yaml
deploy:
  replicas: 2
  resources:
    limits:
      cpus: '0.50'
      memory: 200M
```

### Usage

> "address": `http://`**reminder**`:5000/api/v1/reminders`

```http request
###
POST http://reminder:5000/api/v1/reminders
Content-Type: application/json

{
  "hours": 0,
  "minutes": 0,
  "seconds": 30,
  "address": "http://reminder:5000/api/v1/reminders"
}

###  HTTP/1.1 200 OK
###  {
###    "id": "381b213b-5bc9-4efe-ac00-b10227794b7c",
###    "status": "Active",
###    "timeLeft": 28.6739571
###  }

### 
GET http://reminder:5000/api/v1/reminders/381b213b-5bc9-4efe-ac00-b10227794b7c

###  HTTP/1.1 200 OK
###  {
###    "id": "381b213b-5bc9-4efe-ac00-b10227794b7c",
###    "status": "Completed",
###    "timeLeft": 0
###  }
```

### Development

The respective [./docker-compose.Development.Infrastructure.yaml](./docker-compose.Development.Infrastructure.yaml) will
provide all the necessary resources, with public exposure to the connection
ports:

```bash
docker-compose -f ./docker-compose.Development.Infrastructure.yaml up -d
```

### Usage

> "address": `http://`**localhost**`:5000/api/v1/reminders`

```http request
###
POST http://localhost:5000/api/v1/reminders
Content-Type: application/json

{
  "hours": 0,
  "minutes": 0,
  "seconds": 30,
  "address": "http://localhost:5000/api/v1/reminders"
}

###  HTTP/1.1 200 OK
###  {
###    "id": "381b213b-5bc9-4efe-ac00-b10227794b7c",
###    "status": "Active",
###    "timeLeft": 28.6739571
###  }

### 
GET http://localhost:5000/api/v1/reminders/381b213b-5bc9-4efe-ac00-b10227794b7c

###  HTTP/1.1 200 OK
###  {
###    "id": "381b213b-5bc9-4efe-ac00-b10227794b7c",
###    "status": "Completed",
###    "timeLeft": 0
###  }
```

