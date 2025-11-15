# GIC .NET Take-Home Assignment
**Author:** Satya Parimi  
**Role:** Senior .NET Developer  
**Email:** satya.parimi@example.com  
**GitHub:** [https://github.com/Parimisatya30/myhometest](https://github.com/Parimisatya30/myhometest)  

---

## 📌 Project Overview

This project demonstrates a **microservices-based e-commerce system** built using **.NET Core**, **event-driven architecture**, and **Kafka** for messaging.  
It contains **two main services**:

1. **UserService**  
   - Manages user registration and information  
   - Publishes `UserCreated` events to Kafka  

2. **OrderService**  
   - Manages orders  
   - Publishes `OrderCreated` events to Kafka  
   - Consumes `UserCreated` events to demonstrate cross-service communication  

**Event Bus:** Kafka (Confluent)  
**Database:** In-memory EF Core (for demo purposes)  
**Containerization:** Docker + Docker Compose  

This architecture demonstrates **decoupled services**, **event-driven communication**, and **containerized deployment**.

---

## 🛠️ Prerequisites

- **Docker Desktop** (with Docker Compose)  
- **.NET 8 SDK** (for local runs if desired)  
- **Git** (for cloning the repository)  

---

## 🚀 Running the Application

1. **Clone the repository**

```bash
git clone https://github.com/Parimisatya30/myhometest.git
cd myhometest

2. **Start services with Docker Compose**
docker-compose up --build

3. **Access the services**

UserService API: http://localhost:5001

OrderService API: http://localhost:5002