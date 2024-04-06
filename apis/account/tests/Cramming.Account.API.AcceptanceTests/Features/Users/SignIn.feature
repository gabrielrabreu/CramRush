Feature: SignIn
  As a user
  I want to sign in
  So that I can access the platform

  Scenario: Valid sign in
    Given the user is not authenticated
    And there exists an user with UserName "user" and Password "User!123"
    When the user submits a request to sign in with UserName "user" and Password "User!123"
    Then the response status code should be 200
    And the response should contain the user's access token

  Scenario: Invalid sign in
    Given the user is not authenticated
    And there exists an user with UserName "user" and Password "User!123"
    When the user submits a request to sign in with UserName "user" and Password "InvalidPassword"
    Then the response status code should be 401
