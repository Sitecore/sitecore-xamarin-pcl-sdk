Feature: GetPublicKeyTest
	In order to get data from sitecore securely
	As a daveloper
	I want to get public key

@mytag
Scenario: Get item with correct authentication parameters 
	Given I logged in as sitecore admin user
	When I send request to get Home item by ID
	Then I've got one item in 'Response'
