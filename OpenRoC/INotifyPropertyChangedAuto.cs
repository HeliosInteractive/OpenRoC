namespace oroc
{
    using System.ComponentModel;
    using System.Diagnostics;

    public abstract class INotifyPropertyChangedAuto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged()
        {
            string propertyName = new StackTrace()
                .GetFrame(1)
                .GetMethod()
                .Name
                .Remove(0, 4);

            NotifyPropertyChanged(propertyName);
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
