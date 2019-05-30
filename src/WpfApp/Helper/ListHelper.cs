using Core.Enums;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
namespace WpfApp.Helper
{
    /// <summary>
    /// Define the properties of the interview UI helper class.
    /// </summary>
    public class ListHelper
    {
        /// <summary>
        /// The equipment selection option list
        /// </summary>
        public static List<string> EquipmentSelectionOptionList = new List<string>()
        {
            " Audio",
            " Video",
            " Audio & Video"
        };
        

        public static List<string> GetPredefinedUser(WellKnownPredefinedUserType wellKnownPredefinedUserType)
        {
            List<string> list = new List<string>();

            List<PredefinedUserModel> founds = BaseUserControl.PredefinedUsers.FindAll(p => p.UserType == (byte)wellKnownPredefinedUserType);

            foreach (PredefinedUserModel item in founds)
            {
                list.Add(item.Name);
            }

            return list;
        }


    }


}
