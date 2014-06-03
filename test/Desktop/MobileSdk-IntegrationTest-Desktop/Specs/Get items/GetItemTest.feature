Feature: GetItemTest
	In order to get data from sitecore to mobile app
	As a daveloper
	I want to get item using Mobile SDK

@mytag
Scenario: Get Item by ID
	Given I have logged in authenticated instance
	And I have choosed user
	| Username		  | Password |
	| sitecore\\admin | b        |
	When I send request to get item by id "HomeItemId"
	Then I've got 1 items in response
	And This is Home item

Scenario: Get Item by invalid ID 
	Given I have logged in authenticated instance
	And I have choosed user
	| Username          | Password |
	| sitecore\\admin   | b        |
	When I send request to get item by id "ItemIdInvalid"
	Then I've got 0 items in response

Scenario: Get Item by not existent ID 
	Given I have logged in authenticated instance
	And I have choosed user
	| Username          | Password |
	| sitecore\\admin   | b        |
	When I send request to get item by id "ItemIdNotExistent"
	Then I've got 0 items in response
	
Scenario: Get Item by Path
	Given I have logged in authenticated instance
	And I have choosed user
	| Username          | Password |
	| sitecore\\admin   | b        |
	When I send request to get item by path "HomeItemPath"
	Then I've got 1 items in response
	And This is Home item

Scenario: Get Item by not existent Path 
	Given I have logged in authenticated instance
	And I have choosed user
	| Username          | Password |
	| sitecore\\admin   | b        |
	When I send request to get item by path "ItemPathNotExistent"
	Then I've got 0 items in response

# for this scenario we should created item with path /sitecore/content/T E S T/i t e m
Scenario: Get Item by Path with spaces
	Given I have logged in authenticated instance
	And I have choosed user
	| Username          | Password |
	| sitecore\\admin   | b        |
	When I send request to get item by path "ItemPathWithSpaces"
	Then I've got 1 items in response
    And This is "i t e m" item with path "ItemPathWithSpaces" and template "Sample/Sample Item" 
	
# for this scenario we should created two the same items with path /sitecore/content/T E S T/i t e m
Scenario: Get Item by Path for two items with same path exist
	Given I have logged in authenticated instance
	And I have choosed user
	| Username          | Password |
	| sitecore\\admin   | b        |
	When I send request to get item by path "ItemPathWithSpaces"
	Then I've got 1 items in response
	And This is "i t e m" item with path "ItemPathWithSpaces" and template "Sample/Sample Item"