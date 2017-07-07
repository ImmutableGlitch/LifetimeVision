using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace projWeekVisualiser
{
    public partial class Form1 : Form
    {
        int x = 0;
        int y = 0;

        /* Get the age and life expectancy of the user
         * get the current week of the year
         * print red squares up to current week
         * then print green squares up to life expectancy
         */

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBegin_Click(object sender , EventArgs e)
        {

            // Reset the panel for repainting
            x = 0;
            y = 0;
            Graphics g = pan.CreateGraphics();
            g.Clear(pan.BackColor);
            g.Dispose();

            // Store the user age, lifetime expectancy,
            // current week and amount of weeks passed to date
            var currentCulture = CultureInfo.CurrentCulture;
            var currentWeek = currentCulture.Calendar.GetWeekOfYear(
                            DateTime.Now ,
                            currentCulture.DateTimeFormat.CalendarWeekRule ,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);

            var age = Convert.ToInt32(txtAge.Text);
            var life = Convert.ToInt32(txtLife.Text);
            var weeksPassed = (52 * age - 1) + currentWeek;

            // Print all the weeks from birth to now
            printSquares(Color.FromArgb(239 , 97 , 97) , 1 , weeksPassed);
            // Print all the weeks from next week till death
            printSquares(Color.FromArgb(97 , 239 , 97) , weeksPassed + 1 , 52 * life);
        }

        private void printSquares(Color c , int start , int end)
        {
            SolidBrush b = new SolidBrush(c);
            Graphics g = pan.CreateGraphics();
            // Size of square to represent week
            int w = 8;
            int h = 8;
            int margin = w + 2;

            for (int i = start; i <= end; i++)
            {
                g.FillRectangle(b , x , y , w , h);

                x = x + margin;
                // Begin print on 'new line' after a year
                if (i % 52 == 0)
                {
                    x = 0;
                    y = y + margin;
                }
            }

            b.Dispose();
            g.Dispose();
        }
    }
}
