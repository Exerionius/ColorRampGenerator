﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;

namespace ColorRampGenerator.Prism
{
    public abstract class DelegateCommandBase: ICommand, IActiveAware
    {
        private bool _isActive;

        private readonly SynchronizationContext _synchronizationContext;
        private readonly HashSet<string> _observedPropertiesExpressions = new HashSet<string>();

        /// <summary>
        /// Creates a new instance of a <see cref="DelegateCommandBase"/>, specifying both the execute action and the can execute function.
        /// </summary>
        protected DelegateCommandBase()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public virtual event EventHandler CanExecuteChanged;

        /// <summary>
        /// Raises <see cref="ICommand.CanExecuteChanged"/> so every 
        /// command invoker can re-query <see cref="ICommand.CanExecute"/>.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                if (_synchronizationContext != null && _synchronizationContext != SynchronizationContext.Current)
                    _synchronizationContext.Post((o) => handler.Invoke(this, EventArgs.Empty), null);
                else
                    handler.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises <see cref="CanExecuteChanged"/> so every command invoker
        /// can re-query to check if the command can execute.
        /// </summary>
        /// <remarks>Note that this will trigger the execution of <see cref="CanExecuteChanged"/> once for each invoker.</remarks>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        private void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        /// <summary>
        /// Handle the internal invocation of <see cref="ICommand.Execute(object)"/>
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        protected abstract void Execute(object parameter);

        /// <summary>
        /// Handle the internal invocation of <see cref="ICommand.CanExecute(object)"/>
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns><see langword="true"/> if the Command Can Execute, otherwise <see langword="false" /></returns>
        protected abstract bool CanExecute(object parameter);

        /// <summary>
        /// Observes a property that implements INotifyPropertyChanged, and automatically calls DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        protected void ObservesPropertyInternal<T>(Expression<Func<T>> propertyExpression)
        {
            if (_observedPropertiesExpressions.Contains(propertyExpression.ToString()))
            {
                throw new ArgumentException($"{propertyExpression} is already being observed.", 
                    nameof(propertyExpression));
            }

            _observedPropertiesExpressions.Add(propertyExpression.ToString());
            PropertyObserver.Observes(propertyExpression, RaiseCanExecuteChanged);
        }

        #region IsActive

        /// <summary>
        /// Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value><see langword="true" /> if the object is active; otherwise <see langword="false" />.</value>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnIsActiveChanged();
                }
            }
        }

        /// <summary>
        /// Fired if the <see cref="IsActive"/> property changes.
        /// </summary>
        public virtual event EventHandler IsActiveChanged;

        /// <summary>
        /// This raises the <see cref="DelegateCommandBase.IsActiveChanged"/> event.
        /// </summary>
        protected virtual void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
    
    /// <summary>
    /// Interface that defines if the object instance is active
    /// and notifies when the activity changes.
    /// </summary>
    public interface IActiveAware
    {
        /// <summary>
        /// Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value><see langword="true" /> if the object is active; otherwise <see langword="false" />.</value>
        bool IsActive { get; set; }

        /// <summary>
        /// Notifies that the value for <see cref="IsActive"/> property has changed.
        /// </summary>
        event EventHandler IsActiveChanged;
    }
}