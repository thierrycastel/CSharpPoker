﻿using CsharpPoker;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace CsharpPokerTests
{
    public class HandTests
    {
        [Fact]
        public void CanCreateHand()
        {
            var hand = new Hand();
            hand.Cards.Should().BeEmpty();
        }

        [Fact]
        public void CanHandDrawCard()
        {
            var card = new Card(CardValue.Ace, CardSuit.Spades);
            var hand = new Hand();

            hand.Draw(card);

            hand.Cards.First().Should().Be(card);
        }

        [Fact]
        public void CanGetHighCard()
        {
            var hand = new Hand()
                .Draw(new Card(CardValue.Seven, CardSuit.Spades))
                .Draw(new Card(CardValue.Ten, CardSuit.Clubs))
                .Draw(new Card(CardValue.Five, CardSuit.Hearts))
                .Draw(new Card(CardValue.King, CardSuit.Hearts))
                .Draw(new Card(CardValue.Two, CardSuit.Hearts));

            hand.HighCard().Value.Should().Be(CardValue.King);
            // Assert.Equal(CardValue.King, hand.HighCard().Value);
        }

        [Fact]
        public void CanScoreFlush()
        {
            var hand = new Hand()
                .Draw(new Card(CardValue.Two, CardSuit.Spades))
                .Draw(new Card(CardValue.Three, CardSuit.Spades))
                .Draw(new Card(CardValue.Ace, CardSuit.Spades))
                .Draw(new Card(CardValue.Five, CardSuit.Spades))
                .Draw(new Card(CardValue.Six, CardSuit.Spades));

            hand.GetHandRank().Should().Be(HandRank.Flush);
        }

        [Fact]
        public void CanScoreRoyalFlush()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Queen, CardSuit.Spades));
            hand.Draw(new Card(CardValue.King, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));

            hand.GetHandRank().Should().Be(HandRank.RoyalFlush);
        }
    }
}