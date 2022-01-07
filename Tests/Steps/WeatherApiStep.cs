using System;
using TechTalk.SpecFlow;
using ApiBddAutomationFramework.Services;

namespace ApiBddAutomationFramework
{
    [Binding]
    public class WeatherApiStep
    {
        private Service service = new Service();

        [When(@"User hits service '([^']*)'")]
        public void WhenUserHitsService(string ServiceName)
        {
            service.CreateAndCallServiceRequest(ServiceName);
            
        }

        [Then(@"user can see status code is (.*)")]
        public void ThenUserCanSeeStatusCodeIs(int StatusCode)
        {
            service.VerifyStatusCode(StatusCode);
        }

        [Then(@"User validate the responce contains city name '([^']*)'")]
        public void ThenUserValidateTheResponceContainsCityName(string CityName)
        {
            service.VerifyResponseContent(CityName);
        }
    }
}
