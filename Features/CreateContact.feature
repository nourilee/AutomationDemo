Feature: CreateContact
    
  @CreateContact
  Scenario: Create a new contact with generated data
    Given I am on the Admin App Page
    When I create a new contact
    Then the contact should be created successfully