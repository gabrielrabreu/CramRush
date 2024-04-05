@webapi
Feature: SignUp
  As a user
  I want to sign up
  So that I can access the platform

  Scenario: Valid sign up
    Given the user is not authenticated
    When the user submits a request to signup with UserName "user", Email "user@localhost" and Password "User!123"
    Then the response status code should be 204

  Scenario: Invalid sign up
    Given the user is not authenticated
    When the user submits a request to signup with UserName "user", Email "user@localhost" and Password "User"
    Then the response status code should be 400
    And the response for signup should contain the error list
