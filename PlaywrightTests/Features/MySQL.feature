@MySQL_test 
Feature: DatabaseContainer

Scenario: DatabaseContainer
	Given I have a running MySQL Container
	Then the database should be accessible