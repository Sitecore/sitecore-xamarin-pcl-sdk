Feature: GetItemTest
	In order to get data from sitecore to mobile app
	As a daveloper
	I want to get item using Mobile SDK

@mytag
Scenario: Get Item by ID
	Given I have logged in "authenticatedInstanceURL"
	And I have choosed user
	| Username		  | Password |
	| sitecore\\admin | b        |
	When I send request to get item by id "HomeItemId"
	Then I've got 1 items in response
	And This is Home item

Scenario: Get Item by ID invalid
	Given I have logged in "authenticatedInstanceURL"
	And I have choosed user
	| Username          | Password |
	| sitecore\\admin   | b        |
	When I send request to get item by id "ItemIdInvalid"
	Then I've got 0 items in response

Scenario: Get Item by ID not existent
	Given I have logged in "authenticatedInstanceURL"
	And I have choosed user
	| Username          | Password |
	| sitecore\\admin   | b        |
	When I send request to get item by id "ItemIdNotExistent"
	Then I've got 0 items in response

	
Scenario: Get Item by Path
	Given I have logged in "authenticatedInstanceURL"
	And I have choosed user
	| Username          | Password |
	| sitecore\\admin   | b        |
	When I send request to get item by path "HomeItemPath"
	Then I've got 1 items in response
	And This is Home item


Scenario: Get Item by Path Not Existent
	Given I have logged in "authenticatedInstanceURL"
	And I have choosed user
	| Username          | Password |
	| sitecore\\admin   | b        |
	When I send request to get item by path "NotExistentItemPath"
	Then I've got 0 items in response