@Pulsar_test 
Feature: Pulsar Producer and Consumer Tests

  Scenario: Produce and consume a simple message
    Given a Pulsar producer is running
    When Pulsar producer send a simple message
    Then Pulsar consumer should receive a message 

 Scenario: Produce and consume a complex message
    Given a Pulsar producer is running
    When Pulsar producer send a complex message'
    Then Pulsar consumer should receive a message

  Scenario: Produce and consume a list-based message
    Given a Pulsar producer is running
    When Pulsar producer send a list message'
    Then Pulsar consumer should receive a message