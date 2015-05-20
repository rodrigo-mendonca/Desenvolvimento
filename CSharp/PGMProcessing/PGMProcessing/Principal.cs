using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PGMProcessing
{
    public partial class Principal : Form
    {
        PGM oControler = new PGM();

        public Principal()
        {
            InitializeComponent();
        }

        public void ImgReflesh()
        {
            if (!oControler.IsEmpty())
            {
                pctScreen.Image = oControler.GetPgmBitmap();

                this.Width  = 100+Math.Max(oControler.GetWidth(),580);
                this.Height = oControler.GetHeight() + ToolMenu.Height + 50;

                this.Text = oControler.cFielSource;

                ToolTip tt = new ToolTip();
                tt.SetToolTip(pctScreen, this.Text);
            }
            ToolUndo.Enabled = true;
            if (oControler.IsLastVerison())
                ToolRedo.Enabled = false;
        }

        private void ToolOpen_Click(object sender, EventArgs e)
        {
            oControler.OpenFile();

            if (!oControler.IsEmpty())
            {
                ImgReflesh();

                foreach (var item in ToolMenu.Items)
                {
                    if (item.GetType() == typeof(ToolStripSplitButton))
                    {
                        ToolStripSplitButton oMenu = (ToolStripSplitButton)item;
                        oMenu.Enabled = true;
                        foreach (ToolStripMenuItem item2 in oMenu.DropDownItems)
                        {
                            item2.Enabled = true;
                        }
                    }
                    if (item.GetType() == typeof(ToolStripButton)){
                        ToolStripButton oMenu = (ToolStripButton)item;
                        oMenu.Enabled = true;
                    }
                }
                stpReduction.Enabled = true;
                stpMedia.Enabled = true;
                stpDesvio.Enabled = true;
                ToolRedo.Enabled = false;
                ToolUndo.Enabled = false;

                stpDesvio.Value = Convert.ToDecimal(((double)stpMedia.Value) / 6F);
            }
        }

        private void ToolSave_Click(object sender, EventArgs e)
        {
            oControler.SaveFile();
        }

        private void ToolUndo_Click(object sender, EventArgs e)
        {
            oControler.Undo();
            ImgReflesh();

            ToolRedo.Enabled = true;
            if (oControler.IsFirstVerison())
                ToolUndo.Enabled = false;
        }

        private void ToolRedo_Click(object sender, EventArgs e)
        {
            oControler.Redo();
            ImgReflesh();
        }
        private void ToolLeft_Click(object sender, EventArgs e)
        {
            oControler.RotateLeft();
            ImgReflesh();
        }

        private void ToolRight_Click(object sender, EventArgs e)
        {
            oControler.RotateRight();
            ImgReflesh();
        }

        private void ToolSave_ButtonClick(object sender, EventArgs e)
        {
            oControler.SaveFile();
        }

        private void ToolSavar_Click(object sender, EventArgs e)
        {
            oControler.SaveFile();
        }

        private void ToolSavarComo_Click(object sender, EventArgs e)
        {
            oControler.SaveAsFile("");
        }

        private void ToolColorReduction_Click(object sender, EventArgs e)
        {
            oControler.ColorReduction((int)stpReduction.Value);
            ImgReflesh();
        }

        private void ToolFloydSteinberg_Click(object sender, EventArgs e)
        {
            oControler.FloydSteinberg((int)stpReduction.Value);
            ImgReflesh();
        }

        private void ToolHistograma_Click(object sender, EventArgs e)
        {
            Histogram oHis = new Histogram();
            oHis.Colors = oControler.GetColorList();
            oHis.Accum = oControler.GetColorAccumList();
            oHis.ShowDialog(this);
        }

        private void ToolEqualizarCores_Click(object sender, EventArgs e)
        {
            oControler.EqualizeColors();
            ImgReflesh();
        }

        private void stpMedia_ValueChanged(object sender, EventArgs e)
        {
            if (stpMedia.Value % 2 == 0)
                stpMedia.Value--;

            stpDesvio.Value = Convert.ToDecimal(((double)stpMedia.Value) / 6F);
        }

        private void ToolFiltroMedia_Click(object sender, EventArgs e)
        {
            oControler.FiltroMedia((int)stpMedia.Value);
            ImgReflesh();
        }

        private void ToolFiltroMediana_Click(object sender, EventArgs e)
        {
            oControler.FiltroMediana((int)stpMedia.Value);
            ImgReflesh();
        }

        private void ToolGaussiano_Click(object sender, EventArgs e)
        {
            oControler.Gaussiano((int)stpMedia.Value, (double)stpDesvio.Value);
            ImgReflesh();
        }

        private void ToolLaplaciano_Click(object sender, EventArgs e)
        {
            oControler.Laplaciano((int)stpMedia.Value, (double)stpDesvio.Value);
            ImgReflesh();
        }

        private void ToolErosaoMorfologica_Click(object sender, EventArgs e)
        {
            oControler.ErosaoMorfologica((int)stpMedia.Value);
            ImgReflesh();
        }

        private void ToolDilatacaoMorfologica_Click(object sender, EventArgs e)
        {
            oControler.DilatacaoMorfologica((int)stpMedia.Value);
            ImgReflesh();
        }

        private void ToolFourier_Click(object sender, EventArgs e)
        {
            oControler.Fourier('D');
            ImgReflesh();
        }
    }
}
