using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    // Enumeration used in class and various menu lists below.
    public enum MenuSymbolType { text, glyph };       // If text, use resource "fontText"=Segoe UI.  if glyph, use resource "fontSymbol"=Segoe MDL2 Assets.

    // Class used by various menus below, all List<generic>.
    public class MenuItem
    {
        public MenuSymbolType SymbolType { get; set; }
        public string SymbolText { get; set; }
        public string MenuText { get; set; }
        public Type PageNavigate { get; set; }
    }

    public sealed partial class MainPage : Page
    {
        /*
        The Menu/Hamburger button on title bar activates a Splitview menu that has a top menu and a bottom menu.  Two of list below
        establish the contents of those two menus respectively.  Edit the two lists below to set what appears on the menus and what page
        to navigate to when selected.  For 'SymbolType', you can use symbols (glyphs) or text string.

        To use symbols, use the Segoe MDL2 Assets font, Menu/Hamburger for instance, the C# unicode character escape sequence is "\uE700".
        If entering the same symbol directly into XAML, you use "&#xE700".  The XAML version will not work in the C# code behind.
        Guidelines for Segoe MDL2 icons: https://msdn.microsoft.com/en-us/windows/uwp/controls-and-patterns/segoe-ui-symbol-font
        Segoe MDL2 Assets - Cheatsheet: http://modernicons.io/segoe-mdl2/cheatsheet/
        Unicode in C#:   Hamburger=\uE700,  Home=\uE80F,  Back=\uE72B,  Forward=\uE72A,  Page=\uE7C3
        Unicode in XAML: Hamburger=&#xE700, Home=&#xE80F, Back=&#xE72B, Forward=&#xE72A, Page=&#xE7C3
        */

        // Primary menu that shows up below the hamburger button on topMenu defined in MainPage.xaml.
        public List<MenuItem> topMenuList = new List<MenuItem>     // First item in this list is always the "Home" page.
        {
            new MenuItem() { SymbolType=MenuSymbolType.glyph, SymbolText = "\uE80F",   MenuText = "Beam Concentrated Load",   PageNavigate = typeof(P01)},
            new MenuItem() { SymbolType=MenuSymbolType.glyph, SymbolText = "\uE7E6",   MenuText = "Theme Resources",          PageNavigate = typeof(P02)},
            new MenuItem() { SymbolType=MenuSymbolType.glyph, SymbolText = "\uE8E9",   MenuText = "Rich Text Blocks",         PageNavigate = typeof(P03)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P04",      MenuText = "Grouping LV and GV",       PageNavigate = typeof(P04)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P05",      MenuText = "Pivot Basic",              PageNavigate = typeof(P05)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P06",      MenuText = "Binding Samples",          PageNavigate = typeof(P06)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P07",      MenuText = "Responsive Techniques",    PageNavigate = typeof(P07)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P08",      MenuText = "Explore Shapes",           PageNavigate = typeof(P08)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P09",      MenuText = "Win2D Shape Sample",       PageNavigate = typeof(P09)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P10",      MenuText = "Win2D Geometry Sample",    PageNavigate = typeof(P10)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P11",      MenuText = "Win2D Practice Drawings",  PageNavigate = typeof(P11)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P12",      MenuText = "Win2D Animation",          PageNavigate = typeof(P12)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P13",      MenuText = "Win2D Delegate Print",     PageNavigate = typeof(P13)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P14",      MenuText = "Win2D Printing Example",   PageNavigate = typeof(P14)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P15",      MenuText = "ComboBox Samples",         PageNavigate = typeof(P15)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P16",      MenuText = "Console Code Samples",     PageNavigate = typeof(P16)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "P17",      MenuText = "Test Numeric Input Types", PageNavigate = typeof(P17)}
        };

        // Secondary menu that shows up below the hamburger button on botMenu defined in MainPage.xaml.
        public List<MenuItem> botMenuList = new List<MenuItem>
        {
            new MenuItem() { SymbolType=MenuSymbolType.glyph, SymbolText = "\uE713",   MenuText = "Settings",                   PageNavigate = typeof(S01)},
            new MenuItem() { SymbolType=MenuSymbolType.text,  SymbolText = "S02",      MenuText = "Window Size Inforamtion",    PageNavigate = typeof(S02)}
        };

        // Another menu that shows up via Binding Samples selected from topMenu below the hamburger button.  These items are displayed via a Pivot menu.
        public List<MenuItem> bindingMenuList = new List<MenuItem>
        {
            new MenuItem() { SymbolType=MenuSymbolType.text,   SymbolText = "B01",      MenuText = "ValueConverters",         PageNavigate = typeof(B01)},
            new MenuItem() { SymbolType=MenuSymbolType.glyph,  SymbolText = "\uE001",   MenuText = "BindingToAModel",         PageNavigate = typeof(B02)},
            new MenuItem() { SymbolType=MenuSymbolType.text,   SymbolText = "B03",      MenuText = "Indexers",                PageNavigate = typeof(B03)},
            new MenuItem() { SymbolType=MenuSymbolType.glyph,  SymbolText = "\uE0A2",   MenuText = "DataTemplates",           PageNavigate = typeof(B04)},
            new MenuItem() { SymbolType=MenuSymbolType.text,   SymbolText = "B05",      MenuText = "CollectionViewSource",    PageNavigate = typeof(B05)},
            new MenuItem() { SymbolType=MenuSymbolType.glyph,  SymbolText = "\uE005",   MenuText = "CollectionChanges",       PageNavigate = typeof(B06)},
            new MenuItem() { SymbolType=MenuSymbolType.text,   SymbolText = "B07",      MenuText = "IncrementalLoading",      PageNavigate = typeof(B07)},
            new MenuItem() { SymbolType=MenuSymbolType.glyph,  SymbolText = "\uE224",   MenuText = "UpdateSourceTrigger",     PageNavigate = typeof(B08)},
            new MenuItem() { SymbolType=MenuSymbolType.text,   SymbolText = "B09",      MenuText = "Fallback-TargetNull",     PageNavigate = typeof(B09)}
        };

    }
}
