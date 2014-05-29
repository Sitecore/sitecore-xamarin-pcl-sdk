Feature: GetItemTest
	In order to get data from sitecore to mobile app
	As a daveloper
	I want to get item using Mobile SDK

@mytag
Scenario: Get Item by ID
	Given I logged in as sitecore admin user
	When I send request to get Home item by ID
	Then I've got one item in 'Response'
	And The 'Item' = Home item
