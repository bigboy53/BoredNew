namespace Bored.Manager.Core
{
    public class SelectItem
    {
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public SelectItem() { }
        public SelectItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
        public SelectItem(string text, string value, bool selected)
        {
            Text = text;
            Value = value;
            Selected = selected;
        }
    }
}