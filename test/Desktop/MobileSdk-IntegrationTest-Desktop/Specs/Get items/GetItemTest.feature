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
