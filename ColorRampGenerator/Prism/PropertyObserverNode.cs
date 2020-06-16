using System;
using System.ComponentModel;
using System.Reflection;

namespace ColorRampGenerator.Prism
{
    internal class PropertyObserverNode
    {
        private readonly Action _action;
        private INotifyPropertyChanged _inPcObject;

        public PropertyInfo PropertyInfo { get; }
        public PropertyObserverNode Next { get; set; }

        public PropertyObserverNode(PropertyInfo propertyInfo, Action action)
        {
            PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            _action = () =>
            {
                action?.Invoke();
                if (Next == null) return;
                Next.UnsubscribeListener();
                GenerateNextNode();
            };
        }

        public void SubscribeListenerFor(INotifyPropertyChanged inPcObject)
        {
            _inPcObject = inPcObject;
            _inPcObject.PropertyChanged += OnPropertyChanged;

            if (Next != null) GenerateNextNode();
        }

        private void GenerateNextNode()
        {
            var nextProperty = PropertyInfo.GetValue(_inPcObject);
            if (nextProperty == null) return;
            if (!(nextProperty is INotifyPropertyChanged nextInPcObject))
                throw new InvalidOperationException("Trying to subscribe PropertyChanged listener in object that " +
                                                    $"owns '{Next.PropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.");

            Next.SubscribeListenerFor(nextInPcObject);
        }

        private void UnsubscribeListener()
        {
            if (_inPcObject != null)
                _inPcObject.PropertyChanged -= OnPropertyChanged;

            Next?.UnsubscribeListener();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Invoke action when e.PropertyName == null in order to satisfy:
            //  - DelegateCommandFixture.GenericDelegateCommandObservingPropertyShouldRaiseOnEmptyPropertyName
            //  - DelegateCommandFixture.NonGenericDelegateCommandObservingPropertyShouldRaiseOnEmptyPropertyName
            if (e?.PropertyName == PropertyInfo.Name || e?.PropertyName == null)
            {
                _action?.Invoke();
            }
        }
    }
}