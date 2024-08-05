Feature: DeleteContact

  @DeleteContact
  Scenario: Delete an existing contact
    Given I am on the Admin App Page
    When I delete a random contact
    Then the contact should be deleted successfully
