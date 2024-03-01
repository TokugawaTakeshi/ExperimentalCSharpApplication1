using MockDataSource.AdditionalStructures;


namespace MockDataSource.SamplesRepositories;


public class PeopleNamesRepository
{

  public static PersonNameData[] PeopleNames => PeopleNamesRepository.JapaneseOnes.
      Concat(PeopleNamesRepository.ArabicOnes).
      ToArray();

  
  public static PersonNameData[] JapaneseOnes = 
  {
    new PersonNameData {
      FamilyName = "田中",
      FamilyNameSpell = "たなか",
      GivenName = "太郎",
      GivenNameSpell = "たろう",
      IsGivenNameForMales = true
    },
    new PersonNameData {
      FamilyName = "山田",
      FamilyNameSpell = "やまだ",
      GivenName = "花子",
      GivenNameSpell = "はなこ",
      IsGivenNameForMales = false
    },
    new PersonNameData {
      FamilyName = "鈴木",
      FamilyNameSpell = "すずき",
      GivenName = "次郎",
      GivenNameSpell = "じろう",
      IsGivenNameForMales = true
    },
    new PersonNameData {
      FamilyName = "佐藤",
      FamilyNameSpell = "さとう",
      GivenName = "美香",
      GivenNameSpell = "みか",
      IsGivenNameForMales = false
    },
    new PersonNameData {
      FamilyName = "高橋",
      FamilyNameSpell = "たかはし",
      GivenName = "裕子",
      GivenNameSpell = "ゆうこ",
      IsGivenNameForMales = false
    },
    new PersonNameData {
      FamilyName = "伊藤",
      FamilyNameSpell = "いとう",
      GivenName = "健太",
      GivenNameSpell = "けんた",
      IsGivenNameForMales = true
    },
    new PersonNameData {
      FamilyName = "渡辺",
      FamilyNameSpell = "わたなべ",
      GivenName = "さくら",
      GivenNameSpell = "さくら",
      IsGivenNameForMales = false
    },
    new PersonNameData {
      FamilyName = "中村",
      FamilyNameSpell = "なかむら",
      GivenName = "大輔",
      GivenNameSpell = "だいすけ",
      IsGivenNameForMales = true
    },
    new PersonNameData {
      FamilyName = "小林",
      FamilyNameSpell = "こばやし",
      GivenName = "愛",
      GivenNameSpell = "あい",
      IsGivenNameForMales = false
    },
    new PersonNameData {
      FamilyName = "加藤",
      FamilyNameSpell = "かとう",
      GivenName = "光一",
      GivenNameSpell = "こういち",
      IsGivenNameForMales = true
    },
    new PersonNameData {
      FamilyName = "吉田",
      FamilyNameSpell = "よしだ",
      GivenName = "真由美",
      GivenNameSpell = "まゆみ",
      IsGivenNameForMales = false
    },
    new PersonNameData {
      FamilyName = "山口",
      FamilyNameSpell = "やまぐち",
      GivenName = "拓海",
      GivenNameSpell = "たくみ",
      IsGivenNameForMales = true
    },
    new PersonNameData
    {
      FamilyName = "斎藤",
      FamilyNameSpell = "さいとう",
      GivenName = "翔",
      GivenNameSpell = "しょう",
      IsGivenNameForMales = true
    },
    new PersonNameData
    {
      FamilyName = "佐々木",
      FamilyNameSpell = "ささき",
      GivenName = "美咲",
      GivenNameSpell = "みさき",
      IsGivenNameForMales = false
    },
    new PersonNameData
    {
      FamilyName = "山田",
      FamilyNameSpell = "やまだ",
      GivenName = "健太",
      GivenNameSpell = "けんた",
      IsGivenNameForMales = true
    },
    new PersonNameData
    {
      FamilyName = "鈴木",
      FamilyNameSpell = "すずき",
      GivenName = "まどか",
      GivenNameSpell = "まどか",
      IsGivenNameForMales = false
    },
    new PersonNameData
    {
      FamilyName = "高橋",
      FamilyNameSpell = "たかはし",
      GivenName = "大輔",
      GivenNameSpell = "だいすけ",
      IsGivenNameForMales = true
    }
  };
  
  public static PersonNameData[] ArabicOnes =
  {
    new PersonNameData
    {
      FamilyName = "عبدالله",
      FamilyNameSpell = "Abdullah",
      GivenName = "محمد",
      GivenNameSpell = "Mohammed",
      IsGivenNameForMales = true
    },
    new PersonNameData
    {
      FamilyName = "محمود",
      FamilyNameSpell = "Mahmoud",
      GivenName = "فاطمة",
      GivenNameSpell = "Fatima",
      IsGivenNameForMales = false
    },
    new PersonNameData
    {
      FamilyName = "علي",
      FamilyNameSpell = "Ali",
      GivenName = "عائشة",
      GivenNameSpell = "Aisha",
      IsGivenNameForMales = false
    },
    new PersonNameData
    {
      FamilyName = "حسين",
      FamilyNameSpell = "Hussein",
      GivenName = "عمر",
      GivenNameSpell = "Omar",
      IsGivenNameForMales = true
    },
    new PersonNameData
    {
      FamilyName = "محمدي",
      FamilyNameSpell = "Mohammadi",
      GivenName = "زينب",
      GivenNameSpell = "Zainab",
      IsGivenNameForMales = false
    },
    new PersonNameData
    {
      FamilyName = "عبدالرحمن",
      FamilyNameSpell = "Abdul Rahman",
      GivenName = "ليلى",
      GivenNameSpell = "Layla",
      IsGivenNameForMales = false
    },
    new PersonNameData
    {
      FamilyName = "أحمد",
      FamilyNameSpell = "Ahmed",
      GivenName = "علي",
      GivenNameSpell = "Ali",
      IsGivenNameForMales = true
    },
    new PersonNameData
    {
      FamilyName = "خالد",
      FamilyNameSpell = "Khalid",
      GivenName = "مريم",
      GivenNameSpell = "Maryam",
      IsGivenNameForMales = false
    },
    new PersonNameData
    {
      FamilyName = "مصطفى",
      FamilyNameSpell = "Mustafa",
      GivenName = "فايزة",
      GivenNameSpell = "Fayza",
      IsGivenNameForMales = false
    },
    new PersonNameData
    {
      FamilyName = "سعيد",
      FamilyNameSpell = "Saeed",
      GivenName = "نور",
      GivenNameSpell = "Nour",
      IsGivenNameForMales = true
    }
  };
  
}