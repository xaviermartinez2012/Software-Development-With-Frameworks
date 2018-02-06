using System;
using System.Collections.Generic;

namespace Cecs475.Poker.Cards {
/// <summary>
/// Represents a Poker hand made up of Cards.
/// </summary>
public class PokerHand: IComparable<PokerHand>{
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
    ClassifyHand();
}

private void SortHand() {
    Hand.Sort((c1, c2) => c1.CompareTo(c2));
}

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

private void IsStraightFlush() {
    IsStraight();
    if (PokerHandType == PokerHand.HandType.Straight) {
	IsFlush();
	if (PokerHandType == PokerHand.HandType.Flush)
	    PokerHandType = PokerHand.HandType.StraightFlush;
    }
}

private void IsRoyalFlush() {
    IsStraightFlush();
    if (PokerHandType == PokerHand.HandType.StraightFlush) {
	if (Hand[0].Kind == Card.CardKind.Ten)
	    PokerHandType = PokerHand.HandType.RoyalFlush;
    } else {
	IsFlush();
    }
}

private void IsNOfKind(int numMatches, HandType handType) {
    for (int i = 0; i < Hand.Count; i++) {
	int matches = 0;
	for (int j = 0; j < Hand.Count; j++) {
	    if (Hand[i].Kind == Hand[j].Kind)
		matches++;
	}
	if (matches == numMatches) {
	    PokerHandType = handType;
	    break;
	}
    }
}

private Card HasPair() {
    Card pair = Hand[0];

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

private void IsTwoPair(Card pair) {
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

private void IsFullHouse() {
    IsNOfKind(4, PokerHand.HandType.FourOfKind);
    if (PokerHandType != PokerHand.HandType.FourOfKind) {
	IsNOfKind(3, PokerHand.HandType.ThreeOfKind);
	if (PokerHandType == PokerHand.HandType.ThreeOfKind) {
	    IsNOfKind(2, PokerHand.HandType.Pair);
	    if (PokerHandType == PokerHand.HandType.Pair)
		PokerHandType = PokerHand.HandType.FullHouse;
	} else {
	    IsTwoPair(HasPair());
	}
    }
}

public void ClassifyHand() {
    IsRoyalFlush();
    IsFullHouse();
}

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
