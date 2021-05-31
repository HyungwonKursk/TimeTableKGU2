using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Interface
{
    public interface IToast
    {
        // отображение всплывающего уведомления
        void Show(string message);
    }
}
