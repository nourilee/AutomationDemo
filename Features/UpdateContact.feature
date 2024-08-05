Feature: UpdateContact

  @UpdateContact
  Scenario: Update an existing contact with new address data
    Given I am on the Admin App Page
    When I update the contact's address
    Then the address should be updated successfully
