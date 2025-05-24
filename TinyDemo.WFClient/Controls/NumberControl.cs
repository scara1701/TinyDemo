
namespace TinyDemo.WFClient.Controls
{
    public class NumberControl : UserControl
    {
        private int _number = 1; //Gwen - Standaard cijfer

        public int Number
        {
            get { return _number; }
            set { _number = value; Invalidate(); } //Gwen - Zorgt ervoor dat de control opnieuw wordt getekend
        }

        public NumberControl()
        {
            this.Size = new Size(120, 120); //Gwen - Standaard grootte
            this.Paint += NumberControl_Paint;
        }

        private void NumberControl_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int diameter = Math.Min(this.Width, this.Height) - 10;
            int x = (this.Width - diameter) / 2;
            int y = (this.Height - diameter) / 2;

            // Tekent een cirkel
            g.DrawEllipse(Pens.Black, x, y, diameter, diameter);

            // Tekent het cijfer in de cirkel
            string text = _number.ToString();
            Font font = new Font("Arial", 24, FontStyle.Bold);
            SizeF textSize = g.MeasureString(text, font);
            float textX = x + (diameter - textSize.Width) / 2;
            float textY = y + (diameter - textSize.Height) / 2;
            g.DrawString(text, font, Brushes.Black, textX, textY);
        }
    }
}
