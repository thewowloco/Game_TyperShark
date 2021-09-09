using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace game
{
    public class Fish
    {
        public bool is_alive = true;//tình trạng của cá (sống hay chết)
      

        private int step;
        public int Step//(độ dài mỗi bước của cá)
        {
            get { return step; }
            set { step = value; }
        }
        private int start_position;
        public int Start_position//Vị trí  bắt đầu của cá
        {
            get { return start_position; }
            set { start_position = value; }
        }
        private bool is_typed = false;
        public bool Is_typed//Đánh dấu chữ  đã đc gõ
        {

            get { return is_typed; }
            set { is_typed = value; }
        }
      

    }
}
