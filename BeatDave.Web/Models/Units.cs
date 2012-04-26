
namespace BeatDave.Web.Models
{
    public enum SymbolPosition
    {
        Before = 0,
        After = 1
    }

    public class Units
    {
        public string Symbol { get; set; }
        public SymbolPosition SymbolPosition { get; set; }
        public int Precision { get; set; }
    }
}