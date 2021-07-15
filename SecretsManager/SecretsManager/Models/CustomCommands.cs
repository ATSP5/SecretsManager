using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecretsManager.Models
{
    public static class CustomCommands // For the purpose of commands eg.: Ctrl+E handling!
    {
        public static readonly RoutedUICommand EncryptCMD = new RoutedUICommand
            (
                "EncryptCMD",
                "EncryptCMD",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.E, ModifierKeys.Control)
                }
            );
        public static readonly RoutedUICommand DecryptCMD = new RoutedUICommand
           (
               "DecryptCMD",
               "DecryptCMD",
               typeof(CustomCommands),
               new InputGestureCollection()
               {
                    new KeyGesture(Key.D, ModifierKeys.Control)
               }
           );

    }
}
