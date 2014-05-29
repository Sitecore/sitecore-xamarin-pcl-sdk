Feature: GetPublicKeyTest
	In order to get data from sitecore securely
	As a daveloper
	I want to get public key

@mytag
Scenario: Get item with correct authentication parameters 
	Given I logged in as sitecore admin user
	When I send request to get Home item by ID
	Then I've got one item in 'Response'

@mytag
Scenario: Get item with invalid instance Url 
	Given I set incorrect instance Url
	When I try to get an item
	Then I've got an AggregateException error

@mytag
Scenario: Get item with null instance Url 
	Given I set null instance Url
	When I try to get an item
	Then I've got an InvalidOperation error