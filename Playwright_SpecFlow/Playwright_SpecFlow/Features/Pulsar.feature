@Pulsar_test 
Feature: Pulsar Producer and Consumer Tests

  Scenario: Produce and consume a simple message
    Given a Pulsar producer is running
    When send a simple message
    Then the consumer should receive a message 
