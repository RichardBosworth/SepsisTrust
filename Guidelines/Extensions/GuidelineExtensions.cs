using System;
using System.Collections.Generic;
using Prism.Events;

namespace Guidelines.Extensions
{
    public static class GuidelineExtensions
    {
        private static List<GuidelineExtension> RegisteredExtensions { get; set; } = new List<GuidelineExtension>();

        public static void Register<T>( IEventAggregator eventAggregator ) where T : GuidelineExtension
        {
            T instance = Activator.CreateInstance(typeof(T), eventAggregator) as T;
            if ( instance != null )
            {
                RegisteredExtensions.Add(instance);
            }
        }
    }
}