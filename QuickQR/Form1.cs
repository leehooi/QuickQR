using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickQR
{
    public partial class Form1 : Form
    {
        private string _qrContent;
        public Form1()
        {
            InitializeComponent();
            InitializeAppearance();
            _qrContent = Clipboard.GetText();
        }

        private void InitializeAppearance()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = this.Height - 20;
            this.Text = "QuickQR";
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            PaintQR(_qrContent);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            PaintQR(_qrContent);
        }

        private void PaintQR(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }
            var graphics = this.CreateGraphics();
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.L);
            QrCode qrCode = qrEncoder.Encode(str);
            var moduleSize = new FixedCodeSize(Math.Min((int)graphics.VisibleClipBounds.Width, (int)graphics.VisibleClipBounds.Height), QuietZoneModules.Two);
            GraphicsRenderer render = new GraphicsRenderer(moduleSize, Brushes.Black, Brushes.White);
            graphics.Clear(Color.White);
            render.Draw(graphics, qrCode.Matrix);
        }
    }
}
