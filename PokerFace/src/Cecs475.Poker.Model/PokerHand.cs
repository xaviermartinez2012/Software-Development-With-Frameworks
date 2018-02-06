using System;
using System.Collections.Generic;

namespace Cecs475.Poker.Cards {
/// <summary>
/// Represents a Poker hand made up of Cards.
/// </summary>
public class PokerHand: IComparable<PokerHand>{
// 10 types of poker hands, named HighCard, Pair, ThreeOfKind, FourOfKind,
// TwoPair, Flush, FullHouse, Straight, StraightFlush, and RoyalFlush.
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
// The "hand" of cards
public List<Card> Hand {
    get;
    private set;
}
// The type of poker hand
public HandType PokerHandType {
    get;
    private set;
}
// Constructor
public PokerHand(List<Card> hand, HandType pokerHandType) {
    PokerHandType = pokerHandType;
    Hand = hand;
    SortHand();
}

// Sorts the hand in ascending order
private void SortHand() {
    Hand.Sort((c1, c2) => c1.CompareTo(c2));
}

// Determines if the hand is a "straight"
private void IsStraight() {
    Card	current = Hand[0];
    bool	isStraight = true;

    for (int i = 1; i < Hand.Count; i++) {
	if ((current.Kind + 1) != Hand[i].Kind) {
	    isStraight = false;
	    break;
	} else {
	    current = Hand[i];
	}
    }
    if (isStraight)
	PokerHandType = PokerHand.HandType.Straight;
}

// Determines if the hand is a "flush"
private void IsFlush() {
    Card	current = Hand[0];
    bool	isFlush = true;

    for (int i = 1; i < Hand.Count; i++) {
	if (Hand[i].Suit != current.Suit) {
	    isFlush = false;
	    break;
	} else {
	    current = Hand[i];
	}
    }
    if (isFlush)
	PokerHandType = PokerHand.HandType.Flush;
}

// Determines if the hand is a "straight flush"
// Must be "straight" AND "flush"
private void IsStraightFlush() {
    IsStraight();
    if (PokerHandType == PokerHand.HandType.Straight) {
	IsFlush();
	if (PokerHandType == PokerHand.HandType.Flush)
	    PokerHandType = PokerHand.HandType.StraightFlush;
    }
}

// Determines if the hand is a "royal flush"
// Must be "straight flush" AND begin with 10
// Because a straight flush depends on both a
// straight and a flush (implemented in that order),
// the hand is checked against a flush
private void IsRoyalFlush() {
    IsStraightFlush();
    if (PokerHandType == PokerHand.HandType.StraightFlush) {
	if (Hand[0].Kind == Card.CardKind.Ten)
	    PokerHandType = PokerHand.HandType.RoyalFlush;
    } else {
	if (PokerHandType != PokerHand.HandType.Straight)
	    IsFlush();
    }
}

// Helper function for determining if the hand
// is just a "two of a kind (pair)",
// "three of a kind", or
// "four of a kind"
private void IsNOfKind(int numMatches) {
    if ((numMatches < 2) || (numMatches > 4))
	return;
    HandType possibleHand;
    switch (numMatches) {
    case 3:
	possibleHand = PokerHand.HandType.ThreeOfKind;
	break;
    case 4:
	possibleHand = PokerHand.HandType.FourOfKind;
	break;
    default:
	possibleHand = PokerHand.HandType.Pair;
	break;
    }
    for (int i = 0; i < Hand.Count; i++) {
	int matches = 0;
	for (int j = 0; j < Hand.Count; j++) {
	    if (Hand[i].Kind == Hand[j].Kind)
		matches++;
	}
	if (matches == numMatches) {
	    PokerHandType = possibleHand;
	    break;
	}
    }
}

// Determines existance of a pair in the hand
// and returns the base card of the pair
private Card HasPair() {
    Card pair = null;

    for (int i = 0; i < Hand.Count; i++) {
	int matches = 0;
	for (int j = 0; j < Hand.Count; j++) {
	    if (Hand[i].Kind == Hand[j].Kind)
		matches++;
	}
	if (matches == 2) {
	    PokerHandType = PokerHand.HandType.Pair;
	    pair = Hand[i];
	    break;
	}
    }
    return pair;
}

// Determines the existance of a second pair in
// the hand by finding a "first" pair and ingnoring it
// during a second pass for a pair
private void IsTwoPair() {
    Card pair = HasPair();

    if (pair == null)
	return;
    if (PokerHandType == PokerHand.HandType.Pair) {
	for (int i = 0; i < Hand.Count; i++) {
	    if (Hand[i].CompareTo(pair) == 0)
		continue;
	    int matches = 0;
	    for (int j = 0; j < Hand.Count; j++) {
		if (Hand[i].Kind == Hand[j].Kind)
		    matches++;
	    }
	    if (matches == 2) {
		PokerHandType = PokerHand.HandType.TwoPair;
		break;
	    }
	}
    }
}

// Determines if the hand is a "full house"
// Must be only a distinct "three of a kind" AND
// "two of a kind (pair)". Therefore, a "four of a kind"
// is exclusive for a full house and so is a "two pair"
// Lastly, a three of a kind is exclusive from a
// two pair
private void IsFullHouse() {
    IsNOfKind(4);
    if (PokerHandType != PokerHand.HandType.FourOfKind) {
	IsNOfKind(3);
	if (PokerHandType == PokerHand.HandType.ThreeOfKind) {
	    IsNOfKind(2);
	    if (PokerHandType == PokerHand.HandType.Pair)
		PokerHandType = PokerHand.HandType.FullHouse;
	} else {
	    IsTwoPair();
	}
    }
}

// Classifies the hand
public void ClassifyHand() {
    PokerHandType = PokerHand.HandType.HighCard;
    IsRoyalFlush();
    IsFullHouse();
}

// Primary comparison between two poker hands is based on their HandType
// If the two HandTypes are equal, then the comparison is made
// by examining the hands' cards in decreasing order
public int CompareTo(PokerHand other) {
    int compareHandType = PokerHandType.CompareTo(other.PokerHandType);

    if (compareHandType == 0) {
	for (int i = (other.Hand.Count - 1); i > 0; i--) {
	    compareHandType = this.Hand[i].CompareTo(other.Hand[i]);
	    if (compareHandType != 0)
		return compareHandType;
	}
    }
    return compareHandType;
}
}
}
