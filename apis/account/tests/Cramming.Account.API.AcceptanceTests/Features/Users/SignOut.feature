@webapi
Feature: SignOut
    As a user
    I want to be able to sign out of the system
    So that my session is terminated and my refresh token is revoked

    Scenario: User signs out successfully
        Given the user is authenticated
        When the user submits a request to sign out
        Then the response status code should be 204

    Scenario: User is not authenticated and receives 401 Unauthorized
        Given the user is not authenticated
        When the user submits a request to sign out
        Then the response status code should be 401

