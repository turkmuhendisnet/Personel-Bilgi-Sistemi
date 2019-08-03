using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelBilgiSistemiCalismasi
{
    public static class KontrolTemizlemeSinifi
    {
        public static void KontrolTemizle (Control.ControlCollection FormUzerindekiKontroller)
        {
            foreach (Control Kontrol in FormUzerindekiKontroller)
            {
                if (Kontrol is TextBox) ((TextBox)Kontrol).Clear();
                else if (Kontrol is DateTimePicker) ((DateTimePicker)Kontrol).Value = DateTime.Now;
                else if (Kontrol is ComboBox) ((ComboBox)Kontrol).SelectedIndex = -1;
                else if (Kontrol is PictureBox) ((PictureBox)Kontrol).Image = null;
            }
            
        }
    }
}
