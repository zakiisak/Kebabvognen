﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class AdapterManager
    {
        private static DalAdapter adapter = new SQLAdapter();

        public AdapterManager()
        {
            
        }

        private static void AssertStart()
        {
            if (adapter.IsRunning() == false)
                adapter.Start();
        }

        public static Menu[] GetMenus()
        {
            AssertStart();
            return adapter.GetMenus();
        }

        public static Review[] Get5NewestReviews()
        {
            AssertStart();
            return adapter.GetNewestReviews(5);
        }


        public static OpeningHours[] GetOpeningHours()
        {
            AssertStart();
            return adapter.GetOpeningHours();
        }

        public static void Dispose()
        {
            if(adapter.IsRunning())
                adapter.Stop();
        }

    }
}
