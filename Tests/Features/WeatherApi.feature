Feature: WeatherApi
In order to verify weather service
As a user 
I want to test different combinations of Service

@api @positive @automated
Scenario: Execute Api call to get city data
	When User hits service 'weatherApi'
	Then user can see status code is 200
	And User validate the responce contains city name 'Islamabad'


