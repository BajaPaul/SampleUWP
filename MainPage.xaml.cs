using LibraryCoder.UtilitiesMisc;
using System;
using System.Diagnostics;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace SampleUWP
{
    enum UseBackButton { Off, On };     // Used to turn back button on or off.  Enumeration limits this to one way or the other.

    public partial class MainPage : Page
    {
        // Declare pointer to this MainPage so other pages can call methods and variable from this MainPage().
        public static MainPage mainPagePointer;

        // Save currentDeviceFamily value here and use to customize layouts for specific devices if needed.
        public DeviceFamily currentDeviceFamily;

        // TODO: do not use following!
        // Gets system accent color set by user in W10 personalization settings.
        // public Color usersSystemAccentColor = (Color)Application.Current.Resources["SystemAccentColor"];

        // ListView variables below used to save last SplitView menu occurrences.  Used on topMenu or botMenu so can deselect them when switching between the menus.
        private ListView topMenuSavedSelection = null;
        private ListView botMenuSavedSelection = null;
        private bool backEvent = false;                 // True if last navigation request came from buttonBack or global back event.

        // UWP apps can use built-in global back event to navigate to previous pages.  But many of Microsoft's apps have moved the back button down into
        // the application's top bar just right of the Hamburger Menu Button.  Show, none, one, the other, or both, by setting following two variables accordingly.
        private readonly UseBackButton backOnGlobal = UseBackButton.Off;        // Manually set to On to show global back event button on window title bar.
        private readonly UseBackButton backOnTitleBar = UseBackButton.On;       // Manually set to On to show back button right of Hamburger Menu Button.

        public MainPage()
        {
            this.InitializeComponent();

            // These are public static variables that allows other pages to call public variables and methods that are part of this MainPage().
            mainPagePointer = this;                 // Set value of pointer declared above

            currentDeviceFamily = LibUM.GetDeviceFamily();  // Get value of currentDeviceFamily.
            Debug.WriteLine($"MainPage(): currentDeviceFamily={currentDeviceFamily}");
            // TODO: Following Xbox code may not be required now.  Verify!
            /*
            if (DeviceFamily.Xbox.Equals(currentDeviceFamily))
            {
                // If Xbox, then adjust margins on title bar items to be inside Safe Border Area Margin, "left,top,right,bottom",
                // or "48,27,48,27" or "48,27".  Appearance will vary depending on actual TV used.
                TblkAppName.Margin = new Thickness(0, 28, 0, 8);
                ButAbout.Margin = new Thickness(50, 0, 0, 8);
                ButBack.Margin = new Thickness(50, 0, 0, 8);
                mainScroller.Margin = new Thickness(28, 0, 28, 8);      // Xbox/TV output requires more margin to prevent chopping.
            } */

            // Register a global back event handler. This can be registered on a per-page-bases if only have a subset of pages
            // that needs to handle back button or if want to do page-specific logic before deciding to navigate back from those pages.
            SystemNavigationManager.GetForCurrentView().BackRequested += Global_BackRequested;

            // If on a phone device that has dedicated back button, then hide the software back button.
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                backOnGlobal = UseBackButton.Off;
                backOnTitleBar = UseBackButton.Off;
            }

            MainScrollerOff();                      // Turn mainScroller off so other pages can set it's values as needed.

            topMenu.ItemsSource = topMenuList;      // Populate the top SplitView menu from list topMenuList defined in menuSetup.cs file.
            botMenu.ItemsSource = botMenuList;      // Populate the bot Splitview menu from list botMenuList defined in menuSetup.cs file.
            topMenu.SelectedIndex = 0;              // Set topMenu[0] as current page which triggers Menu_SelectionChanged event and navigates to topMenu[0].

            //(Application.Current).DebugSettings.EnableFrameRateCounter = true;                         // Override debug settings in app.xaml.cs
            //(Application.Current).DebugSettings.IsTextPerformanceVisualizationEnabled = true;          // Override debug settings in app.xaml.cs
        }

        /************************************* Public Methods ****************************************************/

        /// <summary>
        /// Each page needs to set it's own scroll and zoom requirements.  Only parameters that need to be changed need to be entered.
        /// </summary>
        /// <param name="horz"></param>
        /// <param name="vert"></param>
        /// <param name="horzVis"></param>
        /// <param name="vertVis"></param>
        /// <param name="zoom"></param>
        /// <param name="maxZoom"></param>
        /// <param name="minZoom"></param>
        public void MainScrollerOn(ScrollMode horz = ScrollMode.Auto, ScrollMode vert = ScrollMode.Auto, ScrollBarVisibility horzVis = ScrollBarVisibility.Disabled,
            ScrollBarVisibility vertVis = ScrollBarVisibility.Visible, ZoomMode zoom = ZoomMode.Disabled, float maxZoom = 2.0f, float minZoom = 0.5f)
        {
            mainScroller.HorizontalScrollMode = horz;
            mainScroller.VerticalScrollMode = vert;
            mainScroller.HorizontalScrollBarVisibility = horzVis;
            mainScroller.VerticalScrollBarVisibility = vertVis;
            mainScroller.ZoomMode = zoom;
            mainScroller.MaxZoomFactor = maxZoom;
            mainScroller.MinZoomFactor = minZoom;
        }

        /// <summary>
        /// This is invoked before navigating to a new page and resets mainScroller.  Each page can then set values as needed.
        /// </summary>
        public void MainScrollerOff()
        {
            mainScroller.HorizontalScrollMode = ScrollMode.Disabled;
            mainScroller.VerticalScrollMode = ScrollMode.Disabled;
            mainScroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            mainScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            mainScroller.ChangeView(0d, 0d, 1.0f);      // Reset zoom factor before navigating to new page.
            mainScroller.MaxZoomFactor = 2.0f;
            mainScroller.MinZoomFactor = 0.5f;
            mainScroller.ZoomMode = ZoomMode.Disabled;
        }

        /// <summary>
        /// Update Title Bar Status text and set matching tooltip since truncation possible if space not available.
        /// This also allows other pages to access and update the Title Bar Status via pointer to this page.
        /// </summary>
        /// <param name="status"></param>
        public void UpdateTitleBarStatus(string status)
        {
            titleBarStatus.Text = status;
            ToolTipService.SetToolTip(titleBarStatus, status);
        }

        /************************************* Private Methods ****************************************************/

        /// <summary>
        /// Invoked when user makes a selection from the topMenu or botMenu splitView menus and will navigate to the selected page.
        /// Also invoked whenever SelectedIndex is set as in MainPage() and Global_BackRequested() methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = e;          // Discard unused parameter.
            UpdateTitleBarStatus(string.Empty);     // Clear title bar status and tooltip when navigating
            // Automatically close SplitView menu on selection if DisplayMode is Overlay or CompactOverlay.
            // This matches behavior that MicroSoft is doing with all their apps.
            SplitViewDisplayMode currentDisplayMode = menuSetup.DisplayMode;
            switch (currentDisplayMode)
            {
                case SplitViewDisplayMode.Overlay:          // Overlay passes over the content
                    menuSetup.IsPaneOpen = false;           // Inline pushes content to the right
                    break;
                case SplitViewDisplayMode.CompactOverlay:
                    menuSetup.IsPaneOpen = false;
                    break;
                default:    // Throw exception so error can be discovered and corrected.
                    throw new NotSupportedException("Invalid switch (currentDisplayMode).");
            }
            // Get the triggered event from ListView of SelectedIndex.  Could be from topMenu or botMenu and needs to determined below.
            if (sender is ListView listView)
            {
                if (listView.SelectedItem is MenuItem menuItem)
                {
                    // Test if selected item is from bottom menu since if is typically shortest and least used.  
                    // If it is not from bottom menu, then it should be from top menu.
                    bool isInBotMenu = false;
                    Type currentPage = menuItem.PageNavigate;   // get the current page
                    foreach (MenuItem item in botMenuList)
                    {
                        if (currentPage == item.PageNavigate)
                        {
                            isInBotMenu = true;                 // Selected item is from botMenu
                            break;
                        }
                    }
                    if (isInBotMenu)
                    {
                        botMenuSavedSelection = listView;       // Save current selection so it can be deselected later when go back to topMenu
                        if (topMenuSavedSelection != null)
                            topMenuSavedSelection.SelectedIndex = -1;   // Deselect highlighted selection on topMenu
                    }
                    else
                    {
                        topMenuSavedSelection = listView;       // Save current selection so it can be deselected later when go back to botMenu
                        if (botMenuSavedSelection != null)
                            botMenuSavedSelection.SelectedIndex = -1;   // Deselect highlighted selection on botMenu
                    }
                    titleBarText.Text = menuItem.MenuText;              // Display the current selected menu item in the title bar
                    if (backEvent)              // Skip navigation if global back happened since GoBack already navigated back
                        backEvent = false;      // Reset flag
                    else
                    {
                        MainScrollerOff();                              // Turn mainScroller off before navigating to next page.
                        mainFrame.Navigate(menuItem.PageNavigate);      // Navigate to the page of the item selected from the SplitView menus
                    }
                    if (currentPage == topMenuList[0].PageNavigate)     // Check if current page is the Home page, hide back button if so.
                        ToggleBackButton(UseBackButton.Off);
                    else                        // Show back button if not on Home page, but check if actually can go back first.
                    {
                        if (mainFrame.CanGoBack)
                            ToggleBackButton(UseBackButton.On);
                        else                    // Collapse the back button if can't go back for whatever reason.
                            ToggleBackButton(UseBackButton.Off);
                    }
                }
            }
        }

        /// <summary>
        /// Invoked when user issues a global back on device.
        /// If the app has no in-app back stack left for the current view/frame the user may be navigated away back 
        /// to the previous app in the system's app back stack or to the start screen.  In windowed mode on desktop 
        /// there is no system app back stack and the user will stay in the app even when the in-app back stack is depleted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Global_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // Back Button Navigation:         https://msdn.microsoft.com/en-us/library/windows/apps/mt465734.aspx
            //SystemNavigationManager Class:  https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.core.systemnavigationmanager.aspx

            if (mainFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;       // Mark event as handled so don't get bounced out of this app.
                MainScrollerOff();      // Turn mainScroller off before navigating to next page.
                mainFrame.GoBack();     // Navigate to last/back page.  Now need to update menu selections to match this page.
                GoBackPage();           // Update selection on SplitView menu.
            }
            else    // Hide Back button if can't go back anymore.  Not neccessarily on the Home page yet!  Error or cache expended?
            {
                UpdateTitleBarStatus("Error: CanGoBack is false, unable to navigate back.");
                ToggleBackButton(UseBackButton.Off);
            }
        }

        /// <summary>
        /// Invoked when user selects Back button on title bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            if (mainFrame.CanGoBack)
            {
                MainScrollerOff();      // Turn mainScroller off before navigating to next page.
                mainFrame.GoBack();     // Navigate to last/back page.  Now need to update menu selections to match this page.
                GoBackPage();           // Update selection on SplitView menu.
            }
            else    // Hide Back button if can't go back anymore.  Not neccessarily on the Home page yet!  Error or cache expended?
            {
                UpdateTitleBarStatus("Error: CanGoBack is false, unable to navigate back.");
                ToggleBackButton(UseBackButton.Off);
            }
        }

        /// <summary>
        /// Updates selection on SplitView menu.  This is called from ButtonBack_Click() and Global_BackRequested() events above.
        /// </summary>
        private void GoBackPage()
        {
            backEvent = true;                           // Need to flag that back button was pressed to prevent navigating to page again.
                                                        // Magic/key happens with following statement
            Type navPage = mainFrame.SourcePageType;    // Get current page after navigating back.  Now need find and update SplitVew menu to matching page.

            bool matchInTopMenu = false;
            bool matchInBotMenu = false;
            for (int i = 0; i < topMenuList.Count; i++)             // Search topMenuList for match
            {
                if (navPage == topMenuList[i].PageNavigate)         // Match found if true
                {
                    matchInTopMenu = true;
                    topMenu.SelectedIndex = i;      // Set topMenu[i] as current page, this triggers Menu_SelectionChanged event above which does all the work.
                    break;
                }
            }
            if (!matchInTopMenu)         // No match was found in topMenuList so search botMenuList
            {
                for (int i = 0; i < botMenuList.Count; i++)
                {
                    if (navPage == botMenuList[i].PageNavigate)     // Match found if true;
                    {
                        matchInBotMenu = true;
                        botMenu.SelectedIndex = i;  // Set botMenu[i] as current page, this triggers Menu_SelectionChanged event above which does all the work.
                        break;
                    }
                }
            }
            if (!matchInTopMenu && !matchInBotMenu)     // Just in case if dynamically changing menus and navigate back to a page that is no longer in menu.
            {
                UpdateTitleBarStatus("Error: Item not found in menu that matches current page, unable to update menu selection.");
                backEvent = false;    // Reset flag
            }
        }

        /// <summary>
        /// Toggle back button on or off.
        /// </summary>
        /// <param name="state"></param>
        private void ToggleBackButton(UseBackButton state)
        {
            if (state == UseBackButton.Off)     // Turn back button off
            {
                if (backOnGlobal == UseBackButton.On)
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                if (backOnTitleBar == UseBackButton.On)
                    buttonBack.Visibility = Visibility.Collapsed;
            }
            else  // Turn back button on if it isn't off
            {
                if (backOnGlobal == UseBackButton.On)
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                if (backOnTitleBar == UseBackButton.On)
                    buttonBack.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Invoked when user selects ListViewItem Hamburger Menu Button on title bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hamburger_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            menuSetup.IsPaneOpen = !menuSetup.IsPaneOpen;         // Manually toggle SplitView menu on and off
        }

        /// <summary>
        /// Invoked when user right-clicks on Hamburger Menu Button.  Use to experiment since this overrides current menu style.  
        /// May want to disable this action in any release code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hamburger_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            SplitViewDisplayMode current = menuSetup.DisplayMode;
            switch (current)
            {
                case SplitViewDisplayMode.Overlay:
                    menuSetup.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                    UpdateTitleBarStatus("DisplayMode=CompactOverlay");     // Overlay passes over the content
                    break;
                case SplitViewDisplayMode.CompactOverlay:
                    menuSetup.DisplayMode = SplitViewDisplayMode.Inline;
                    UpdateTitleBarStatus("DisplayMode=Inline");             // Inline pushes content to the right
                    break;
                case SplitViewDisplayMode.Inline:
                    menuSetup.DisplayMode = SplitViewDisplayMode.CompactInline;
                    UpdateTitleBarStatus("DisplayMode=CompactInline");      // Inline pushes content to the right
                    break;
                case SplitViewDisplayMode.CompactInline:
                    menuSetup.DisplayMode = SplitViewDisplayMode.Overlay;
                    UpdateTitleBarStatus("DisplayMode=Overlay");            // Overlay passes over the content
                    break;
                default:    // Throw exception so error can be discovered and corrected.
                    throw new NotSupportedException("Invalid switch (current).");
            }
        }


        // TODO: Delete following method since not used???
        /// <summary>
        /// Invoked by MainPage() above.  If on phone device that has top Status Bar functionality, then hide the phone Status 
        /// Bar to conserve screen space.  This will not work without reference to "Windows Mobile Extensions For The UWP".
        /// </summary>
        //private async void CollapsePhoneStatusBar()
        //{
        //    Windows.UI.ViewManagement.StatusBar statusBar = StatusBar.GetForCurrentView();
        //    await statusBar.HideAsync();
        //    //await statusBar.ShowAsync();    // FYI, this would show the phone Status Bar but it resets on app exit so not needed.
        //}



    }   // End of MainPage()

    /**************************************************************************************************************************************************
    The following items are bound IValueconverers that format items for display on the top and bottom SplitView menus.  IValueConverter knows which list
    to use from initialiation code following "this.InitializeComponent()".  The following statement starts the process, "topMenu.ItemsSource = topMenuList;" */

    // MainPage.xaml calls this via ItemTemplate to alternate the font of left SplitMenu TextBlock depending if it is glyph ot text.
    public class SymbolTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //Debug.WriteLine("Inside SymbolTypeConverter()");
            if (value is MenuItem listItem)
            {
                if (listItem.SymbolType == MenuSymbolType.glyph)
                    return (string)Application.Current.Resources["fontSymbol"];     // SymbolType is a glyph, so return symbol font.
                else
                    return (string)Application.Current.Resources["fontText"];       // Default, SymbolType is text, so return text font.
            }
            else
                return null;
        }

        // No need to implement converting back on one-way binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // MainPage.xaml calls this via ItemTemplate to fill out the text or symbol items in left SplitMenu TextBlock.
    public class SymbolTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //Debug.WriteLine("Inside SymbolTextConverter()");
            if (value is MenuItem listItem)
                return listItem.SymbolText;     // Return SymbolText value from the MenuItem list.t
            else
                return null;
        }

        // No need to implement converting back on one-way binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // MainPage.xaml calls this via ItemTemplate to fill out the text in right SplitMenu TextBlock.
    public class MenuTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //Debug.WriteLine("Inside MenuTextConverter()"
            if (value is MenuItem listItem)
                return listItem.MenuText;       // Return MenuText value from the MenuItem list.
            else
                return null;
        }

        // No need to implement converting back on one-way binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
