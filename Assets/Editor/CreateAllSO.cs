using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class CreateAllSO : EditorWindow
{
    [MenuItem("Liminal/0. СОЗДАТЬ ВСЕ ДАННЫЕ")]
    static void Open() => GetWindow<CreateAllSO>("Create All SO");

    void OnGUI()
    {
        GUILayout.Label("Создание всех ScriptableObjects", EditorStyles.boldLabel);
        GUILayout.Label("Это создаст квесты, диалоги, флаги, inspect\'ы,\nтелефон и заказы кофе.", EditorStyles.wordWrappedLabel);
        GUILayout.Space(20);

        if (GUILayout.Button("СОЗДАТЬ ВСЁ!", GUILayout.Height(50)))
        {
            CreateEverything();
        }
    }

    static void CreateEverything()
    {
        // QUESTS
        var q01 = CreateQuest("Q_01_TakePhone", "take_phone", "Взять телефон с прикроватной тумбы", "Первый квест. Тумбочка в спальне.");
        var q02 = CreateQuest("Q_02_OpenCurtains", "open_curtains", "Открыть шторы", "Окно в спальне.");
        var q03 = CreateQuest("Q_03_TakeShower", "take_shower", "Принять душ", "Душевая кабина в ванной.");
        var q04 = CreateQuest("Q_04_BrushTeeth", "brush_teeth", "Почистить зубы", "Раковина в ванной.");
        var q05 = CreateQuest("Q_05_TakeTowel", "take_towel", "Забрать полотенце", "Полотенце в ванной.");
        var q06 = CreateQuest("Q_06_GetDressed", "get_dressed", "Одеться", "Шкаф в спальне.");
        var q07 = CreateQuest("Q_07_TurnOffPC", "turn_off_pc", "Выключить компьютер", "Компьютер в гостиной.");
        var q08 = CreateQuest("Q_08_TakeKeys", "take_keys", "Взять ключи", "Ключница в прихожей.");
        var q09 = CreateQuest("Q_09_CheckEverything", "check_everything", "Проверить, всё ли взято", "Прихожая.");
        var q10 = CreateQuest("Q_10_LeaveApartment", "leave_apartment", "Выйти из квартиры", "Входная дверь.");

        // Link quests
        q01.nextQuest = q02;
        q02.nextQuest = q03;
        q03.nextQuest = q04;
        q04.nextQuest = q05;
        q05.nextQuest = q06;
        q06.nextQuest = q07;
        q07.nextQuest = q08;
        q08.nextQuest = q09;
        q09.nextQuest = q10;

        SaveAsset(q01); SaveAsset(q02); SaveAsset(q03); SaveAsset(q04); SaveAsset(q05);
        SaveAsset(q06); SaveAsset(q07); SaveAsset(q08); SaveAsset(q09); SaveAsset(q10);

        // GAMEFLAGS
        var f1 = CreateFlag("F_PhoneTaken", "phone_taken", "Телефон взят");
        var f2 = CreateFlag("F_ShowerTaken", "shower_taken", "Душ принят");
        var f3 = CreateFlag("F_TowelTaken", "towel_taken", "Полотенце взято");
        var f4 = CreateFlag("F_Dressed", "dressed", "Одета");
        var f5 = CreateFlag("F_PCTurnedOff", "pc_turned_off", "Компьютер выключен");
        var f6 = CreateFlag("F_KeysTaken", "keys_taken", "Ключи взяты");
        var f7 = CreateFlag("F_EveningMode", "evening_mode", "Вечерний режим");
        SaveAsset(f1); SaveAsset(f2); SaveAsset(f3); SaveAsset(f4); SaveAsset(f5); SaveAsset(f6); SaveAsset(f7);

        // INSPECTS
        SaveAsset(CreateInspect("I_Bed", "Моя кровать. Я мало сплю последнее время."));
        SaveAsset(CreateInspect("I_Bed_Dressed", "Кровать заправлена. Я люблю порядок."));
        SaveAsset(CreateInspect("I_Window", "За окном серое утро. Как всегда."));
        SaveAsset(CreateInspect("I_Chair", "Моё любимое кресло. Здесь я читаю."));
        SaveAsset(CreateInspect("I_Desk", "Рабочий стол. Здесь я рисую."));
        SaveAsset(CreateInspect("I_PC", "Мой компьютер. Нужно его выключить перед уходом."));
        SaveAsset(CreateInspect("I_Clock", "Уже поздно. Нужно торопиться."));
        SaveAsset(CreateInspect("I_Mirror", "Я выгляжу уставшей. Нужно привести себя в порядок."));
        SaveAsset(CreateInspect("I_Drawings", "Мои рисунки. Я рисую, когда тревожно."));
        SaveAsset(CreateInspect("I_Fridge", "Холодильник почти пуст. Нужно купить продукты."));
        SaveAsset(CreateInspect("I_Kettle", "Чайник. Я пью много кофе."));
        SaveAsset(CreateInspect("I_Cups", "Наши кружки. Мы с подругой купили их вместе."));
        SaveAsset(CreateInspect("I_Table", "Кухонный стол. Здесь я завтракаю, если успеваю."));
        SaveAsset(CreateInspect("I_Bath", "Ванна. Иногда я долго лежу здесь, чтобы расслабиться."));
        SaveAsset(CreateInspect("I_Toilet", "..."));
        SaveAsset(CreateInspect("I_Shelves", "Полки с косметикой и лекарствами."));
        SaveAsset(CreateInspect("I_Washer", "Стиральная машина. Нужно постирать вещи."));
        SaveAsset(CreateInspect("I_PS3", "PlayStation 3. Я редко играю сейчас."));
        SaveAsset(CreateInspect("I_TV", "Телевизор. Включаю его для фона."));
        SaveAsset(CreateInspect("I_Sofa", "Диван. Здесь я засыпаю, смотря сериалы."));
        SaveAsset(CreateInspect("I_Plants", "Мои растения. Я стараюсь о них заботиться."));
        SaveAsset(CreateInspect("I_PhoneTable", "Тумбочка. Здесь лежит мой телефон."));
        SaveAsset(CreateInspect("I_MysteriousNote", "Записка... Я не помню, чтобы я её оставляла."));

        // DIALOGUES
        var dIntro = CreateDialogue("D_Intro", new[]{ ("Инга", "Опять этот звук. Каждое утро одно и то же..."), ("Инга", "Нужно встать. Опаздывать нельзя.") });
        var dNotReady = CreateDialogue("D_NotReady", new[]{ ("Инга", "Я ещё не всё сделала. Нужно проверить список дел.") });
        var dAdminStart = CreateDialogue("D_Admin_Start", new[]{ ("Администратор", "Ты опаздываешь. Снова."), ("Инга", "Извини, я..."), ("Администратор", "Нет оправданий. Клиенты ждут. Приступай к работе.") });
        var dAdminEnd = CreateDialogue("D_Admin_End", new[]{ ("Администратор", "Смена окончена. Можешь идти."), ("Инга", "Спасибо. До завтра.") });
        var dFriend = CreateDialogue("D_Friend", new[]{ ("Подруга", "Он опять... Он опять так со мной."), ("Инга", "Всё хорошо. Ты можешь остаться у меня."), ("Подруга", "Спасибо. Ты всегда меня спасаешь.") });
        var dShadows = CreateDialogue("D_Shadows", new[]{ ("???", "Ты видишь нас?"), ("Инга", "Что... кто вы?"), ("???", "Мы всегда были здесь."), ("Инга", "Нет... это не может быть реальным.") });
        var dCS1 = CreateDialogue("D_Coffee_Success1", new[]{ ("Клиент", "Спасибо, вкусно!") });
        var dCF1 = CreateDialogue("D_Coffee_Fail1", new[]{ ("Клиент", "Эм... это не то, что я заказывал. Но ладно.") });
        var dCS2 = CreateDialogue("D_Coffee_Success2", new[]{ ("Клиент", "Отличный кофе, спасибо!") });
        var dCF2 = CreateDialogue("D_Coffee_Fail2", new[]{ ("Клиент", "Странный вкус... Но спасибо.") });
        var dCS3 = CreateDialogue("D_Coffee_Success3", new[]{ ("Клиент", "Идеально! Ты лучшая!") });
        var dCF3 = CreateDialogue("D_Coffee_Fail3", new[]{ ("Клиент", "Ну... сойдёт. В следующий раз внимательнее.") });

        SaveAsset(dIntro); SaveAsset(dNotReady); SaveAsset(dAdminStart); SaveAsset(dAdminEnd);
        SaveAsset(dFriend); SaveAsset(dShadows);
        SaveAsset(dCS1); SaveAsset(dCF1); SaveAsset(dCS2); SaveAsset(dCF2); SaveAsset(dCS3); SaveAsset(dCF3);

        // PHONE CONTACTS
        SaveAsset(CreateContact("C_Mom", "Мама", "Звонит каждое воскресенье. Я редко отвечаю."));
        SaveAsset(CreateContact("C_Friend", "Подруга", "Моя лучшая подруга. Сейчас у неё трудности."));
        SaveAsset(CreateContact("C_Colleague", "Коллега", "Работаем вместе в кофейне."));
        SaveAsset(CreateContact("C_Ex", "Бывший", "Я заблокировала его номер. Но иногда перечитываю старые сообщения."));

        // PHONE MESSAGES
        SaveAsset(CreateMessage("M_Colleague", "Коллега", "Ты опаздываешь. Снова.", true));
        SaveAsset(CreateMessage("M_Mom", "Мама", "Доченька, ты как? Давно не звонила.", false));
        SaveAsset(CreateMessage("M_Friend", "Подруга", "Можно я приду к тебе сегодня?", true));
        SaveAsset(CreateMessage("M_Ex_Old", "Бывший", "Я скучаю. Мы можем поговорить?", true));

        // COFFEE ORDERS
        var o1 = CreateOrder("O_01_Latte", "Анна", "Латте", new[]{ "cup_large", "espresso", "milk_oat" }, false, "Большое латте с овсяным молоком. Без льда.");
        var o2 = CreateOrder("O_02_Cappuccino", "Михаил", "Капучино", new[]{ "cup_small", "espresso", "milk_cow" }, false, "Маленькое капучино. Классическое.");
        var o3 = CreateOrder("O_03_IcedCoffee", "Елена", "Айс-кофе", new[]{ "cup_large", "espresso", "syrup_vanilla", "ice" }, true, "Большой айс-кофе с ванильным сиропом. С льдом.");

        // Link dialogues to coffee orders
        o1.successDialogue = dCS1; o1.failDialogue = dCF1;
        o2.successDialogue = dCS2; o2.failDialogue = dCF2;
        o3.successDialogue = dCS3; o3.failDialogue = dCF3;

        SaveAsset(o1); SaveAsset(o2); SaveAsset(o3);

        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("Готово!", "Все ScriptableObjects созданы.\nКвесты связаны в цепочку.\nДиалоги привязаны к заказам кофе.", "Круто!");
    }

    static QuestData CreateQuest(string name, string id, string display, string note)
    {
        var q = ScriptableObject.CreateInstance<QuestData>();
        q.name = name;
        q.questId = id;
        q.displayText = display;
        q.designerNote = note;
        return q;
    }

    static GameFlagData CreateFlag(string name, string id, string display)
    {
        var f = ScriptableObject.CreateInstance<GameFlagData>();
        f.name = name;
        f.id = id;
        f.displayName = display;
        return f;
    }

    static InspectData CreateInspect(string name, string text)
    {
        var i = ScriptableObject.CreateInstance<InspectData>();
        i.name = name;
        i.entries = new InspectData.Entry[]
        {
            new InspectData.Entry { text = text }
        };
        return i;
    }

    static DialogueData CreateDialogue(string name, (string character, string text)[] lines)
    {
        var d = ScriptableObject.CreateInstance<DialogueData>();
        d.name = name;
        foreach (var line in lines)
        {
            d.entries.Add(new DialogueEntry { characterName = line.character, text = line.text });
        }
        return d;
    }

    static PhoneContactData CreateContact(string name, string cname, string desc)
    {
        var c = ScriptableObject.CreateInstance<PhoneContactData>();
        c.name = name;
        c.contactName = cname;
        c.description = desc;
        return c;
    }

    static PhoneMessageData CreateMessage(string name, string sender, string text, bool read)
    {
        var m = ScriptableObject.CreateInstance<PhoneMessageData>();
        m.name = name;
        m.sender = sender;
        m.messageText = text;
        m.isRead = read;
        return m;
    }

    static CoffeeOrderData CreateOrder(string name, string customer, string drink, string[] ingredients, bool ice, string desc)
    {
        var o = ScriptableObject.CreateInstance<CoffeeOrderData>();
        o.name = name;
        o.customerName = customer;
        o.drinkName = drink;
        o.requiredIngredients = ingredients;
        o.needsIce = ice;
        o.orderDescription = desc;
        return o;
    }

    static void SaveAsset(ScriptableObject so)
    {
        string path = "Assets/ScriptableObjects/";
        if (so is QuestData) path += "Quests/";
        else if (so is GameFlagData) path += "GameFlags/";
        else if (so is InspectData) path += "Inspects/";
        else if (so is DialogueData) path += "Dialogues/";
        else if (so is PhoneContactData) path += "Phone/Contacts/";
        else if (so is PhoneMessageData) path += "Phone/Messages/";
        else if (so is CoffeeOrderData) path += "Coffee/";

        path += so.name + ".asset";
        AssetDatabase.CreateAsset(so, path);
    }
}
