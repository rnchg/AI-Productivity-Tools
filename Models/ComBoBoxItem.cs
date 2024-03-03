namespace General.Apt.App.Models
{
    public class ComBoBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public object Item { get; set; }
    }

    public class ComBoBoxItem<V>
    {
        public string Text { get; set; }
        public V Value { get; set; }
        public object Item { get; set; }
    }

    public class ComBoBoxItem<V, I>
    {
        public string Text { get; set; }
        public V Value { get; set; }
        public I Item { get; set; }
    }
}
