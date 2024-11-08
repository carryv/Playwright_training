Feature: Validate True Answers

  Scenario: Validate that "correct_answer": "True" exists
    Given User send a request to "https://opentdb.com/api.php?amount=10&category=9&difficulty=easy&type=boolean"
    When User check the API response for questions with "correct_answer" as "True"
    Then the response should contain at least one "correct_answer": "True"
    And print all questions with correct_answer is True
