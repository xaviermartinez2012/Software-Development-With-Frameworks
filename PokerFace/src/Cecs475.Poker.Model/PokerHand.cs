using System;
using System.Collections.Generic;

namespace Cecs475.Poker.Model {
/// <summary>
/// Represents a Poker hand made up of Cards.
/// </summary>
    public class PokerHand : IComparable<PokerHand>
    {
        public enum HandType {
            HighCard = 1,
            Pair,
            TwoPair,
            ThreeOfKind,
            Straight,
            Flush,
			FullHouse,
			FourOfKind,
			StraightFlush,
			RoyalFlush
		}

        /// <summary>
		/// The cards in hand (in ascending order).
		/// </summary>
		public List<Card> Hand {
			get;
			private set;
		}

        /// <summary>
		/// The "kind" of hand represented.
		/// </summary>
		public HandType PokerHandType {
			get;
			private set;
		}

        public PokerHand(List<Card> hand, HandType pokerHandType) {
            PokerHandType = pokerHandType;
            Hand = hand;
            SortHand();
        }

        public void SortHand() {
            Hand.Sort((c1, c2) => c1.CompareTo(c2));
        }

        public int CompareTo(PokerHand other) {
            int compareHandType = this.PokerHandType.CompareTo(other.PokerHandType);
            if (compareHandType == 0)
            {
                for (int i = (other.Hand.Count - 1); i > 0; i--)
                {
                    compareHandType = this.Hand[i].CompareTo(other.Hand[i]);
                    if (compareHandType != 0) {
                        return compareHandType;
                    }
                }
            }
            return compareHandType;
        }
    }
}
