﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XwMaxLib.Objects;

namespace XwRemote
{
    public static class MyExt
    {
        public static void SelectID(this ComboBox combo, int ID)
        {
            foreach (var i in combo.Items)
            {
                ListItem item = (ListItem)i;
                if (item.ID == ID)
                {
                    combo.SelectedItem = i;
                    return;
                }
            }
        }
    }
}
