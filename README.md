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
git clone https://github.com/your-username/WhatsAppNotificationService.git
cd WhatsAppPushNotification
