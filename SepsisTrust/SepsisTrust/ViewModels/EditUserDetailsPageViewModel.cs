﻿using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using PCLStorage;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SepsisTrust.Model.Storage;
using SepsisTrust.Model.User;

namespace SepsisTrust.ViewModels
{
    public class EditUserDetailsPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IJsonObjectStreamReader _userDataStreamReader;
        private readonly IJsonObjectStreamWriter _userDataStreamWriter;
        private readonly IFileStreamRetriever _fileStreamRetriever;
        private AppUserData _appUserData;

        private string _designation;

        private string _forename;

        private string _surname;

        private DelegateCommand _saveCommand;

        public DelegateCommand SaveCommand
        {
            get { return _saveCommand; }
            set { SetProperty(ref _saveCommand, value); }
        }

        private List<string> _selectableItems;

        public List<string> SelectableItems
        {
            get { return _selectableItems; }
            set { SetProperty(ref _selectableItems, value); }
        }

        public EditUserDetailsPageViewModel( INavigationService navigationService , IJsonObjectStreamReader userDataStreamReader, IJsonObjectStreamWriter userDataStreamWriter, IFileStreamRetriever fileStreamRetriever )
        {
            _navigationService = navigationService;
            _userDataStreamReader = userDataStreamReader;
            _userDataStreamWriter = userDataStreamWriter;
            _fileStreamRetriever = fileStreamRetriever;

            SaveCommand = new DelegateCommand(SaveData);

            SelectableItems = new List<string>() {"Hello", "NOpe", "Yes"};
        }

        public string Forename
        {
            get { return _forename; }
            set
            {
                SetProperty(ref _forename, value);
                _appUserData.Forename = value;
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                SetProperty(ref _surname, value);
                _appUserData.Surname = value;
            }
        }

        public string Designation
        {
            get { return _designation; }
            set
            {
                SetProperty(ref _designation, value);
                _appUserData.Designation = value;
            }
        }


        public void OnNavigatedFrom( NavigationParameters parameters )
        {
        }

        public void OnNavigatedTo( NavigationParameters parameters )
        {
        }

        public async void OnNavigatingTo( NavigationParameters parameters )
        {
            // Check if user data is already in memory
            if ( StaticUserDataStore.UserData != null )
            {
                // This copies the memory-based user data, thus ensuring that
                // the Save/Back functionality works.
                // If this is directly set to the memory-based store, changes will
                // be automatically "saved".
                var memoryAppUserData = StaticUserDataStore.UserData;
                _appUserData = new AppUserData();
                foreach ( var runtimeProperty in memoryAppUserData.GetType().GetRuntimeProperties() )
                {
                    var memoryAppData = runtimeProperty.GetValue(memoryAppUserData);
                    runtimeProperty.SetValue(_appUserData, memoryAppData);
                }
                /*_appUserData = new AppUserData
                               {
                                   Forename = memoryAppUserData.Forename,
                                   Surname = memoryAppUserData.Surname,
                                   Designation = memoryAppUserData.Designation
                               };*/
            }

            // Otherwise, load app user data from file path.
            if ( _appUserData == null )
            {
                // This loads the data from persistent storage into a new instance
                // of app user data.
                _appUserData = new AppUserData();
                _appUserData = await LoadAppUserDataFromFileAsync(StaticUserDataStore.UserFileName);
            }

            // If no data has been specified, then create new app user data.
            if ( _appUserData == null )
            {
                // This simply creates a new app user data.
                _appUserData = new AppUserData();
            }

            // Load the data for the view model properties.
            Forename = _appUserData?.Forename;
            Surname = _appUserData?.Surname;
            Designation = _appUserData?.Designation;
        }

        /// <summary>
        /// This method saves the user data both to memory, and subsequently to persistent storage.
        /// </summary>
        private async void SaveData( )
        {
            // Save the user data to memory.
            StaticUserDataStore.UserData = _appUserData;

            // Write the data to persistent storage.
            var fileWriteStream = await _fileStreamRetriever.ObtainFileStreamAsync(StaticUserDataStore.UserFileName, true, FileAccess.ReadAndWrite);
            var writeTask = _userDataStreamWriter.Write(fileWriteStream, _appUserData);

            // Navigate back to previous page upon saving.
            await _navigationService.GoBackAsync();
        }

        private async Task<AppUserData> LoadAppUserDataFromFileAsync( string userFileName )
        {
            var fileReadStream = await _fileStreamRetriever.ObtainFileStreamAsync(userFileName, false, FileAccess.Read);
            return await _userDataStreamReader.Read<AppUserData>(fileReadStream);
        }
    }
}