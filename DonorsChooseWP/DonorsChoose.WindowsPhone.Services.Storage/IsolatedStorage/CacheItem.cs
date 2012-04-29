using System;
using System.IO.IsolatedStorage;

namespace DonorsChoose.WindowsPhone.Services.Storage.IsolatedStorage
{
    public class CacheItem<T>
    {
        private string _name;
        private T _value;
        private T _defaultValue;
        private bool _hasValue;
        private DateTime _lastModified;
        private TimeSpan _lifeSpan;

        // Added t the value of the _name variable in order
        // to store the _lastModified value in Isolated Storage
        private const string _lastModifiedSuffix = "_LastModified";


        public CacheItem(string name, T defaultValue, TimeSpan lifeSpan)
        {
            _name = name;
            _defaultValue = defaultValue;
            _lifeSpan = lifeSpan;
            _lastModified = DateTime.Now;
        }


        public T DefaultValue
        {
            get { return _defaultValue; }
        }


        public void ForceRefresh()
        {
            // Clears the cache "lazily" without unnecessarily accessing 
            // Isolated Storage immediately due to the implementation of
            // the accesor for the _value field
            _hasValue = false;
        }


        public T Value
        {
            get
            {
                // Retrieve the value and its timestamp from Isolated Storage
                // if we already haven't done so since the app started
                if (!_hasValue)
                {
                    getValueAndTimestampFromIsolatedStorage();
                }

                // Determine whether or not the cached value is stale
                if (_lastModified.Add(_lifeSpan) < DateTime.Now)
                {
                    resetCache();
                }

                return _value;
            }
            set
            {
                // Save the value in a local backing field in order to avoid
                // unnecessarily accesing Isolated Storage if/when we try to 
                // access this value later on during the program's execution
                _value = value;
                _lastModified = DateTime.Now;
                _hasValue = true;

                // Save the new value in Isolated Storage
                if (IsolatedStorageSettings.ApplicationSettings.Contains(_name))
                {
                    IsolatedStorageSettings.ApplicationSettings[_name] = _value;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings.Add(_name, _value);
                }

                // Save the new timestamp in Isolated Storage
                if (IsolatedStorageSettings.ApplicationSettings.Contains(_name + _lastModifiedSuffix))
                {
                    IsolatedStorageSettings.ApplicationSettings[_name + _lastModifiedSuffix] = _lastModified;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings.Add(_name + _lastModifiedSuffix, _lastModified);
                }
            }
        }


        private void getValueAndTimestampFromIsolatedStorage()
        {
            // Attempt to get the value and its timestamp from Isolated Storage
            if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue(_name, out _value)
                || !IsolatedStorageSettings.ApplicationSettings.TryGetValue(_name + _lastModifiedSuffix, out _lastModified))
            {
                // If either the value or timestamp couldn't be 
                // retrieved then we'll simply reset the cache
                resetCache();
            }

            // Either way, we now have a value and won't need to access
            // Isolated Storage again unnecessarily during the program's execution
            _hasValue = true;
        }


        private void resetCache()
        {
            // Replace the cached value wih the default value
            _value = _defaultValue;

            // Update the timestamp
            _lastModified = DateTime.Now;

            // Save the reset value in Isolated Storage
            if (IsolatedStorageSettings.ApplicationSettings.Contains(_name))
            {
                IsolatedStorageSettings.ApplicationSettings[_name] = _value;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(_name, _value);
            }

            // Save the new timestamp in Isolated Storage
            if (IsolatedStorageSettings.ApplicationSettings.Contains(_name + _lastModifiedSuffix))
            {
                IsolatedStorageSettings.ApplicationSettings[_name + _lastModifiedSuffix] = _lastModified;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(_name + _lastModifiedSuffix, _lastModified);
            }
        }

    }
}
