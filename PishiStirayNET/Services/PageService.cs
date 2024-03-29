﻿using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace PishiStirayNET.Services
{
    public class PageService
    {
        public event Action<Page> OnPageChanged;

        public void ChangePage(Page page)
        {
            Debug.WriteLine("Page changed");
            OnPageChanged?.Invoke(page);
        }


    }
}
