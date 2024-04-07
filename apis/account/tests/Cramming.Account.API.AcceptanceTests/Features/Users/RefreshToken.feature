@webapi
Feature: RefreshToken
    As a user
    I want to be able to refresh my access token
    So that I can maintain my session without re-authenticating frequently

Scenario: User refreshes token successfully
    Given the user is authenticated
    When the user submits a request to refresh token
    Then the response status code should be 200
    And the response should contain a new access token

Scenario: User is not authenticated and receives 401 Unauthorized
    Given the user is not authenticated
    When the user submits a request to refresh token
    Then the response status code should be 401
