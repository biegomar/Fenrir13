using System.Text;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;
using Heretic.InteractiveFiction.Resources;
using Heretic.InteractiveFiction.Subsystems;

namespace Fenrir13.Printing;

internal class ConsolePrintingSubsystem : IPrintingSubsystem
{
    public bool ActiveLocation(Location activeLocation, IDictionary<Location, IEnumerable<DestinationNode>> locationMap)
    {
        Console.Write(wordWrap(activeLocation, Console.WindowWidth));
        DestinationNode(activeLocation, locationMap);
        return true;
    }

    public bool ActivePlayer(Player activePlayer)
    {
        Console.WriteLine(wordWrap(activePlayer, Console.WindowWidth));
        return true;
    }

    public bool AlterEgo(AContainerObject item)
    {
        if (item == default)
        {
            Console.Write(wordWrap(BaseDescriptions.ITEM_NOT_VISIBLE, Console.WindowWidth));
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine(wordWrap(item.AlterEgo(), Console.WindowWidth));
        }
        return true;
    }

    public bool AlterEgo(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Console.Write(wordWrap(BaseDescriptions.ITEM_NOT_VISIBLE, Console.WindowWidth));
            Console.WriteLine();

        }
        else
        {
            var result = new StringBuilder();
            result.AppendFormat(BaseDescriptions.ALTER_EGO_DESCRIPTION, this.GetObjectName(itemName));
            result.AppendLine(string.Join(", ", itemName.Split('|')));
            Console.WriteLine(wordWrap(result.ToString(), Console.WindowWidth));
        }

        return true;
    }

    public bool CanNotUseObject(string objectName)
    {
        Console.Write(wordWrap(BaseDescriptions.ITEM_UNKNOWN, Console.WindowWidth), objectName);
        Console.WriteLine();

        return true;
    }

    public void ClearScreen()
    {
        Console.Clear();
    }

    public bool PrintObject(AContainerObject item)
    {
        if (item == default)
        {
            Console.Write(wordWrap(BaseDescriptions.ITEM_NOT_VISIBLE, Console.WindowWidth));
            Console.WriteLine();
        }
        else
        {
            Console.Write(wordWrap(item, Console.WindowWidth));
        }
        return true;
    }

    public bool Help(IDictionary<string, IEnumerable<string>> verbResource)
    {
        var description = new StringBuilder();
        description.AppendLine(BaseDescriptions.HELP_DESCRIPTION);
        description.AppendLine(new string('-', BaseDescriptions.HELP_DESCRIPTION.Length)).AppendLine();

        foreach (var verbs in verbResource)
        {
            description.AppendLine(BaseDescriptions.ResourceManager.GetString(verbs.Key));
            var index = 0;
            foreach (var value in verbs.Value)
            {
                description.Append(index != 0 ? ", " : "...");

                description.Append(value);
                index++;
            }
            description.AppendLine().AppendLine();
        }

        Console.Write(wordWrap(description, Console.WindowWidth));

        return true;
    }

    public bool History(ICollection<string> historyCollection)
    {
        var history = new StringBuilder(historyCollection.Count);
        history.AppendJoin(Environment.NewLine, historyCollection);

        Console.WriteLine(wordWrap(history, Console.WindowWidth));

        return true;
    }

    public bool ItemNotVisible()
    {
        return Resource(BaseDescriptions.ITEM_NOT_VISIBLE);
    }

    public bool ImpossiblePickup(AContainerObject containerObject)
    {
        if (containerObject != default)
        {
            if (!string.IsNullOrEmpty(containerObject.UnPickAbleDescription))
            {
                return Resource(containerObject.UnPickAbleDescription);
            }

            if (containerObject is Character)
            {
                return Resource(BaseDescriptions.IMPOSSIBLE_CHARACTER_PICKUP);
            }
        }

        return Resource(BaseDescriptions.IMPOSSIBLE_PICKUP);
    }

    public bool ItemToHeavy()
    {
        return Resource(BaseDescriptions.TO_HEAVY);
    }

    public bool ItemPickupSuccess(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.ITEM_PICKUP, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool ImpossibleDrop(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.IMPOSSIBLE_DROP, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool ItemAlreadyClosed(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.ALREADY_CLOSED, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool ItemClosed(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.NOW_CLOSED, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool ItemAlreadyOpen(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.ALREADY_OPEN, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool ItemAlreadyUnlocked(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.ALREADY_UNLOCKED, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool ItemStillLocked(AContainerObject item)
    {
        Console.Write(!string.IsNullOrEmpty(item.LockDescription)
            ? wordWrap(item.LockDescription, Console.WindowWidth)
            : string.Format(wordWrap(BaseDescriptions.ITEM_STILL_LOCKED, Console.WindowWidth), item.Name));

        Console.WriteLine();
        return true;
    }

    public bool ItemUnlocked(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.ITEM_UNLOCKED, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool ItemNotLockAble(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.ITEM_NOT_LOCKABLE, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool ItemOpen(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.NOW_OPEN, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool ItemDropSuccess(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.ITEM_DROP, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool ItemNotOwned()
    {
        return Resource(BaseDescriptions.ITEM_NOT_OWNED);
    }

    public bool ItemAlreadyOwned()
    {
        return Resource(BaseDescriptions.ITEM_ALREADY_OWNED);
    }

    public bool DestinationNode(Location activeLocation, IDictionary<Location, IEnumerable<DestinationNode>> locationMap)
    {
        if (locationMap.ContainsKey(activeLocation))
        {
            var unhiddenMappings = locationMap[activeLocation].Where(l => !l.IsHidden).ToList();
            if (unhiddenMappings.Any())
            {
                foreach (var item in unhiddenMappings)
                {
                    Console.Write(wordWrap(item, Console.WindowWidth));
                }

                Console.WriteLine();
            }
        }
        return true;
    }

    public bool Misconcept()
    {
        return Resource(BaseDescriptions.MISCONCEPTION);
    }

    public bool NothingToTake()
    {
        return Resource(BaseDescriptions.NOTHING_TO_TAKE);
    }

    public bool NoAnswer(string phrase)
    {
        Console.WriteLine($@"{string.Format(wordWrap(BaseDescriptions.NO_ANSWER, Console.WindowWidth), phrase)}");
        return true;
    }

    public bool NoAnswerToInvisibleObject(Character character)
    {
        string genderSwitch = character.Gender switch
        {
            Genders.Female => BaseDescriptions.GENDER_FEMALE,
            Genders.Male => BaseDescriptions.GENDER_MALE,
            _ => BaseDescriptions.GENDER_UNKNOWN
        };
        
        Console.WriteLine($@"{string.Format(wordWrap(BaseDescriptions.ASK_FOR_INVISIBLE_OBJECT, Console.WindowWidth), genderSwitch)}");
        return true;
    }

    public bool NoAnswerToQuestion(string phrase)
    {
        Console.WriteLine($@"{string.Format(wordWrap(BaseDescriptions.NO_ANSWER_TO_QUESTION, Console.WindowWidth), phrase)}");
        return true;
    }

    public bool Opening()
    {
        var (version, productName) = GetMetaInfo();
        Console.WriteLine($@"{productName} - {version}");
        Console.WriteLine();
        Console.Write(wordWrap(BaseDescriptions.HELLO_STRANGER, Console.WindowWidth));
        Console.Write(wordWrap(Descriptions.OPENING, Console.WindowWidth));
        Console.WriteLine();
        return true;
    }

    public bool Resource(string resource)
    {
        if (!string.IsNullOrEmpty(resource))
        {
            Console.Write(wordWrap(resource, Console.WindowWidth));
            Console.WriteLine();
        }
        return true;
    }

    public bool Score(int score, int maxScore)
    {
        Console.WriteLine($@"{string.Format(BaseDescriptions.SCORE, score, maxScore)}");
        return true;
    }

    public bool Talk(Character character)
    {
        if (character != default)
        {
            var talk = character.Talk();
            if (!string.IsNullOrEmpty(talk))
            {
                Console.WriteLine(wordWrap(talk, Console.WindowWidth));
            }
            else
            {
                Console.WriteLine(wordWrap(BaseDescriptions.WHAT, Console.WindowWidth));
            }
            Console.WriteLine();
        }
        return true;
    }

    public bool TitleAndScore(int score, int maxScore)
    {
        var (version, productName) = GetMetaInfo();
        Console.Title = $@"{productName} - {version} ({string.Format(BaseDescriptions.SCORE, score, maxScore)} )";
        return true;
    }

    public bool WrongKey(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.WRONG_KEY, Console.WindowWidth), item.Name);
        Console.WriteLine();
        return true;
    }

    public bool Prompt()
    {
        Console.Write(@"> ");
        return true;
    }

    public bool PayWithWhat()
    {
        return Resource(BaseDescriptions.PAY_WITH_WHAT);
    }

    public bool SameActionAgain()
    {
        return Resource(BaseDescriptions.SAME_ACTION_AGAIN);
    }

    public bool NoEvent()
    {
        return Resource(BaseDescriptions.NO_EVENT);
    }

    private static (string version, string productName) GetMetaInfo()
    {
        return (MetaData.VERSION, MetaData.DESCRIPTION);
    }

    public bool WayIsLocked(AContainerObject item)
    {
        if (!string.IsNullOrEmpty(item.LockDescription))
        {
            return Resource(item.LockDescription);
        }

        return Resource(BaseDescriptions.WAY_IS_LOCKED);
    }

    public bool WayIsClosed(AContainerObject item)
    {
        if (!string.IsNullOrEmpty(item.CloseDescription))
        {
            return Resource(item.CloseDescription);
        }

        return Resource(BaseDescriptions.WAY_IS_CLOSED);
    }

    public bool ImpossibleUnlock(AContainerObject item)
    {
        Console.Write(wordWrap(BaseDescriptions.IMPOSSIBLE_UNLOCK, Console.WindowWidth), this.LowerFirstChar(item.Name));
        Console.WriteLine();
        return true;
    }

    private string LowerFirstChar(string description)
    {
        return description[..1].ToLower() + description[1..];
    }

    private static string wordWrap(string message, int width)
    {
        var messageLines = message.Split(Environment.NewLine);
        var result = new StringBuilder();

        foreach (var messageLine in messageLines)
        {
            var newMessage = messageLine;

            while (!string.IsNullOrWhiteSpace(newMessage) && newMessage.Length > width)
            {
                int start = 0;
                int last = 0;
                do
                {
                    start = last;
                    last = newMessage.IndexOf(' ', start + 1);
                } while (last > -1 && last < width);

                result.AppendLine(newMessage.Substring(0, start));
                newMessage = newMessage.Substring(start + 1);
            }

            result.AppendLine(newMessage);
        }

        return result.ToString();
    }

    private static string wordWrap(object message, int width)
    {
        return wordWrap(message.ToString(), width);
    }

    private string GetObjectName(string itemName)
    {
        var sentence = itemName.Split('|');
        return sentence[0].Trim();
    }

    public bool Credits()
    {
        Console.WriteLine($"{MetaData.DESCRIPTION} - {MetaData.VERSION}");
        Console.WriteLine($"Written by {MetaData.AUTHOR}");
        Console.WriteLine($"{MetaData.COPYRIGHT}");

        return true;
    }
}