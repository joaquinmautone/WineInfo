using System.ComponentModel;

namespace WineInfo.Entities
{
    public enum Variety
    {
        [Description("Red")]
        Red = 1,
        [Description("White")]
        White = 2,
        [Description("Rose")]
        Rose = 3
    }
}
