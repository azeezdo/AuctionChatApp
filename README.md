This project contains a .NET microservices auction chat system built with asynchronous communication using RabbitMQ and MassTransit. The solution demonstrates real‑time auction bidding, notifications, invoice generation, and payment processing using isolated services communicating through events.


Overview

The system simulates an online auction platform where users:

Enter auction rooms

Place bids in real-time

Receive live updates

Automatically generate invoices after the auction

Process payments for the winning bidder

This example architecture uses event‑driven microservices to keep services decoupled and scalable.


Architecture Summary

The project contains five independent services, each with its own API and message consumers:

1. RoomService

Manages rooms and starts auctions. Publishes StartAuction events.

2. BiddingService

Handles bid submissions. Maintains highest bid state. Publishes:

HighestBidUpdated

AuctionEnded

3. NotificationService

Consumes bidding updates and auction end events. Publishes GenerateInvoice.

4. InvoiceService

Generates invoices. Publishes InvoiceGenerated.

5. PaymentService

Processes invoice payments. Publishes PaymentProcessed.

6. Messages Project

Shared message contracts used for inter-service communication.

All services communicate only through RabbitMQ events using MassTransit.


Event Flow
RoomService → StartAuction
↓
BiddingService → HighestBidUpdated → NotificationService
↓
AuctionEnded → NotificationService
↓
GenerateInvoice → InvoiceService
↓
InvoiceGenerated → PaymentService
↓
PaymentProcessed

Technologies Used

.NET 8 Web API

RabbitMQ (event broker)

MassTransit (message bus abstraction)

Docker

Sql Server

Limitation
Due to limited time frame and unforeseen circumstances, Some services have not been fully implemented

Improvement
The area to be improve on when there is time 
- complete  all the services
- Improve the security of the application
- Adding caching
