using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureHunt.Test.Input;
using Xunit;

namespace TreasureHunt.Test
{
    public class HuntService_Test : AbstractTest
    {
        [Theory]
        [MemberData(nameof(HuntData.SingleAdventurerTreasureFound), MemberType = typeof(HuntData))]
        public void LaunchHunt_SingleAdventurer_OK(IList<string> fileContent)
        {
            // Arrange
            InitializeTest();

            // Act
            var huntService = GetService<IHuntService>();

            // Assert
            var result = huntService.LaunchHunt(fileContent);
            result.Adventurers.Count().Should().Be(1);
            result.Adventurers[0].ToString().Should().Be("A-Lara-0-3-S-3");
            result.Treasures[0].ToString().Should().Be("T-0-3-0");
            result.Treasures[1].ToString().Should().Be("T-1-3-2");
        }

        [Theory]
        [MemberData(nameof(HuntData.MultipleAdventurersWithConflicts), MemberType = typeof(HuntData))]
        public void LaunchHunt_MultipleAdventurersWithConflicts_OK(IList<string> fileContent)
        {
            // Arrange
            InitializeTest();

            // Act
            var huntService = GetService<IHuntService>();

            // Assert
            var result = huntService.LaunchHunt(fileContent);
            result.Adventurers.Count().Should().Be(3);
            result.Adventurers[0].ToString().Should().Be("A-Lara-0-3-S-3");
            result.Adventurers[1].ToString().Should().Be("A-Robert-1-4-S-1");
            result.Adventurers[2].ToString().Should().Be("A-John-2-5-S-1");
            result.Treasures[0].ToString().Should().Be("T-0-3-0");
            result.Treasures[1].ToString().Should().Be("T-1-3-0");
        }
    }
}
