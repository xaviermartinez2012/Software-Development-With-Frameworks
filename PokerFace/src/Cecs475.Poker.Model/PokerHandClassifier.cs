using System;
using System.Collections.Generic;
namespace Cecs475.Poker.Cards {
public class PokerHandClassifier {
public static PokerHand ClassifyHand(IEnumerable<Card> hand) {
    PokerHand classifiedHand = new PokerHand(new List<Card>(hand), PokerHand.HandType.HighCard);

    return classifiedHand;
}
}
}
