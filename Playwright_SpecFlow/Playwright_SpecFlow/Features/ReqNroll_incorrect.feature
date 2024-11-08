Feature: Validate Incorrect Answers

  Scenario: Validate each question has 3 incorrect answers
    Given User send a request to "https://opentdb.com/api.php?amount=50&category=21&difficulty=medium&type=multiple"
    When User receive the incurrect response
    Then each question should have exactly 3 incorrect answers
    And extract and print "correct_answer" for the question "Which car manufacturer won the 2017 24 Hours of Le Mans?"
