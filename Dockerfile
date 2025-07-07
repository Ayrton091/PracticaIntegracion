version: '3.8'

services:
  mongo:
    image: mongo:7
    container_name: mongo
    restart: always
    ports:
      - "27017:27017"
    environment:
      MONGO_INITDB_DATABASE: InstitutoDb
    volumes:
      - mongo_data:/data/db

  api:
    build:
      context: .
      dockerfile: Dockerfile
    container_name: api-instituto
    ports:
      - "8080:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - MongoDbSettings__ConnectionString=mongodb://mongo:27017
      - MongoDbSettings__DatabaseName=InstitutoDb
    depends_on:
      - mongo

volumes:
  mongo_data: