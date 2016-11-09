using System;
using Chill;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace LiquidProjections.Specs
{
    namespace StatisticsServiceSpecs
    {
        public class When_a_projector_has_processed_a_number_of_commits_in_a_certain_time_frame : GivenWhenThen
        {
            private StatisticsService service;

            public When_a_projector_has_processed_a_number_of_commits_in_a_certain_time_frame()
            {
                DateTime nowUtc = 9.November(2016).At(10, 00);

                Given(() =>
                {
                    service = new StatisticsService(A.Fake<IStoreProjectionStatistics>(), () => nowUtc);    
                });

                When(() =>
                {
                    var statistics = service.GetForProjector("projector1");

                    nowUtc = nowUtc.Add(1.Hours());
                    statistics.TrackCheckpoint(6000);
                });
            }

            [Fact]
            public void Then_it_should_be_able_to_predict_the_time_to_catch_up()
            {
                TimeSpan timeToCatchup = service.GetTimeToCatchup("projector1", 12000);
                timeToCatchup.Should().Be(1.Hours());
            }
        }    
    }
}