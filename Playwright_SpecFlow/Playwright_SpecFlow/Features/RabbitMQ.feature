@rabbitmq_test
Feature: RabbitMQ Producer and Consumer Tests

  Scenario: Produce and consume a simple message
    Given a RabbitMQ producer is running
    When the producer send a simple message
    Then the consumer should receive a message with the same content and structure

 Scenario: Produce and consume a complex message
    Given a RabbitMQ producer is running
    When the producer sends a complex message'
    Then the consumer should receive a message with the same content and structure

  Scenario: Produce and consume a list-based message
    Given a RabbitMQ producer is running
    When the producer sends a list message'
    Then the consumer should receive a message with the same content and structure
