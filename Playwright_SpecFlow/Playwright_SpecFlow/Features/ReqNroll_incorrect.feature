Feature: Validate Incorrect Answers

  Scenario: Validate Incorrect answers
    Given User sends a request to "https://opentdb.com/api.php?amount=50&category=21&difficulty=medium&type=multiple"
    When User receives the API response
    Then each question should have exactly three incorrect answers
    And extract and print "correct_answer" for the question "Which car manufacturer won the 2017 24 Hours of Le Mans?"

