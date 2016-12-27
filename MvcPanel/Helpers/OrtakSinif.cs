using MvcPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPanel.Helpers
{
    public class OrtakSinif
    {
        MvcPanelDB db = new MvcPanelDB();

        public static bool EditIzinYetkiVarmi(int id,Kullanici user)
        {
            
            if (user.ID == id)
            {

                return true;
            }
            else if (user.YetkiID > 2)
            {
                
                return true;
            }

            return false;
        }


        public static bool DeleteIzinYetkiVarmi(int id, Kullanici user)
        {

            if (user.ID == id)
            {

                return true;
            }
            else if (user.YetkiID > 3)
            {

                return true;
            }

            return false;


        }


        }
}