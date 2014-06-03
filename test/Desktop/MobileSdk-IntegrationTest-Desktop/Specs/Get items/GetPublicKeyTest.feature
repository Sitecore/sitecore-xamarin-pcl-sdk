Feature: GetPublicKeyTest
	In order to get data from sitecore securely
	As a daveloper
	I want to get public key

@mytag
Scenario: Get item with correct authentication parameters 
	Given I have logged in authenticated instance
	And I have choosed user
	| Username		  | Password |
	| sitecore\\admin | b        |
	When I send request to get item by id "HomeItemId"
	Then I've got 1 items in response

@mytag
Scenario: Get item with invalid instance Url 
	Given I have logged in "http://mobiledev1ua1.dddk.sitecore.net"
	And I have choosed user
	| Username		  | Password |
	| sitecore\\admin | b        |
	When I try to get an item by id "HomeItemId"
	Then I've got an "System.Xml.XmlException" error
	And the error message contains "Name cannot begin with the ' ' character"

@mytag
Scenario: Get item with empty instance Url 
	Given I have logged in ""
	And I have tried to connect as admin user
	Then the error message contains "Value cannot be null"

@mytag
Scenario: Get item with incorrect username and password 
	Given I have logged in authenticated instance
	And I have choosed user
	| Username		   | Password  |
	| sitecore\\nouser | incorrect |
	When I try to get an item by id "HomeItemId"
	Then I've got an "ScAuthenticationError" error
	And the error message contains "Cannot connect with specified username and password"

@mytag
Scenario: Get item with empty password 
	Given I have logged in authenticated instance
	And I have choosed user
	| Username		  | Password  |
	| sitecore\\admin |           |
	When I try to get an item by id "HomeItemId"
	Then I've got an "ScAuthenticationError" error
	And the error message contains "Cannot connect with specified username and password"

@mytag
Scenario: Get item with invalid password 
	Given I have logged in authenticated instance
	And I have choosed user
	| Username	  | Password   |
	| tra-ta-ta$# | pwd^&- + " |
	When I try to get an item by id "HomeItemId"
	Then I've got an "ScAuthenticationError" error
	And the error message contains "Cannot connect with specified username and password"