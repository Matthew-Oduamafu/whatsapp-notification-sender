# WhatsApp Push Notification

This project provides a simple service for pushing WhatsApp notifications using Twilio, RabbitMQ, and OpenTelemetry with a C# ASP.NET Core backend.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [Running the Project](#running-the-project)
- [Endpoints](#endpoints)
- [Observability](#observability)
- [Future Enhancements](#future-enhancements)
- [License](#license)

## Overview

The WhatsApp Notification Service is composed of two main components:

1. **API Service**: A REST API built with ASP.NET Core that receives requests to send WhatsApp messages and queues them in RabbitMQ.
2. **Worker Service**: A background service that listens to the RabbitMQ queue, picks up messages, and sends them to users via Twilio's WhatsApp API.

## Architecture

The architecture consists of the following components:

- **ASP.NET Core Web API**: Exposes an endpoint to receive message requests and enqueue them in RabbitMQ.
- **RabbitMQ**: Acts as a message broker to queue and manage messages.
- **Worker Service**: Consumes messages from the RabbitMQ queue and sends them to WhatsApp users via Twilio.
- **OpenTelemetry**: Provides observability for tracing requests and monitoring system behavior.

![Architecture Diagram](https://via.placeholder.com/800x400.png?text=Architecture+Diagram)

## Technologies Used

- **C# .NET 8**: For building the API and worker services.
- **ASP.NET Core**: For creating the Web API.
- **RabbitMQ**: For message queuing and broker services.
- **Twilio API**: For sending WhatsApp messages.
- **OpenTelemetry**: For distributed tracing and observability.
- **Docker**: For containerizing the services.

## Setup and Installation

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [RabbitMQ](https://www.rabbitmq.com/download.html)
- [Docker](https://www.docker.com/get-started) (optional)
- [Twilio Account](https://www.twilio.com/try-twilio)

### Clone the Repository

```bash
git clone https://github.com/Matthew-Oduamafu/whatsapp-notification-sender.git
cd WhatsAppPushNotification
```

### Configure Environment Variables
Update the `appsettings.json` file in both the API and Worker projects with the following configurations:
```json
{
  "TwilioConfig": {
    "AccountSid": "ACXXXXXXXX", // Twilio Account SID
    "AuthToken": "your_auth_token",  // Twilio Auth Token
    "PhoneNumber": "your_twilio_phone_number" // Twilio Phone Number
  },
  "RabbitMqConfig": {
    "Host": "localhost",
    "QueueName": "WhatsappNotificationQueue"
  }
}
```

### Install Required Packages
Run the following commands in both the `WhatsAppNotificationApi` and `WhatsAppNotificationWorker` directories:
```bash
dotnet add package RabbitMQ.Client
dotnet add package Twilio
dotnet add package OpenTelemetry.Extensions.Hosting
dotnet add package OpenTelemetry.Instrumentation.AspNetCore
dotnet add package OpenTelemetry.Exporter.Console
```

### Run RabbitMQ Locally (Docker)
Set up a RabbitMQ container using Docker from the docker-compose file in the root directory:
```bash
docker-compose up -d
```

## Running the Project
1. Run the API Project

   Navigate to the WhatsAppNotificationApi directory and run:
```bash
dotnet run
```
2. Run the Worker Service

   Navigate to the WhatsAppNotificationWorker directory and run:

```bash
dotnet run
```
