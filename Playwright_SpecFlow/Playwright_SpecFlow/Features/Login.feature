Feature: Login to SauceDemo

  @login_test
  Scenario Outline: Test login in SauceDemo
    Given I navigate to the SauceDemo login page
    When I enter the username and password
    And I click the login button
    Then I should see the expected result
