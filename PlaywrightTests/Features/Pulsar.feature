@Pulsar_test 
Feature: Pulsar Producer and Consumer Tests

  Scenario: Produce and consume a simple message
    Given a Pulsar producer is running
    When I send a message with content '{"type": "simple", "content": "Hello, World"}'
    Then I should receive a message with the same content and structure

  Scenario: Produce and consume a complex message
    Given a Pulsar producer is running
    When I send a message with content '{"type": "complex", "content": {"text": "Hello", "number": 123}}'
    Then I should receive a message with the same content and structure

  Scenario: Produce and consume a list-based message
    Given a Pulsar producer is running
    When I send a message with content '{"type": "list", "content": ["item1", "item2", "item3"]}'
    Then I should receive a message with the expected list elements