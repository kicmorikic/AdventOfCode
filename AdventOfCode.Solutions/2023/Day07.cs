using System.Collections.Immutable;

namespace AdventOfCode.Solutions._2023;

public class Day07 : IChallangeSolution
{
    public string ChallangeName => "2023, day 7: Camel Cards";
    public bool HasSecondStage => true;
    
    public string FirstStageInputPath => "Data/2023/07/01.txt";

    public string SecondStageInputPath => FirstStageInputPath;

    public string GetFirstStageResult(IEnumerable<string> input)
    {
        var handList = new List<Hand>();
        foreach (var inputItem in input)
        {
            handList.Add(new Hand(inputItem));
        }
        handList.Sort();
        int winnings = 0;
        for (var i = 0; i < handList.Count; i++)
        {
            winnings+=handList[i].Bid * (i + 1);
        }

        return winnings.ToString();
    }

    public string GetSecondStageResult(IEnumerable<string> input)
    {
        var handList = new List<Hand>();
        foreach (var inputItem in input)
        {
            handList.Add(new Hand(inputItem,true));
        }
        handList.Sort();
        int winnings = 0;
        for (var i = 0; i < handList.Count; i++)
        {
            winnings+=handList[i].Bid * (i + 1);
        }

        return winnings.ToString();
    }

    

    public class Hand : IComparable<Hand>
    {
        private static Dictionary<char, int> CardStrength = new Dictionary<char, int>()
        {
            {'2', 1},
            {'3', 2},
            {'4', 3},
            {'5', 4},
            {'6', 5},
            {'7', 6},
            {'8', 7},
            {'9', 8},
            {'T', 9},
            {'J', 10},
            {'Q', 11},
            {'K', 12},
            {'A', 13},
        };

        private static Dictionary<char, int> CardStrengthStage2 = new Dictionary<char, int>()
        {
            
            {'J', 1},
            {'2', 2},
            {'3', 3},
            {'4', 4},
            {'5', 5},
            {'6', 6},
            {'7', 7},
            {'8', 8},
            {'9', 9},
            {'T', 10},
            {'Q', 11},
            {'K', 12},
            {'A', 13},
        };

        public enum HandType: int
        {
            HighCard = 0,
            OnePair = 1,
            TwoPair = 2,
            ThreeOfAKind = 3,
            FullHouse = 4,
            FourOfAKind = 5,
            FiveOfAKid = 6
        }

        public readonly string _originalHand;
        private readonly int[] _cardValuesInOrder = new int[5];
        private readonly Dictionary<int,int> _cardBuckets = new Dictionary<int, int>();
        private readonly HandType _handType;
        private readonly bool _isStage2;
        public int Bid { get; }

        public Hand(string handDefinition, bool isStage2 = false)
        {
            var handAndBidStr= handDefinition.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            _originalHand = handAndBidStr[0];
            Bid = int.Parse(handAndBidStr[1]);
            if (_originalHand.Length != 5)
                throw new ArgumentException("hand in wrong format. It was supossed to have 5 cards");
            for (var i = 0; i < _originalHand.Length; i++)
            {
                _cardValuesInOrder[i] = isStage2?CardStrengthStage2[_originalHand[i]] :CardStrength[_originalHand[i]];
                if (_cardBuckets.ContainsKey(_cardValuesInOrder[i]))
                {
                    _cardBuckets[_cardValuesInOrder[i]]++;
                }
                else
                {
                    _cardBuckets.Add(_cardValuesInOrder[i],1);
                } 
            }

            _isStage2 = isStage2;
            if (isStage2 && _cardBuckets.ContainsKey(1))
            {
                switch (_cardBuckets.Count)
                {
                    case 1:
                        _handType = HandType.FiveOfAKid;
                        break;
                    case 2:
                        _handType = HandType.FiveOfAKid;
                        break;
                    case 3:
                        _handType = _cardBuckets.Max(kvp => kvp.Value) == 2 && _cardBuckets[1]==1 ? HandType.FullHouse : HandType.FourOfAKind;
                        break;
                    case 4:
                        _handType = HandType.ThreeOfAKind;
                        break;
                    case 5:
                        _handType = HandType.OnePair;
                        break;
                    default:
                        throw new ArgumentException(
                            "hand in wrong format, after analyzing found more than one symbol or didn't find any");
                }
            }
            else
            {
                _handType = _cardBuckets.Count switch
                {
                    1 => HandType.FiveOfAKid,
                    2 => _cardBuckets.Max(kvp => kvp.Value) == 3 ? HandType.FullHouse : HandType.FourOfAKind,
                    3 => _cardBuckets.Max(kvp => kvp.Value) == 3 ? HandType.ThreeOfAKind : HandType.TwoPair,
                    4 => HandType.OnePair,
                    5 => HandType.HighCard,
                    _ => throw new ArgumentException(
                        "hand in wrong format, after analyzing found more than one symbol or didn't find any")
                };
            }
            
        }

        public int CompareTo(Hand? other)
        {
            if (other == null)
                throw new ArgumentException("comparing with null");
            if (other._handType == this._handType)
                return HighCardComparisionFromTheLeft(this, other);
            return CompareInts((int) this._handType, (int) other._handType);

        }

        private int HighCardComparisionFromTheLeft(Hand hand, Hand other)
        {
            for (var i = 0; i < hand._cardValuesInOrder.Length; i++)
            {
                int comparisionResult = CompareInts(hand._cardValuesInOrder[i], other._cardValuesInOrder[i]);
                if (comparisionResult != 0)
                {
                    return comparisionResult;
                }
            }
            return 0;
        }

        private int CompareInts(int first, int second)
        {
            if(first == second)
                return 0;
            if(first > second)
                return 1;
            return -1;
        }
    }
    
}