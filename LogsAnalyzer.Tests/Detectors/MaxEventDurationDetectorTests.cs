using FluentAssertions;
using LogsAnalyzer.BLL.Detectors;
using LogsAnalyzer.BLL.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogsAnalyzer.BLL.Tests.Detectors
{
    [TestFixture]
    public class MaxEventDurationDetectorTests
    {
        private MaxEventDurationDetector _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MaxEventDurationDetector();
        }

        [TestCase(5)]
        [TestCase(6)]
        public void Detect_ProvideEventWithDurationThatExceedMaximum_ShouldReturnEventThatExceedMaximum(int eventDuration)
        {
            //Arrange
            var dateTimeOffsetNow = DateTimeOffset.Now;
            var firstEventTime = dateTimeOffsetNow.ToUnixTimeMilliseconds();
            var secondEventTime = dateTimeOffsetNow.AddMilliseconds(eventDuration).ToUnixTimeMilliseconds();
            var events = new List<Event>()
            {
                new Event()
                {
                    Id = "1",
                    State = "STARTED",
                    Timestamp = firstEventTime
                },
                   new Event()
                {
                    Id = "1",
                    State = "FINISHED",
                    Timestamp = secondEventTime,
                    Host = "localhost",
                    Type = "APPLICATION_LOG"
                }
            };

            //Act
            var result = _sut.Detect(events);

            //Assert
            result.Should().ContainSingle();
            result.First().Alert.Should().BeTrue();
            result.First().DurationInMilliseconds.Should().Equals(eventDuration);
            result.First().Host.Should().Equals("localhost");
            result.First().Type.Should().Equals("APPLICATION_LOG");
        }

        [TestCase(1)]
        [TestCase(4)]
        public void Detect_ProvideEventWithDurationThatDoesntExceedMaximum_ShouldReturnEventThatDoesntExceedMaximum(int eventDuration)
        {
            //Arrange

            var firstEventTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var secondEventTime = DateTimeOffset.Now.AddMilliseconds(eventDuration).ToUnixTimeMilliseconds();
            var events = new List<Event>()
            {
                new Event()
                {
                    Id = "1",
                    State = "STARTED",
                    Timestamp = firstEventTime
                },
                   new Event()
                {
                    Id = "1",
                    State = "FINISHED",
                    Timestamp = secondEventTime
                }
            };

            //Act
            var result = _sut.Detect(events);

            //Assert
            result.Should().BeEmpty();
        }
    }
}