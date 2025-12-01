using System;
using System.Globalization;
using Elite_Dangerous_Addon_Launcher_V2.Localization;
using Elite_Dangerous_Addon_Launcher_V2.Properties;

namespace Elite_Dangerous_Addon_Launcher_V2.Tests
{
    /// <summary>
    /// Simple test class to verify localization works correctly.
    /// Run this by calling LocalizationTests.RunAllTests() from your app startup.
    /// </summary>
    public static class LocalizationTests
    {
        public static bool RunAllTests()
        {
            Console.WriteLine("=== Localization Tests ===\n");
            bool allPassed = true;

            allPassed &= TestEnglishResources();
            allPassed &= TestGermanResources();
            allPassed &= TestStringsHelper();
            allPassed &= TestCultureSwitch();

            Console.WriteLine(allPassed ? "\n✅ All tests passed!" : "\n❌ Some tests failed!");
            return allPassed;
        }

        private static bool TestEnglishResources()
        {
            Console.WriteLine("Test 1: English Resources (Default)");
            CultureInfo.CurrentUICulture = new CultureInfo("en");
            
            bool passed = true;
            
            // Test some key resources exist and have values
            var tests = new (string key, string expected)[]
            {
                ("AppTitle", "EDData Addon Helper"),
                ("MainWindow_AddApp", "Add App"),
                ("MainWindow_Launch", "Launch"),
                ("Button_OK", "OK"),
                ("Button_Cancel", "Cancel"),
            };

            foreach (var (key, expected) in tests)
            {
                var actual = Resources.ResourceManager.GetString(key, CultureInfo.CurrentUICulture);
                if (actual != expected)
                {
                    Console.WriteLine($"  ❌ {key}: Expected '{expected}', got '{actual}'");
                    passed = false;
                }
                else
                {
                    Console.WriteLine($"  ✅ {key}: '{actual}'");
                }
            }

            return passed;
        }

        private static bool TestGermanResources()
        {
            Console.WriteLine("\nTest 2: German Resources");
            CultureInfo.CurrentUICulture = new CultureInfo("de");
            
            bool passed = true;
            
            var tests = new (string key, string expected)[]
            {
                ("MainWindow_AddApp", "App hinzufügen"),
                ("MainWindow_Launch", "Starten"),
                ("Button_Cancel", "Abbrechen"),
                ("Button_Yes", "Ja"),
                ("Button_No", "Nein"),
            };

            foreach (var (key, expected) in tests)
            {
                var actual = Resources.ResourceManager.GetString(key, CultureInfo.CurrentUICulture);
                if (actual != expected)
                {
                    Console.WriteLine($"  ❌ {key}: Expected '{expected}', got '{actual}'");
                    passed = false;
                }
                else
                {
                    Console.WriteLine($"  ✅ {key}: '{actual}'");
                }
            }

            // Reset to English
            CultureInfo.CurrentUICulture = new CultureInfo("en");
            return passed;
        }

        private static bool TestStringsHelper()
        {
            Console.WriteLine("\nTest 3: Strings Helper Class");
            bool passed = true;

            // Test Get method
            CultureInfo.CurrentUICulture = new CultureInfo("en");
            var result = Strings.Get("MainWindow_Launch");
            if (result != "Launch")
            {
                Console.WriteLine($"  ❌ Strings.Get failed: Expected 'Launch', got '{result}'");
                passed = false;
            }
            else
            {
                Console.WriteLine($"  ✅ Strings.Get: '{result}'");
            }

            // Test missing key
            var missing = Strings.Get("NonExistentKey");
            if (missing != "[NonExistentKey]")
            {
                Console.WriteLine($"  ❌ Missing key handling failed: Expected '[NonExistentKey]', got '{missing}'");
                passed = false;
            }
            else
            {
                Console.WriteLine($"  ✅ Missing key returns: '{missing}'");
            }

            return passed;
        }

        private static bool TestCultureSwitch()
        {
            Console.WriteLine("\nTest 4: Culture Switching");
            bool passed = true;

            // Switch to German
            Strings.SetCulture("de");
            var german = Strings.Get("MainWindow_Launch");
            if (german != "Starten")
            {
                Console.WriteLine($"  ❌ German switch failed: Expected 'Starten', got '{german}'");
                passed = false;
            }
            else
            {
                Console.WriteLine($"  ✅ After SetCulture('de'): '{german}'");
            }

            // Switch back to English
            Strings.SetCulture("en");
            var english = Strings.Get("MainWindow_Launch");
            if (english != "Launch")
            {
                Console.WriteLine($"  ❌ English switch failed: Expected 'Launch', got '{english}'");
                passed = false;
            }
            else
            {
                Console.WriteLine($"  ✅ After SetCulture('en'): '{english}'");
            }

            return passed;
        }
    }
}
