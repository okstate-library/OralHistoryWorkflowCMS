using System.Windows.Controls;

namespace WpfApp.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class UIHelper
    {
        /// <summary>
        /// Sets the mutual exclusivity.
        /// </summary>
        /// <param name="currentCheckBox">The current CheckBox.</param>
        /// <param name="firstCheckBox">The first CheckBox.</param>
        /// <param name="secondCheckBox">The second CheckBox.</param>
        public static void SetMutualExclusivity(CheckBox currentCheckBox, CheckBox firstCheckBox, CheckBox secondCheckBox)
        {
            if (currentCheckBox.Name.Equals(firstCheckBox.Name))
            {
                if ((bool)currentCheckBox.IsChecked)
                {
                    secondCheckBox.IsChecked = false;
                }
                else
                {
                    secondCheckBox.IsChecked = true;
                }
            }
            else
            {
                if ((bool)currentCheckBox.IsChecked)
                {
                    firstCheckBox.IsChecked = false;
                }
                else
                {
                    firstCheckBox.IsChecked = true;
                }

            }
        }
    }
}
